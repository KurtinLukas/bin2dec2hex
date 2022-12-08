using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ciselneSoustavy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int selectedIndex = 0;
        int convertIndex = 1;
        char[] hexCode = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = comboBox1.SelectedIndex;
            switch (selectedIndex)
            {
                case -1:
                    maskedTextBox1.Mask = "";
                    return;
                case 0: 
                    maskedTextBox1.Mask = "00000000.00000000.00000000.00000000"; break;
                case 1:
                    maskedTextBox1.Mask = "000.000.000.000"; break;
                case 2:
                    maskedTextBox1.Mask = "AA:AA:AA:AA"; break;
            }
            ConvertSystemUpdate();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            convertIndex = comboBox2.SelectedIndex;
            ConvertSystemUpdate();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            ConvertSystemUpdate();
        }

        private void ConvertSystemUpdate()
        {
            if (selectedIndex == convertIndex)
            {
                textBox1.Text = maskedTextBox1.Text.ToUpper().Replace(" ", "0");
                return;
            }
            string[] arr = maskedTextBox1.Text.ToUpper().Replace(" ", "0").Split(',', '.', ':');
            {
                int desiredLength = 0;
                switch (selectedIndex)
                {
                    case 0: desiredLength = 8; break;
                    case 1: desiredLength = 3; break;
                    case 2: desiredLength = 2; break;
                }
                for (int i = arr[3].Length; i < desiredLength; i++)
                    arr[3] = arr[3] + "0";
            }

            string output = "";
            foreach (string s in arr)
            {
                switch (selectedIndex)
                {
                    case 0:
                        if (!int.TryParse(s, out int bin))
                        { label2.Text = "= Error"; return; }
                        foreach (char c in s) if ("23456789".Contains(c))
                            { label2.Text = "= Error"; return; }

                        if (convertIndex == 1) //BIN -> DEC
                        {
                            output += ToDec(s, "bin") + ".";
                        }
                        else if (convertIndex == 2) //BIN -> HEX
                        {
                            output += ToHex(Convert.ToInt32(s), "bin") + ":";
                        }
                        break;

                    case 1:
                        if (!int.TryParse(s, out int dec))
                        { label2.Text = "= Error"; return; }
                        if (Convert.ToInt32(s) > 255)
                        { label2.Text = "= Error"; return; }

                        if (convertIndex == 0) // DEC -> BIN
                        {
                            output += ToBin(s, "dec") + ".";
                        }
                        else if (convertIndex == 2) //DEC -> HEX
                        {
                            output += ToHex(Convert.ToInt32(s), "dec") + ":";
                        }
                        break;

                    case 2:
                        if (!hexCode.Contains(s[0]) || !hexCode.Contains(s[1]))
                        { label2.Text = "= Error"; return; }

                        if (convertIndex == 0)
                        {
                            output += ToBin(s, "hex") + ".";
                        }
                        else if (convertIndex == 1)
                        {
                            output += ToDec(s, "hex") + ".";
                        }
                        break;
                }
            }
            textBox1.Text = output.Trim('.', ':');
        }

        private string ToBin(string num, string system)
        {
            string vys = "";
            if(system == "dec" && int.TryParse(num, out int dec))
            {
                for (int i = 8; i > 0; i--)
                {
                    vys = (dec % 2) + vys;
                    dec /= 2;
                    
                }
            }
            else if(system == "hex")
            {
                vys += ToBin(ToDec(num, "hex").ToString(), "dec");
            }
            return vys;
        }

        private int ToDec(string num, string system)
        {
            int vys = 0;
            if (system == "bin" && int.TryParse(num, out int bin))
            {
                for(int i = 0; i < 8; i++)
                {
                    vys += bin % 10 * Mocnina(2, i);
                    bin /= 10;
                }
            }
            else if(system == "hex")
            {
                for(int i = 0; i < 16; i++)
                {
                    if (hexCode[i] == num[0])
                        vys += i * 16;
                    if (hexCode[i] == num[1])
                        vys += i;
                }
            }
            return vys;
        }

        private string ToHex(int num, string system)
        {
            string vys = "";
            if(system == "bin")
            {
                vys += hexCode[ToDec(((num - (num % 10000)) / 10000).ToString(), "bin")];
                vys += hexCode[ToDec((num % 2000).ToString(), "bin")];
            }
            else
            {
                vys += hexCode[(num - (num % 16)) / 16];
                vys += hexCode[num % 16];
            }
            return vys;
        }

        private int Mocnina(int x, int e)
        {
            int abc = x;
            x = 1;
            for (int i = 0; i < e; i++)
                x *= abc;
            return x;
        }
    }
}
