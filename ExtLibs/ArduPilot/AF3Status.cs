using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;

namespace MissionPlanner.Utilities.AF3
{
    public static class Constants
    {
        public const float lowerVoltageThres = 6800f;
        public const float midVoltageThres = 7200f;
        public const float higherCurrentThres = 8000f;
        public const float midCurrentThres = 7000f;

        public const int numPowerBuses = 2;
        public const int BUS_A = 1;
        public const int BUS_B = 2;

        /// <summary>
        /// If a CAN bus is not visitible for more than canElapsedThreshold seconds,
        /// it's deemed faulty
        /// </summary>
        public const int canElapsedThreshold = 1;

        /// <summary>
        /// After AF3 status message is received, it takes sysExpBootupMs milliseconds
        /// to start recording errors. Past this point, if an RFC remains unresponsive,
        /// the ECAM message "SYS INIT SLOW" is also displayed
        /// </summary>
        public const int sysExpBootupMs = 1500;
        public const int maxCpuLoading = 70;
        public const int maxCpuTemperature = 70;
    }

    public static class Util
    {
        static public String MillisToHumanTime(double millis)
        {
            String result = "";
            double seconds = millis / 1000;
            double minutes = seconds / 60;
            double hours = minutes / 60;

            if ((int)hours > 0)
            {
                double min = (hours - ((int)hours)) * 60;
                result = String.Format("{0:0}:{1:00}'", hours, min);
            }
            else if ((int)minutes > 0)
            {
                double secs = seconds - (((int)minutes) * 60);
                result = String.Format("{0:0}' {1:00}\"", minutes, secs);
            }
            else
            {
                result = String.Format("{0:0.0} s", seconds);
            }
            return result;
        }

        static public string printHumanSequence(bool[] flags)
        {
            string ret = "";
            int sumFlagsSet = 0;
            int sumFlagsPrinted = 0;

            for (int i = 0; i < flags.Length; i++)
            {
                if (flags[i]) sumFlagsSet++;
            }

            for (int i = 0; i < flags.Length; i++)
            {
                if (flags[i]) {
                    if (sumFlagsPrinted != 0)
                    {
                        if (sumFlagsPrinted == sumFlagsSet - 1)
                        {
                            ret += " AND ";
                        }
                        else
                        {
                            ret += ", ";
                        }
                    }
                    ret += (i + 1).ToString();
                    sumFlagsPrinted++;
                }
            }

            return ret;
        }
    }

    public class Status
    {
        public float score { get; set; }
        public int number_rfcs { get; set; }
        public float vfcCpuTemperature { get; set; }
        public int vfcCpuLoading { get; set; }
        public float vfcPsVoltageA { get; set; }
        public float vfcPsVoltageB { get; set; }
        public float vfcBitrateTxAf3 { get; set; }
        public float vfcBitrateRxAf3 { get; set; }
        public float vfcBitrateTxAux { get; set; }
        public float vfcBitrateRxAux { get; set; }
        public int number_buses { get { return number_rfcs == 1 ? 2 : 3; } }
        public int all_can_mask { get { return number_rfcs == 1 ? 3 : 7; } }
        public bool[] telemRFC = new bool[] { false, false, false };
        public bool[] ppmVisRFC = new bool[] { false, false, false };
        public uint[] flightModeRFC = new uint[] { 0, 0, 0 };
        public uint[] armedStatRFC = new uint[] { 0, 0, 0 };
        public uint[] canElapsedRFC = new uint[] { 0, 0, 0 };
        /// <summary>
        /// Ecam Messages are only shown if errors ocurred after the 
        /// clearCutoffTimestemp.
        /// </summary>
        private DateTime clearCutoffTimestamp = DateTime.Now;

        public bool allRfcDetected { get {

                if (firstRfcDetected == DateTime.MinValue)
                {

                    bool det = number_rfcs > 0;

                    for (int i = 0; i < number_rfcs; i++)
                    {
                        det = det && telemRFC[i];
                    }

                    if (det)
                    {
                        firstRfcDetected = DateTime.Now;
                    }

                    return det;
                }

                return true;
            } }
        public float[] scoreRFC = new float[] { float.NaN, float.NaN, float.NaN };
        public float activeRFC { 
            get { return _activeRFC; }
            set { 
                _activeRFC = value;
                if (firstAf3MsgDetected == DateTime.MinValue)
                {
                    firstAf3MsgDetected = DateTime.Now;
                }
            } }
        private List<EndPoint> endpointCollection = new List<EndPoint>();
        private List<errorRecord> errorList = new List<errorRecord>();
        private object epCollectionLock = new object();
        private object erCollectionLock = new object();
        private DateTime firstRfcDetected = DateTime.MinValue;
        private DateTime firstAf3MsgDetected = DateTime.MinValue;

        private float _activeRFC;

        public Status()
        {
            _activeRFC = -1.0f;
        }

        private void addError(string origin, errorRecord.opCode state, String message, int failedBuses)
        {
            if (firstRfcDetected == DateTime.MinValue)
                return;

            if (DateTime.Now.Subtract(firstRfcDetected).TotalMilliseconds < Constants.sysExpBootupMs)
                return;

            lock (erCollectionLock)
            {

                errorRecord lastError = errorList.FindLast(error => error.origin == origin &&
                    error.state == state && error.failedBuses == failedBuses && !error.resolved);

                if (lastError == null)
                {
                    errorRecord errRec = new errorRecord(state, message, failedBuses, origin);
                    errorList.Add(errRec);
                }

            }

        }

        public void ClearEcamMessages()
        {
            clearCutoffTimestamp = DateTime.Now;
        }

        public List<ecamErrorRecord> getEcamErrors()
        {
            // Only takes into account errors that are currently unresolved
            // or were generated after the cutoff timestamp
            var errList = getErrors().FindAll(error => error.timestamp > 
                clearCutoffTimestamp || !error.resolved);

            var retEcamErrList = new List<ecamErrorRecord>();
            int epNo = 0;
            string ecamMsg = "";

            lock (epCollectionLock)
            {
                epNo = endpointCollection.Count;
            }

            // detect CAN buses where endpoints are not communicating   

            bool[] failedBus = new bool[number_buses];
            uint failedBusMask = 0;
            int failedBusCount = 0;

            for (int i = 0; i < number_buses; i++)
            {
                // find whether all endpoints in this bus 
                // have reported issues

                List<string> failedBusEp = new List<string>();

                var logicalBusIssues = errList.FindAll(error =>
                    error.state == errorRecord.opCode.BUS_ERROR &&
                    (error.failedBuses & (1 << i)) > 0);

                foreach (var error in logicalBusIssues)
                {
                    if (!failedBusEp.Contains(error.origin))
                    {
                        failedBusEp.Add(error.origin);
                    }
                }

                if (failedBusEp.Count == epNo)
                {

                    var rfcCanIssues = errList.FindAll(error =>
                        error.state == errorRecord.opCode.RFC_NO_CAN &&
                        (error.failedBuses & (1 << i)) > 0);

                    if (logicalBusIssues.Count >= epNo && rfcCanIssues.Count > 0)
                    {
                        failedBus[i] = true;
                        failedBusCount++;
                    }

                }

            }

            if (failedBusCount > 0)
            {
                for (int i = 0; i < failedBus.Length; i++)
                {
                    failedBusMask += failedBus[i] ? 1 : 0 << 0;
                }

                ecamMsg = String.Format("CAN BUS {0} FAIL", Util.printHumanSequence(failedBus));
                retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.CRITICAL));
            }

            //Check for low bus voltage

            bool[] lowVoltageBus = new bool[Constants.numPowerBuses];

            for (int i = 0; i < Constants.numPowerBuses; i++)
            {
                var numFailedEPs = errList.FindAll(error =>
                    error.state == errorRecord.opCode.BUS_ERROR &&
                    error.failedBuses == 7).Count;

                var numLowVoltage = errList.FindAll(error =>
                    error.state == errorRecord.opCode.BUS_VOLTAGE &&
                    error.failedBuses == 1 << i).Count;

                var pwrBusName = Char.ConvertFromUtf32(i + 65);

                if (epNo > 0 && numLowVoltage >= (epNo - numFailedEPs))
                {
                    ecamMsg = String.Format("POWER BUS {0} LOW VOLTAGE", pwrBusName);
                    retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.CRITICAL));
                }
            }

            if (allRfcDetected)
            {

                // Check RFC CAN bus failure or intermittency

                bool[] failedCanRFC = new bool[number_rfcs];
                bool[] intermittentCanRFC = new bool[number_rfcs];
                bool allCanRFCfailed = true;

                for (int i = 0; i < number_rfcs; i++)
                {
                    if (failedBus[i]) continue;

                    failedCanRFC[i] = intermittentCanRFC[i] = false;

                    var canIssues = errList.FindAll(error =>
                        error.state == errorRecord.opCode.RFC_NO_CAN &&
                        (error.failedBuses & (1 << i)) > 0);

                    if (canIssues.Count > 0)
                    {

                        if (canIssues[canIssues.Count - 1].resolved)
                        {
                            intermittentCanRFC[i] = true;
                        }
                        else
                        {
                            failedCanRFC[i] = true;
                        }

                    }

                    // Cheking whether all RFC's have failed to communicate on the
                    // respective CAN buses
                    allCanRFCfailed &= !intermittentCanRFC[i] && failedCanRFC[i];

                }

                if (allCanRFCfailed)
                {
                    ecamMsg = "ALL RFC CAN FAIL";
                    retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.CRITICAL));
                }
                else
                {

                    for (int i = 0; i < number_rfcs; i++)
                    {

                        if (intermittentCanRFC[i])
                        {
                            ecamMsg = String.Format("RFC{0} CAN INTERMITTENT", i + 1);
                            retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.ALERT));
                        }
                        else if (failedCanRFC[i])
                        {
                            ecamMsg = String.Format("RFC{0} CAN FAIL", i + 1);
                            retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.ALERT));
                        }

                    }

                }

                // Check RFC telemetry availability or intermittency

                bool[] failedTelemRFC = new bool[number_rfcs];
                bool[] intermittentTelemRFC = new bool[number_rfcs];
                bool allTelemRFCfailed = true;

                for (int i = 0; i < number_rfcs; i++)
                {
                    if (failedBus[i]) continue;

                    failedTelemRFC[i] = intermittentTelemRFC[i] = false;

                    var telemIssues = errList.FindAll(error =>
                        error.state == errorRecord.opCode.TELEM_FAILURE &&
                        (error.failedBuses & (1 << i)) > 0);

                    if (telemIssues.Count > 0)
                    {

                        if (telemIssues[telemIssues.Count - 1].resolved)
                        {
                            intermittentTelemRFC[i] = true;
                        }
                        else
                        {
                            failedTelemRFC[i] = true;
                        }

                    }

                    allTelemRFCfailed &= !intermittentTelemRFC[i] && failedTelemRFC[i];

                }

                if (allTelemRFCfailed)
                {
                    ecamMsg = "ALL RFC TELEM UNAVAIL";
                    retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.CRITICAL));
                }
                else
                {

                    for (int i = 0; i < number_rfcs; i++)
                    {

                        if (intermittentTelemRFC[i])
                        {
                            ecamMsg = String.Format("RFC{0} TELEM INTERMITTENT", i + 1);
                            retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.ALERT));
                        }
                        else if (failedTelemRFC[i])
                        {
                            ecamMsg = String.Format("RFC{0} TELEM UNAVAIL", i + 1);
                            retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.ALERT));
                        }

                    }

                }

            }

            // Check for endpoints not responding in any 
            // of the CAN buses

            List<string> failedEp = new List<string>();
            List<string> intermittentEp = new List<string>();

            var epFullFailureList = errList.FindAll(error =>
                    error.state == errorRecord.opCode.BUS_ERROR &&
                    error.failedBuses == all_can_mask);

            foreach (var error in epFullFailureList)
            {
                var epFullFailureInstances = epFullFailureList.FindAll(err => 
                    err.origin == error.origin &&
                    (err.failedBuses & (~failedBusMask)) > 0);

                if (epFullFailureInstances.Count == 1)
                {
                    if (!epFullFailureInstances[0].resolved)
                    {
                        if (!failedEp.Contains(error.origin))
                        {
                            failedEp.Add(error.origin);
                        }
                    }

                }
                else
                {
                    if (!intermittentEp.Contains(error.origin))
                    {
                        intermittentEp.Add(error.origin);
                    }
                }
            }

            foreach (var origin in intermittentEp)
            {
                ecamMsg = String.Format("{0} INTERMITTENT", origin);
                retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.CRITICAL));
            }

            foreach (var origin in failedEp)
            {
                if (!intermittentEp.Contains(origin))
                {
                    ecamMsg = String.Format("{0} FAIL", origin);
                    retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.CRITICAL));
                }
            }

            // Check PPM streams
            bool[] failedPPM = new bool[number_rfcs];
            bool[] intermittentPPM = new bool[number_rfcs];
            int failedPPMCount = 0;
            int intermittentPPMCount = 0;
            int failOrIntPPMCount = 0;

            for (int i = 0; i < number_rfcs; i++)
            {
                failedPPM[i] = intermittentPPM[i] = false;

                var ppmIssues = errList.FindAll(error =>
                    error.state == errorRecord.opCode.RFC_NO_PPM &&
                    (error.failedBuses & (1 << i)) > 0);

                if (ppmIssues.Count == 1)
                {
                    if (!ppmIssues[0].resolved)
                    {
                        failedPPM[i] = true;
                        failedPPMCount++;
                    }
                }

                if (ppmIssues.Count > 1)
                {
                    intermittentPPM[i] = true;
                    intermittentPPMCount++;
                }

                if (intermittentPPM[i] || failedPPM[i]) failOrIntPPMCount++;

            }

            if (failOrIntPPMCount >= number_rfcs && number_rfcs > 0)
            {
                ecamMsg = String.Format("NO PPM ALL RFC");
                retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.ALERT));
            }
            else
            {

                for (int i = 0; i < number_rfcs; i++)
                {
                    if (intermittentPPM[i])
                    {
                        ecamMsg = String.Format("PPM RFC{0} INTERMITTENT", i + 1);
                        retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.CRITICAL));
                    }
                    else if (failedPPM[i])
                    {
                        ecamMsg = String.Format("PPM RFC{0} FAIL", i + 1);
                        retEcamErrList.Add(new ecamErrorRecord(ecamMsg, ecamErrorRecord.severityType.CRITICAL));
                    }
                }

            }

            if (allRfcDetected)
            {
                var notOkEcamMsgs = retEcamErrList.FindAll(err => err.severity != ecamErrorRecord.severityType.OK);

                if (notOkEcamMsgs.Count == 0)
                {
                    var okMsg = new ecamErrorRecord("SYSTEM OK",
                        ecamErrorRecord.severityType.OK);

                    retEcamErrList.Add(okMsg);
                }

            }
            else if (firstAf3MsgDetected != DateTime.MinValue && 
                DateTime.Now.Subtract(firstAf3MsgDetected).TotalMilliseconds > Constants.sysExpBootupMs)
            {
                var okMsg = new ecamErrorRecord("SYS INIT SLOW",
                    ecamErrorRecord.severityType.ALERT);

                retEcamErrList.Add(okMsg);
            }

            return retEcamErrList;
        }

        public List<errorRecord> getErrors()
        {
            var retErrList = new List<errorRecord>();

            lock (erCollectionLock)
            {

                for (int i = 0; i < errorList.Count; i++)
                {
                    errorRecord err = errorList[i];

                    retErrList.Add(new errorRecord(
                        err.state,
                        err.message,
                        err.failedBuses,
                        err.origin,
                        err.timestamp,
                        err.resolveTimestamp));
                }
            }

            return retErrList;
        }

        public bool checkArmedStatusMismatch(int rfcIndex)
        {
            bool rfHealthBad = false;

            if (number_rfcs > 1)
            {
                Dictionary<uint, uint> armStatusGroup = new Dictionary<uint, uint>(number_rfcs);

                // Evaluate whether flight modes have mismatched across RFC's
                for (int i = 0; i < number_rfcs; i++)
                {
                    uint asCount = 0;

                    if (armStatusGroup.TryGetValue(armedStatRFC[i], out asCount))
                    {
                        armStatusGroup[armedStatRFC[i]] = asCount + 1;
                    }
                    else
                    {
                        armStatusGroup.Add(armedStatRFC[i], 1);
                    }

                }

                if (armStatusGroup[armedStatRFC[rfcIndex]] <= 1)
                {
                    // This is the only RFC on this mode at the moment.
                    rfHealthBad = true;
                }

            }

            return rfHealthBad;
        }

        public bool checkFlightModeMismatch(int rfcIndex)
        {
            bool rfHealthBad = false;

            if (number_rfcs > 1)
            {
                Dictionary<uint, uint> flightModeGroup = new Dictionary<uint, uint>(number_rfcs);

                // Evaluate whether flight modes have mismatched across RFC's
                for (int i = 0; i < number_rfcs; i++)
                {
                    uint fmCount = 0;

                    if (flightModeGroup.TryGetValue(flightModeRFC[i], out fmCount))
                    {
                        flightModeGroup[flightModeRFC[i]] = fmCount + 1;
                    }
                    else
                    {
                        flightModeGroup.Add(flightModeRFC[i], 1);
                    }

                }

                if (flightModeGroup[flightModeRFC[rfcIndex]] <= 1)
                {
                    // This is the only RFC on this mode at the moment.
                    rfHealthBad = true;
                }

            }

            return rfHealthBad;
        }

        private int analyseCondition(bool errorCondition, string origin, errorRecord.opCode errType, 
            string errorMsg, int failedBuses, bool checkBuses = false)
        {
            if (errorCondition)
            {
                addError(String.Format("{0}", origin),
                    errType,
                    errorMsg,
                    failedBuses);

                return 1;
            }
            else
            {

                errorRecord err;
                
                if (checkBuses)
                    err = errorList.FindLast(error => error.origin == origin &&
                        error.state == errType &&
                        !error.resolved &&
                        error.failedBuses == failedBuses);
                else
                    err = errorList.FindLast(error => error.origin == origin &&
                        error.state == errType &&
                        !error.resolved);

                if (err != null)
                {
                    err.resolve();
                }

                return 0;
            }
        }

        private void clearUnresolvedErrorsByType(string origin, errorRecord.opCode type, int failedBuses)
        {
            List<errorRecord> errList = errorList.FindAll(error => error.origin == origin &&
                        (error.state == type) &&
                        !error.resolved &&
                        error.failedBuses != failedBuses);

            foreach (errorRecord err in errList)
            {
                err.resolve();
            }
        }

        public float calculateScore()
        {
            // Check how many telemetry links are lost between RFC's and VFC
            int score = 0;

            lock (erCollectionLock)
            {

                for (int i = 0; i < number_rfcs; i++)
                {
                    int rfcNo = i + 1;
                    string origin = String.Format("RFC{0}", rfcNo);

                    score += analyseCondition(!telemRFC[i],
                        origin, errorRecord.opCode.TELEM_FAILURE,
                        String.Format("Telemetry link not available between {0} and VFC", origin),
                        1 << i);

                    score += analyseCondition(checkFlightModeMismatch(i),
                        origin, errorRecord.opCode.MODE_MISMATCH,
                        String.Format("Flight mode mismatch on {0}", origin),
                        1 << i);

                    score += analyseCondition(checkArmedStatusMismatch(i),
                        origin, errorRecord.opCode.ARM_MISMATCH,
                        String.Format("Arm status mismatch on {0}", origin),
                        1 << i);

                    score += analyseCondition(canElapsedRFC[i] > Constants.canElapsedThreshold,
                        origin, errorRecord.opCode.RFC_NO_CAN,
                        String.Format("{0} became disconnected from CAN bus", origin),
                        1 << i);

                    score += analyseCondition(!ppmVisRFC[i],
                        origin, errorRecord.opCode.RFC_NO_PPM,
                        String.Format("{0} is not receiveing PPM stream", origin),
                        1 << i);

                }

                // Check if any of the endpoints becomes unresponsive
                // or disconnected from a bus
                lock (epCollectionLock)
                {
                    foreach (var ep in endpointCollection)
                    {
                        // Check communication issues

                        var stale = ep.isDataStale();
                        var busError = ep.isBusMissing();
                        string origin = String.Format("EP{0}", ep.esc_index);

                        if (stale)
                        {
                            String errorMessage = String.Format("Endpoint not communicating in any CAN bus");
                            int failedBuses = 7; // 7 corresponds to all buses failing

                            addError(origin,
                                errorRecord.opCode.BUS_ERROR, errorMessage, failedBuses);

                            clearUnresolvedErrorsByType(origin, errorRecord.opCode.BUS_ERROR, failedBuses);

                        }
                        else if (busError != 0)
                        {
                            int err = (int)busError;
                            int bus0Error = err & 1;
                            int bus1Error = (err & 2) >> 1;
                            int bus2Error = (err & 4) >> 2;
                            int sumErrors = bus0Error + bus1Error + bus2Error;

                            if (sumErrors > 1)
                            {
                                String errorMessage = String.Format("Endpoint not communicating in buses: {0}{1}{2}{3}",
                                    bus0Error > 0 ? "1 and " : "",
                                    bus1Error > 0 ? "2" : "",
                                    (bus1Error + bus2Error) > 1 ? " and " : "",
                                    bus2Error > 0 ? "3" : "");

                                addError(origin,
                                    errorRecord.opCode.BUS_ERROR, errorMessage, err);
                            }
                            else
                            {
                                String errorMessage = String.Format("Endpoint not communicating in bus {0}{1}{2}",
                                    bus0Error > 0 ? "1" : "",
                                    bus1Error > 0 ? "2" : "",
                                    bus2Error > 0 ? "3" : "");

                                addError(origin,
                                    errorRecord.opCode.BUS_ERROR, errorMessage, err);

                            }

                            clearUnresolvedErrorsByType(origin, errorRecord.opCode.BUS_ERROR, busError);

                        }
                        else
                        {
                            List<errorRecord> errList = errorList.FindAll(error => error.origin == origin &&
                            (error.state == errorRecord.opCode.BUS_ERROR) &&
                            !error.resolved);

                            foreach (errorRecord err in errList)
                            {
                                err.resolve();
                            }
                        }

                        // Check bus voltages and currents

                        score += analyseCondition(ep.voltageA < Constants.lowerVoltageThres,
                            origin, errorRecord.opCode.BUS_VOLTAGE,
                            String.Format("{0} low voltage on bus A", origin),
                            Constants.BUS_A, true);

                        score += analyseCondition(ep.currentA > Constants.higherCurrentThres,
                            origin, errorRecord.opCode.BUS_CURRENT,
                            String.Format("{0} low voltage on bus B", origin),
                            Constants.BUS_A, true);

                        score += analyseCondition(ep.voltageB < Constants.lowerVoltageThres,
                            origin, errorRecord.opCode.BUS_VOLTAGE,
                            String.Format("{0} low voltage on bus A", origin),
                            Constants.BUS_B, true);

                        score += analyseCondition(ep.currentB > Constants.higherCurrentThres,
                            origin, errorRecord.opCode.BUS_CURRENT,
                            String.Format("{0} low voltage on bus B", origin),
                            Constants.BUS_B, true);
                    }
                }
            }

            return (float) score;
        }

        public void addEndpoint(EndPoint endpoint)
        {
            lock (epCollectionLock)
            {
                endpointCollection.Add(endpoint);
            }
        }

        public EndPoint getEndpoint(int index)
        {
            lock (epCollectionLock)
            {
                if ((index < endpointCollection.Count) &&
                    (index >= 0) &&
                    (number_rfcs > 0))
                {
                    EndPoint epItem = endpointCollection[index];

                    EndPoint epRetItem = new EndPoint(epItem.esc_index,
                        epItem.voltageA, epItem.voltageB, epItem.currentA, epItem.currentB,
                        epItem.rpm, epItem.elapsedSecBus, epItem.lastUpdate, number_rfcs);

                    return epRetItem;

                }
                else
                {
                    return null;
                }
            }
        }

        public int getEndpointCount()
        {
            lock (epCollectionLock)
            {
                return endpointCollection.Count;
            }
        }

        public EndPoint findEndpoint (uint escIndex)
        {
            lock (epCollectionLock)
            {
                EndPoint endpoint =
                    endpointCollection.Find(ep => ep.esc_index == escIndex);

                return endpoint;
            }
        }

        public void sortEndpoints ()
        {
            lock (epCollectionLock)
            {
                endpointCollection.Sort();
            }
        }


    }

    public class ecamErrorRecord
    {
        public string message;
        public severityType severity;
        
        public enum severityType
        {
            OK,
            ALERT,
            CRITICAL
        }

        public ecamErrorRecord(string msg, severityType _severity)
        {
            message = msg;
            severity = _severity;
        }
    }

    public class errorRecord
    {
        public DateTime timestamp;
        public string origin;
        public opCode state;
        public String message;
        public int failedBuses;
        public bool resolved;
        public DateTime resolveTimestamp;
        public string hash;

        public enum opCode
        {
            NORMAL = 0,
            MODE_MISMATCH,
            TELEM_FAILURE,
            BUS_VOLTAGE,
            BUS_CURRENT,
            ARM_MISMATCH,
            RFC_NO_CAN,
            RFC_NO_PPM,
            BUS_ERROR
        };

        public errorRecord(opCode st, String msg, int failBusesMask, string _origin)
        {
            origin = _origin;
            timestamp = DateTime.Now;
            message = msg;
            state = st;
            failedBuses = failBusesMask;
            resolved = false;
            resolveTimestamp = DateTime.MinValue;
            hash = timestamp.ToBinary().ToString() +
                origin + state.ToString() + failedBuses.ToString();
        }

        public errorRecord(opCode st, String msg, int failBusesMask, string _origin, 
            DateTime creationTime, DateTime resolvedTime)
        {
            origin = _origin;
            timestamp = creationTime;
            message = msg;
            state = st;
            failedBuses = failBusesMask;

            hash = timestamp.ToBinary().ToString() +
                origin + state.ToString() + failedBuses.ToString();

            resolve(resolvedTime);
        }

        public void resolve(DateTime rTimestamp)
        {
            resolved = rTimestamp != DateTime.MinValue;
            resolveTimestamp = rTimestamp;
        }

        public void resolve()
        {
            resolved = true;
            resolveTimestamp = DateTime.Now;
        }
    }

    public class EndPoint : IComparable, IEquatable<EndPoint>
    {
        public uint esc_index;
        public float voltageA;
        public float voltageB;
        public float currentA;
        public float currentB;
        public int rpm;
        public double elapsed;
        public DateTime lastUpdate;
        public byte[] elapsedSecBus = new byte[] {0,0,0};
        public byte elapsedSecBus1;
        public byte elapsedSecBus2;
        public int numRFCs;

        private const int maxPeriodTotal = 6200; 
        private const int maxPeriodSameBus = 3000;
        public EndPoint(uint escIndex, float busVoltageA, float busVoltageB, 
            float busCurrA, float busCurrB, int engRPM, byte[] busElapsed, 
            DateTime timestamp, int numRFCS)
        {
            esc_index = escIndex;
            voltageA = busVoltageA;
            voltageB = busVoltageB;
            currentA = busCurrA;
            currentB = busCurrB;
            lastUpdate = timestamp;
            rpm = engRPM;
            elapsedSecBus[0] = busElapsed[0];
            elapsedSecBus[1] = busElapsed[1];
            elapsedSecBus[2] = busElapsed[2];
            numRFCs = numRFCS;

        }

        public bool isDataStale()
        {
            elapsed = DateTime.Now.Subtract(lastUpdate).TotalMilliseconds;
            return (elapsed > maxPeriodTotal);
        }

        public int isBusMissing()
        {
            int result = 0;
            result += ((int)elapsedSecBus[0]*1000 > maxPeriodSameBus) ? 1 : 0;
            if (numRFCs > 1)
            {
                result += ((int)elapsedSecBus[1]*1000 > maxPeriodSameBus) ? 2 : 0;
            }
            if (numRFCs > 2)
            {
                result += ((int)elapsedSecBus[2]*1000 > maxPeriodSameBus) ? 4 : 0;
            }
            return result;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            EndPoint otherEndpoint = obj as EndPoint;
            if (otherEndpoint != null)
                return this.esc_index.CompareTo(otherEndpoint.esc_index);
            else
                throw new ArgumentException("Object is not a Temperature");
        }

        public override int GetHashCode()
        {
            return (int)esc_index;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            EndPoint objAsPart = obj as EndPoint;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(EndPoint other)
        {
            if (other == null) return false;
            return other.esc_index == esc_index;
        }
    }
}