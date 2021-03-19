using System;
using System.Collections.Generic;

namespace MissionPlanner.Utilities
{
    public class AF3Status
    {
        public float score { get; set; }
        public bool[] telemRFC = new bool[] { false, false, false };
 
        public float activeRFC { get; set; }
        private List<AF3EndPoint> endpointCollection = new List<AF3EndPoint>();
        private object epCollectionLock = new object();

        public float calculateAf3Score()
        {
            // Check how many telemetry links are lost between RFC's and VFC
            int countTelem = 0;
            for (int i = 0; i < telemRFC.Length; i++)
            {
                countTelem += (!telemRFC[i] ? 1 : 0);
            }

            // Check if any of the endpoints became unresponsive
            lock (epCollectionLock)
            {
                foreach (var ep in endpointCollection)
                {
                    countTelem += (ep.isDataStale() ? 1 : 0);
                }
            }

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
                    (index >= 0))
                {
                    AF3EndPoint epItem = endpointCollection[index];

                    AF3EndPoint epRetItem= new AF3EndPoint(epItem.esc_index,
                        epItem.voltageA, epItem.voltageB, epItem.rpm, epItem.lastUpdate);

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
        public int rpm;
        public DateTime lastUpdate;
        private const int maxExpectedPeriod = 5000;
        public AF3EndPoint(uint escIndex, float busVoltageA, float busVoltageB, int engRPM, DateTime timestamp)
        {
            esc_index = escIndex;
            voltageA = busVoltageA;
            voltageB = busVoltageB;
            lastUpdate = timestamp;
            rpm = engRPM;
        }

        public bool isDataStale()
        {
            return (DateTime.Now.Subtract(lastUpdate).TotalMilliseconds > maxExpectedPeriod);
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