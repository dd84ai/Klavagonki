using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace WindowsFormsApp1
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label2.Text = "off";
            Interceptor.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "on";

            bool whiletrue = true;
            while (whiletrue)
            {
                string status = Button.ModifierKeys.ToString();

                string str = Button.IsKeyLocked(Keys.CapsLock).ToString();

                string str1 = "";
                
                if ((str1 = ModifierKeys.ToString())!="None")
                    Console.WriteLine(str1);

                if (IsKeyLocked(Keys.CapsLock))
                    Console.WriteLine("Caps");


                //Control.
            }
            label2.Text = "off";
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            KeyProcess(e);
        }
        void KeyProcess(KeyPressEventArgs e)
        {
            label1.Text = e.KeyChar.ToString() + "|" + (int)e.KeyChar;

            //if (!Button.IsKeyLocked(Keys.CapsLock))
            //{
            //    System.Windows.Forms.SendKeys.SendWait("123");
            //}
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //312label1.Text = e.KeyValue.ToString();
        }
    }
}
