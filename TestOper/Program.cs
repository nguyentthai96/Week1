using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOper
{
    class Program
    {
        static void Main(string[] args)
        {
            string oper = Console.ReadLine();
            Console.WriteLine(oper);
            
            double []S;
            S = new double [100];
            int j = 0;
            //S[0] = Convert.ToDouble(oper);
            //Console.WriteLine("================:::::::::::::::::::::::" + S[0]);

            Stack<string> S_temp;
            S_temp = new Stack<string>();
            string[] uuTien = { "*",":","+","-"};
            int[] doUuTien = { 1, 1, 0, 0 };


            string operand = "";
            int i = 0;
            while (i < oper.Length)
            {
                if ('0' <= oper[i] && oper[i] <= '9')
                    operand += oper[i++];
                else
                {
                    if (operand != "")
                    {
                        S[j++] = Convert.ToDouble(operand);
                        operand = "";
                    }
                    int temp = Array.IndexOf(uuTien, oper[i].ToString());
                    if (temp > -1)
                    {
                        if (S_temp.Count() == 0)
                        {
                            if (temp > -1) S_temp.Push(oper[i++].ToString());
                        }
                        else
                        {
                            while (true && S_temp.Count() != 0)
                            {
                                int temp2 = Array.IndexOf(uuTien, S_temp.Peek());
                                if (doUuTien[temp2] >= doUuTien[temp])
                                {
                                    // S[j++] = S_temp.Pop();
                                    j--;
                                    S[j - 1] = tinh(S[j - 1], S[j], S_temp.Pop());
                                }
                                else
                                    break;
                            }
                            S_temp.Push(oper[i++].ToString());
                        }
                    }
                    else i++;
                }
            }

            if (operand != "")
                S[j++] = Convert.ToDouble(operand);

            while(S_temp.Count()>0&&j>0)
            {
                    // S[j++] = S_temp.Pop();
                    j--;
                    S[j - 1] = tinh(S[j - 1], S[j], S_temp.Pop());   
            }

            Console.WriteLine("================:::::::::::::::::::::::"+S[0]);

        }

        static double tinh(double x, double y, string operators)
        {
            switch (operators)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case ":":
                    return x / y;
                default:
                    return 0;
            }
        }
    }
}


// 2+3*3-4+2       +  +  2  -  4  *  3  3  2
// 1+2-3*4+5:6+7     +  +  +  7  :  6  5  -  *  4  3  2  1
// 1:12*2+13-23+14    +  +  14  -  23  13  :  *  2  12  1