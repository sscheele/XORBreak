﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOR_Break
{
    public partial class Form1 : Form
    {
        byte[] str1Arr;
        byte[] str2Arr;
        public Form1()
        {
            InitializeComponent();
        }

        private void updateStr1Val(object sender, EventArgs e)
        {
            if (radioButton2.Checked) str1Arr = XORTools.hexToBytes(textBox1.Text);
            else if (radioButton3.Checked) str1Arr = Convert.FromBase64String(textBox1.Text);
            else if (radioButton1.Checked) str1Arr = XORTools.stringToBytes(textBox1.Text);
        }

        private void updateStr2Val(object sender, EventArgs e)
        {
            if (radioButton5.Checked) str2Arr = XORTools.hexToBytes(textBox2.Text);
            else if (radioButton4.Checked) str2Arr = Convert.FromBase64String(textBox2.Text);
            else if (radioButton6.Checked) str2Arr = XORTools.stringToBytes(textBox2.Text);
        }

        private bool isHexString(string s)
        {
            Regex hexPattern = new Regex("^[0-9abcedf]+$");
            return hexPattern.IsMatch(s.ToLower());
        }

        private bool isBase64String(string s)
        {
            Regex base64Pattern = new Regex("^[a-zA-Z0-9\\=\\+\\/]+$");
            return base64Pattern.IsMatch(s) && s.Length % 4 == 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton1.Checked = true;
            if (textBox1.Text.Length % 4 == 0)
            {
                if (isBase64String(textBox1.Text)) radioButton3.Checked = true;
            }
            else radioButton3.Enabled = false;
            if (textBox1.Text.Length % 2 == 0)
            {
                if (isHexString(textBox1.Text)) radioButton2.Checked = true;
            }
            else radioButton2.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //radio5 - hex, radio4 - base64, radio6 - ascii
            radioButton4.Enabled = true;
            radioButton5.Enabled = true;
            radioButton6.Checked = true;
            if (textBox2.Text.Length % 4 == 0)
            {
                if (isBase64String(textBox2.Text)) radioButton4.Checked = true;
            }
            else radioButton4.Enabled = false;
            if (textBox2.Text.Length % 2 == 0)
            {
                if (isHexString(textBox2.Text)) radioButton5.Checked = true;
            }
            else radioButton5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text += "XOR of strings is: " + XORTools.bytesToHex(XORTools.xorByteArr(str1Arr, str2Arr)) + "\r\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<int, double> keyLengthFitness = new Dictionary<int, double>();
            keyLengthFitness.Add(str1Arr.Length, 0);
            for (int keyLen = 1; keyLen < str1Arr.Length; keyLen++)
            {
                double equalCharCount = 0;
                for (int offset = 0; offset < keyLen; offset++)
                {
                    Dictionary<byte, int> charCount = new Dictionary<byte, int>();
                    for (int charPos = offset; charPos < str1Arr.Length; charPos += keyLen)
                    {
                        if (!charCount.ContainsKey(str1Arr[charPos])) charCount.Add(str1Arr[charPos], 1);
                        else charCount[str1Arr[charPos]]++;
                    }
                    equalCharCount += getMax(charCount.Values.ToList());
                }
                keyLengthFitness.Add(keyLen, equalCharCount / (str1Arr.Length + Math.Pow(keyLen, 1.5)));
            }
            var sortedFitnesses = keyLengthFitness.OrderBy(x => -x.Value);
            int i = 0;
            textBox3.Text += "Most probable key lengths are:\r\n";
            foreach(KeyValuePair<int, double> v in sortedFitnesses)
            {
                i++;
                textBox3.Text += v.Key.ToString() + ": " + v.Value.ToString() + "\r\n";
                if (i == 10) break;
            }
        }

        private int getMax(List<int> inArr)
        {
            int retVal = inArr[0];
            for (int i = 0; i < inArr.Count; i++)
            {
                if (inArr[i] > retVal) retVal = inArr[i];
            }
            return retVal;
        }
    }
}
