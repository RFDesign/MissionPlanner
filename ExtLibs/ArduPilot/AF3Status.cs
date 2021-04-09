using System;
using System.Collections.Generic;
using System.Windows;

namespace MissionPlanner.Utilities
{
    public class AF3Status
    {
        public float score { get; set; }
        public int number_rfcs { get; set; }
        public bool[] telemRFC = new bool[] { false, false, false };
        public bool[] ppmVisRFC = new bool[] { false, false, false };
        public uint[] flightModeRFC = new uint[] { 0, 0, 0 };
        public uint[] armedStatRFC = new uint[] { 0, 0, 0 };
        public uint[] canElapsedRFC = new uint[] { 0, 0, 0 };
        public float[] scoreRFC = new float[] { float.NaN, float.NaN, float.NaN };

        public float activeRFC { get; set; }
        private List<AF3EndPoint> endpointCollection = new List<AF3EndPoint>();
        private List<errorRecord> errorList = new List<errorRecord>();
        private object epCollectionLock = new object();
        private object erCollectionLock = new object();

        public AF3Status()
        {
            activeRFC = -1.0f;
        }

        public String MillisToTime(double millis)
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

        private void addError(string origin, errorRecord.opCode state, String message, int failedBuses)
        {
            lock (erCollectionLock)
            {

                errorRecord lastError = errorList.FindLast(error => error.origin == origin &&
                    error.state == state && error.failedBuses == failedBuses && error.resolved == DateTime.MinValue);

                if (lastError == null)
                {
                    errorRecord errRec = new errorRecord(state, message, failedBuses, origin);
                    errorList.Add(errRec);
                }

            }

        }

        public List<errorRecord> getErrors()
        {
            return errorList;
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

        private int analyseCondition(bool errorCondition, string origin, errorRecord.opCode errType, string errorMsg, int failedBuses)
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

                errorRecord err = errorList.FindLast(error => error.origin == origin &&
                error.state == errType &&
                error.resolved == DateTime.MinValue);

                if (err != null)
                {
                    err.resolved = DateTime.Now;
                }

                return 0;
            }
        }

        private void clearUnresolvedErrorsByType(string origin, errorRecord.opCode type, int failedBuses)
        {
            List<errorRecord> errList = errorList.FindAll(error => error.origin == origin &&
                        (error.state == type) &&
                        error.resolved == DateTime.MinValue &&
                        error.failedBuses != failedBuses);

            foreach (errorRecord err in errList)
            {
                err.resolved = DateTime.Now;
            }
        }

        public float calculateAf3Score()
        {
            // Check how many telemetry links are lost between RFC's and VFC
            int score = 0;

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

                score += analyseCondition(canElapsedRFC[i] > 1,
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
                        error.resolved == DateTime.MinValue);
                        
                        foreach(errorRecord err in errList)
                        {
                            err.resolved = DateTime.Now;
                        }
                    }
                }
            }

            if (score > 0)
                Console.WriteLine("AF3 presents at least one issue");

            return (float) score;
        }

        public void addEndpoint(AF3EndPoint endpoint)
        {
            lock (epCollectionLock)
            {
                endpointCollection.Add(endpoint);
            }
        }

        public AF3EndPoint getEndpoint(int index)
        {
            lock (epCollectionLock)
            {
                if ((index < endpointCollection.Count) &&
                    (index >= 0) &&
                    (number_rfcs > 0))
                {
                    AF3EndPoint epItem = endpointCollection[index];

                    AF3EndPoint epRetItem = new AF3EndPoint(epItem.esc_index,
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

        public AF3EndPoint findEndpoint (uint escIndex)
        {
            lock (epCollectionLock)
            {
                AF3EndPoint endpoint =
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

    public class errorRecord
    {
        public DateTime timestamp;
        public string origin;
        public opCode state;
        public String message;
        public int failedBuses;
        public object lsItem;
        public DateTime resolved;

        public enum opCode
        {
            NORMAL = 0,
            MODE_MISMATCH,
            TELEM_FAILURE,
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
            resolved = DateTime.MinValue;
        }
    }

    public class AF3EndPoint : IComparable, IEquatable<AF3EndPoint>
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
        public AF3EndPoint(uint escIndex, float busVoltageA, float busVoltageB, 
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

            AF3EndPoint otherEndpoint = obj as AF3EndPoint;
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
            AF3EndPoint objAsPart = obj as AF3EndPoint;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(AF3EndPoint other)
        {
            if (other == null) return false;
            return other.esc_index == esc_index;
        }
    }
}