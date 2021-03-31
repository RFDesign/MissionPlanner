using System;
using System.Collections.Generic;

namespace MissionPlanner.Utilities
{
    public class AF3Status
    {
        public float score { get; set; }
        public int number_rfcs { get; set; }
        public bool[] telemRFC = new bool[] { false, false, false };
 
        public float activeRFC { get; set; }
        private List<AF3EndPoint> endpointCollection = new List<AF3EndPoint>();
        private object epCollectionLock = new object();

        public float calculateAf3Score()
        {
            // Check how many telemetry links are lost between RFC's and VFC
            int countTelem = 0;

            for (int i = 0; i < number_rfcs; i++)
            {
                countTelem += (!telemRFC[i] ? 1 : 0);
            }

            // Check if any of the endpoints becomes unresponsive
            // or disconnected from a bus
            lock (epCollectionLock)
            {
                foreach (var ep in endpointCollection)
                {
                    bool stale = ep.isDataStale();
                    int missing = ep.isBusMissing();

                    if (stale || (missing > 0))
                    {
                        Console.WriteLine("Endpoint %d: stale %s missing %d",
                            ep.esc_index, stale ? "yes" : "no", missing);
                        countTelem += 1;
                    }
                }
            }

            if (countTelem > 0)
                Console.WriteLine("AF3 presents at least one issue");

            return (float) countTelem;
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
                        epItem.rpm, epItem.elapsedSecBus0, epItem.elapsedSecBus1,
                        epItem.elapsedSecBus2, epItem.lastUpdate, number_rfcs);

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
        public byte elapsedSecBus0;
        public byte elapsedSecBus1;
        public byte elapsedSecBus2;
        public int numRFCs;

        private const int maxPeriodTotal = 6200; 
        private const int maxPeriodSameBus = 3000;
        public AF3EndPoint(uint escIndex, float busVoltageA, float busVoltageB, 
            float busCurrA, float busCurrB, int engRPM, byte bus0Elapsed, 
            byte bus1Elapsed, byte bus2Elapsed, DateTime timestamp, int numRFCS)
        {
            esc_index = escIndex;
            voltageA = busVoltageA;
            voltageB = busVoltageB;
            currentA = busCurrA;
            currentB = busCurrB;
            lastUpdate = timestamp;
            rpm = engRPM;
            elapsedSecBus0 = bus0Elapsed;
            elapsedSecBus1 = bus1Elapsed;
            elapsedSecBus2 = bus2Elapsed;
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
            result += ((int)elapsedSecBus0*1000 > maxPeriodSameBus) ? 1 : 0;
            if (numRFCs > 1)
            {
                result += ((int)elapsedSecBus1*1000 > maxPeriodSameBus) ? 2 : 0;
            }
            if (numRFCs > 2)
            {
                result += ((int)elapsedSecBus2*1000 > maxPeriodSameBus) ? 4 : 0;
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