using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static bool isNumber(this string operand)
        {
            try
            {
                double try_temp = Convert.ToDouble(operand);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }
        public static bool isOperatorUnaryPostfix(this string s)
        {
            string[] uuTien = { ")","!"};
            return (Array.IndexOf(uuTien, s) > -1);
        }
        public static bool isOperator(this string s)
        {
            string[] uuTien = { ")", "^", "!", "%", "*", "/", "+", "-", "(" };
            return (Array.IndexOf(uuTien, s) > -1);
        }
        public static int operatorPriority(this string s)
        {
            string[] uuTien = {"(", "^","!", "%", "*", "/", "+", "-" ,")"};
            int[] doUuTien = { 10, 9, 8, 7, 6, 6, 5, 5 ,0};
            int i = Array.IndexOf(uuTien, s);
            if (i > -1)
                return doUuTien[i];
            else
                return -1;
        }
    }
}
