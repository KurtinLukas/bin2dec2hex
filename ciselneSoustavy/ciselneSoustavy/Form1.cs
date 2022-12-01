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
        char[] hexCode = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            char[] text = textBox1.Text.ToCharArray();
            switch (selectedIndex)
            {
                case 0:
                    break;
            }
            text[textBox1.Text.IndexOf("_")] = (char)e.KeyValue;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = comboBox1.SelectedIndex;
            switch (selectedIndex)
            {
                case -1:
                    textBox1.Text = "";
                    return;
                case 0:
                    textBox1.Text = "________.________.________.________";
                    break;
                case 1:
                    textBox1.Text = "___.___.___.___";
                    break;
                case 2:
                    textBox1.Text = "__:__:__:__";
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
