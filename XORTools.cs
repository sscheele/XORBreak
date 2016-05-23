using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XOR_Break
{
    class XORTools
    {
        static double[] letterFrequencies = { 0.0817, 0.0149, 0.0278, 0.0425, 0.127, 0.0223, 0.0202, 0.0609, 0.0697, 0.0015, 0.0077, 0.0403, 0.0241, 0.0675, 0.0751, 0.0193, 0.0009, 0.0599, 0.0633, 0.0906, 0.0276, 0.0098, 0.0236, 0.0015, 0.0197, 0.0007 };

        public static byte[] stringToBytes(string s)
        {
            byte[] retVal = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                retVal[i] = (byte)s[i];
            }
            return retVal;
        }

        //goal should be to minimize entropy
        public static double averageEntropy(byte[] s)
        {
            double retVal = 0;
            foreach(byte b in s)
            {
                byte temp = b;
                if (b < 91 && b > 64) temp -= 65;
                else if (b > 96 && b < 123) temp -= 97;
                retVal -= Math.Log(letterFrequencies[temp]) * letterFrequencies[temp]; //log and log base two are basically the same thing, right?
            }
            retVal /= (double)s.Length;
            return retVal;
        }

        public static string bytesToString(byte[] b)
        {
            string retVal = "";
            for (int i = 0; i < b.Length; i++)
            {
                retVal += (char)b[i];
            }
            return retVal;
        }

        public static byte[] hexToBytes(string s)
        {
            byte[] retVal = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
            {
                retVal[i / 2] = (byte)Convert.ToInt32(s.Substring(i, 2), 16);
            }
            return retVal;
        }

        public static string bytesToHex(byte[] b)
        {
            string retVal = "";
            for (int i = 0; i < b.Length; i++)
            {
                string s = string.Format("{0:X}", b[i]);
                if (s.Length == 1) s = "0" + s;
                retVal += s;
            }
            return retVal;
        }

        //note: wraps shorter array!
        public static byte[] xorByteArr(byte[] message, byte[] key)
        {
            byte[] retVal = new byte[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                retVal[i] = (byte)(message[i] ^ key[i % key.Length]);
            }
            return retVal;
        }
    }
}
