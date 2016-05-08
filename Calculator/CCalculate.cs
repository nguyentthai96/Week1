using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft;
namespace Calculator
{
    public class CCalculate
    {
        public string []sbutton;
       
        public CCalculate(string []sbutton)
        {
            this.sbutton = sbutton;
        }
        public double ReversePolishNotationAndResult()
        {
            double[] S;
            S = new double[30];
            int j = 0; //chi so mang ket qua S

            Stack<string> S_temp;
            S_temp = new Stack<string>();

            string operand = "";
            int i = 0;
            int lengthSbutton = sbutton.Count(sbutton => sbutton != null);
            while (i < lengthSbutton)
            {
                if (sbutton[i].isNumber())//('0' <= sbutton[i][0] && sbutton[i][0] <= '9' || sbutton[i] == ".")
                    operand += sbutton[i++];
                else
                if (sbutton[i] == "(-)")
                {
                    if (operand == "")
                    {
                        operand += "-";
                        i++;
                    }
                    else
                        throw new NotImplementedException("Bạn nạp sai công thức toán \n(-) chưa đúng vị tri");///Lỗi không thể tính ra kết quả chương trình sẽ dừng lại. Lỗi nhập phép tính sai   
                }
                else
                {
                    if (operand != "")
                    {
                        S[j++] = Convert.ToDouble(operand);
                        operand = "";
                    }
                    if (sbutton[i] == "(")
                    {
                        int k=++i;
                        int countPe=0;
                        string []ss=new string[50];
                        int iss = 0;
                        if (sbutton[k] == "(")
                            countPe++;
                        if (sbutton[k] == ")")
                            countPe--;
                        while (sbutton[k]!=")"||countPe>0)
                        {
                            ss[iss++] = sbutton[k];
                            k++;

                            if (k < lengthSbutton)
                            {
                                if (sbutton[k] == "(")
                                    countPe++;
                                if (sbutton[k] == ")")
                                    countPe--;
                            }
                            else break;
                        }
                        if (k - i > 0)
                        {
                            CCalculate c = new CCalculate(ss);
                            S[j++] = c.ReversePolishNotationAndResult();
                            i = k + 1;
                        }
                        else
                            i++;
                    }
                    else
                    {
                        int temp = sbutton[i].operatorPriority();
                        if (temp > -1)
                        {
                            if (S_temp.Count() == 0)
                            {
                                S_temp.Push(sbutton[i++]);
                            }
                            else
                            {
                                while (true && S_temp.Count() != 0)
                                {
                                    int temp2 = S_temp.Peek().operatorPriority();
                                    if (temp2 >= temp)
                                    {
                                        // S[j++] = S_temp.Pop();
                                        j--;
                                        try
                                        {
                                            if (S_temp.Peek().isOperatorUnaryPostfix())
                                            {
                                                S[j] = tinh(S[j], 0, S_temp.Pop());
                                                j++;
                                            }
                                            else
                                            {
                                                string oper_temp = S_temp.Pop();
                                                S[j - 1] = tinh(S[j - 1], S[j], oper_temp);
                                            }
                                        }
                                        catch (IndexOutOfRangeException e)
                                        {
                                            throw new KeyNotFoundException(e.Message + "\nCó lẽ do bạn dư toán tử.");
                                        }
                                    }
                                    else
                                        break;
                                }
                                S_temp.Push(sbutton[i++]);
                            }
                        }
                        else i++;
                    }
                }
            }

            if (operand != "")
                try
                {
                    S[j++] = Convert.ToDouble(operand);
                }
                catch (FormatException)
                {
                    throw new FormatException("Có một toán hạng sai quy tắc, nó phải là một số.");
                }
            while (S_temp.Count() > 0 && j > 0)
            {
                // S[j++] = S_temp.Pop();
                j--;
                if (S_temp.Peek() == "!")
                {
                    S[j] = tinh(S[j], 0, S_temp.Pop());
                    j++;
                }
                else
                {
                    string cc = S_temp.Pop();
                    S[j - 1] = tinh(S[j - 1], S[j], cc);
                }
            }
            return S[0];
        }

        private double tinh(double x, double y, string operators)
        {
            switch (operators)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                case "^":
                    return Math.Pow(x, y);
                case "!":
                    {
                        long result=1, temp;
                        temp = (long)x;
                        if (temp >= x)
                            for (long i = temp; i > 0; i--)
                                result *= i;
                        else
                            throw new EntryPointNotFoundException("Không thể tính giai thừa được, phép tính sai!");
                        return result;
                    }
                case "%":
                    return x % y;
                default:
                    return 0;
            }
        }
    }
}
