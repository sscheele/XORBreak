using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XOR_Break
{
    class XORTools
    {
        public static byte[] stringToBytes(string s)
        {
            byte[] retVal = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                retVal[i] = (byte)s[i];
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
                retVal += String.Format("{0:X}", b[i]);
            }
            return retVal;
        }
    }
}
