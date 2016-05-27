using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOR_Break
{
    public partial class Form2 : Form
    {
        private byte[] ciphertext1, ciphertext2, plaintext1, plaintext2, key;

        public Form2()
        {
            InitializeComponent();
            textBox4.AppendText("Note: the asterisk (*) serves as the wildcard character for the purposes of your crib in both ASCII and hex mode. In hex mode, two asterisks should be used to represent a \"wildcard byte.\" To add a normal asterisk in text mode, simply escape it (\\*).");
            textBox4.AppendText("Wildcards will cause an underscore to be printed in ASCII mode and a zero byte (00) to be printed in hex mode.");
            ciphertext1 = new byte[0];
            ciphertext2 = new byte[0];
            plaintext1 = new byte[0];
            plaintext2 = new byte[0];
            key = new byte[0];
        }



        private void textBox1_TextChanged(object sender, EventArgs e) //string 1 tb
        {
            radioButton1.Enabled = true; //hex
            radioButton3.Enabled = true; //base64
            radioButton2.Checked = true; //ASCII

            if (XORTools.isBase64String(textBox1.Text)) radioButton3.Checked = true;
            else radioButton3.Enabled = false;

            if (XORTools.isHexString(textBox1.Text)) radioButton1.Checked = true;
            else radioButton1.Enabled = false;

            if (radioButton1.Checked) ciphertext1 = XORTools.hexToBytes(textBox1.Text);
            else if (radioButton3.Checked) ciphertext1 = Convert.FromBase64String(textBox1.Text);
            else ciphertext1 = XORTools.stringToBytes(textBox1.Text);

            if (ciphertext1.Length > ciphertext2.Length && ciphertext1.Length != key.Length)
            {
                //resize key
                byte[] temp = new byte[ciphertext1.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    temp[i] = key[i];
                }
                key = temp;
            }

            if (plaintext1.Length != ciphertext1.Length)
            {
                //resize plaintext 1
                byte[] temp = plaintext1;
                for (int i = 0; i < plaintext1.Length; i++)
                {
                    temp[i] = plaintext1[i];
                }
                plaintext1 = temp;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) //string 2 tb
        {
            radioButton4.Enabled = true; //base64
            radioButton6.Enabled = true; //hex
            radioButton5.Checked = true;

            if (XORTools.isBase64String(textBox2.Text)) radioButton4.Checked = true;
            else radioButton4.Enabled = false;

            if (XORTools.isHexString(textBox2.Text)) radioButton6.Checked = true;
            else radioButton6.Enabled = false;

            if (radioButton6.Checked) ciphertext2 = XORTools.hexToBytes(textBox2.Text);
            else if (radioButton4.Checked) ciphertext2 = Convert.FromBase64String(textBox2.Text);
            else ciphertext2 = XORTools.stringToBytes(textBox2.Text);


            if (ciphertext2.Length > ciphertext1.Length && ciphertext2.Length != key.Length)
            {
                //resize key
                byte[] temp = new byte[ciphertext2.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    temp[i] = key[i];
                }
                key = temp;
            }

            if (plaintext2.Length != ciphertext2.Length)
            {
                //resize plaintext 2
                byte[] temp = plaintext2;
                for (int i = 0; i < plaintext2.Length; i++)
                {
                    temp[i] = plaintext2[i];
                }
                plaintext2 = temp;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e) //crib tb
        {

        }
    }
}
