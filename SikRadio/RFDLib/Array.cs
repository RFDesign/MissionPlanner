using System;


namespace RFDLib
{
    public static class Array
    {
        public static T[] CherryPickArray<U, T>(U[] x, Func<U, T> PickFn)
        {
            T[] Result = new T[x.Length];
            for (int n = 0; n < x.Length; n++)
            {
                Result[n] = PickFn(x[n]);
            }
            return Result;
        }
    }
    public class TDeserialiser
    {
        byte[] _Array;
        int _Index = 0;

        public TDeserialiser(byte[] Array)
        {
            _Array = Array;
        }

        T Get<T>(Func<byte[], int, T> Converter, int Size)
        {
            T Result = Converter(_Array, _Index);
            _Index += Size;
            return Result;
        }

        public void Skip(int QTYBytes)
        {
            _Index += QTYBytes;
        }

        public ulong GetULong()
        {
            return Get(BitConverter.ToUInt64, sizeof(ulong));
        }

        public int GetInt()
        {
            return Get(BitConverter.ToInt32, sizeof(int));
        }

        public uint GetUInt()
        {
            return Get(BitConverter.ToUInt32, sizeof(uint));
        }

        public ushort GetUShort()
        {
            return Get(BitConverter.ToUInt16, sizeof(ushort));
        }

        public byte GetByte()
        {
            return Get((b, n) => b[n], 1);
        }
    }
}
