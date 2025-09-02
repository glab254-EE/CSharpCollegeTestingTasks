using System.Security.Cryptography;

namespace _25KT1_IntArrayList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    public class IntArrayList
    {
        private int buffer;
        private int[] Array;
        private int bufferCount
        {
            get
            {
                return buffer;
            }
        }
        private int defaultSize = 2;
        public int Count
        {
            get
            {
                return Array.Length;
            }
        }
        public int Capacity
        {
            get
            {
                return bufferCount;
            }
        }
        public IntArrayList()
        {
            buffer = defaultSize;
            Array = new int[buffer];
        }
        public IntArrayList(int bufferSize)
        {
            buffer = bufferSize;
            Array = new int[buffer];
        }
        public void PushBack(int value)
        {
            if (Array.Length + 1 > bufferCount)
            {
                buffer *= 2;
                int[] newArray = new int[buffer];
                for (int i = 0; i < Array.Length; i++)
                {
                    newArray[i] = Array[i];
                }
                Array = newArray;
            }
            Array[Array.Length - 1] = value;
        }
        public void PopBack()
        {
            if (Array.Length > 0)
            {
                Array[Array.Length - 1] = 0;
            }
        }
        public bool TryInsert(int index, int value)
        {
            bool success = false;
            try
            {
                if (index >= 0)
                {
                    if (index < Array.Length)
                    {
                        Array[index] = value;
                    }
                    else if (index == Array.Length)
                    {
                        PushBack(value);
                    }
                    else
                    {
                        return false;
                    }
                    success = true;
                }
            }
            catch
            {

            }
            return success;
        }
        public bool TryErase(int index)
        {
            bool success = false;
            try
            {
                if (index >= 0 && index < Array.Length)
                {
                    Array[index] = 0;
                    success = true;
                }
            }
            catch
            {

            }
            return success;
        }
        public bool TryGetAt(int index, out int output)
        {
            bool success = false;
            output = 0;
            try
            {
                if (index >= 0 && index < Array.Length)
                {
                    output = Array[index];
                    success = true;
                }
                else
                {
                    output = 0;
                }
            }
            catch
            {

            }
            return success;
        }
        public void Clear()
        {
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = 0;
            }
        }
        public bool TryForceCapacity(int capacity)
        {
            bool success = false;
            try
            {
                if (capacity > 0)
                {
                    if (capacity < buffer)
                    {
                        int[] newarray = new int[capacity];
                        for (int i = 0; i < capacity; i++)
                        {
                            newarray[i] = Array[i];
                        }
                        Array = newarray;
                        buffer = capacity;
                    }
                    else
                    {
                        buffer = capacity;
                    }
                }
            }
            catch
            {

            }
            return success;
        }
        public int Find()
        {
            int output = -1;
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] != 0)
                {
                    output = i;
                    break;
                }
            }
            return output;
        }
    }
}
