using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        int statusPreviousPressButton=0; // number 1, operator 2, DEL 3, AC 4, Ans 5, = 6
        int numberButtonOperator = 0;
        TextBox txbTemp = new TextBox();
        TextBox txbPrime = new TextBox();
        Label lblParenthesis=new Label();
        int countParenthesis = 0;
        public string[] sbutton = new string[1000];
        int ibutton = 0;
        double ans=0;
        CCalculate c;
        public Form1()
        {
            InitializeComponent();
            Controls.Add(lblParenthesis);
            ButtonBorard();
            
            this.Controls.Add(txbTemp);
            txbTemp.BackColor = SystemColors.ButtonFace;
            txbTemp.BorderStyle = BorderStyle.None;
            txbTemp.Font = new Font("", 12F, FontStyle.Regular);
            txbTemp.ForeColor = SystemColors.ControlDark;
            txbTemp.Location = new Point(10, 20);
            txbTemp.Size = new System.Drawing.Size(250, 30);

            this.Controls.Add(txbPrime);
            txbPrime.BackColor = SystemColors.ButtonFace;
            txbPrime.BorderStyle = BorderStyle.None;
            txbPrime.Font = new Font("", 25F, FontStyle.Bold);
            txbPrime.ForeColor = SystemColors.InfoText;
            txbPrime.Location = new Point(10, 45);
            txbPrime.Size = new Size(250, 120);
            txbPrime.TextAlign= HorizontalAlignment.Right;
            txbPrime.Text = "0";
            txbTemp.Enabled = false;
        }
        public Button CreatButton(string name, int y, int x)
        {
            Button btn = new Button();
            this.Controls.Add(btn);
            btn.Name = name;
            btn.Text = name;
            btn.Font = new Font("", 10F, FontStyle.Bold);
            btn.Location = new Point(x, y);
            btn.Height = 50;
            btn.Width = 50;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
        public void ButtonBorard()
        {       
            string[] numberButton = { "7", "8", "9", "4", "5", "6", "1", "2", "3", "0", ".", "(-)" };
            for (int i = 0; i < 12; i++) 
                CreatButton(numberButton[i], 150 + i /3*50, 10+(i % 3)*50).Click += new EventHandler(number_Click);
           
            string[] operatorBasicButton = { "*", "/", "+", "-" };
            for (int i = 0; i <4; i++)
                CreatButton(operatorBasicButton[i], 200 + (i /2)*50, 160+(i % 2)*50).Click += new EventHandler(operatorBasic_Click);
            CreatButton("DEL", 150, 160).Click += new EventHandler(DEL_Click);
            CreatButton("AC", 150, 210).Click += new EventHandler(AC_Click);
            CreatButton("Ans", 300, 160).Click += new EventHandler(Ans_Click);
            CreatButton("=", 300, 210).Click += new EventHandler(Result_Click);
           
            string []operatorSpecial = { "(", ")", "^", "%", "!" };
            for (int i = 0; i < 5; i++)
                CreatButton(operatorSpecial[i], 100 + (i / 5) * 50, 10 + (i % 5) * 50).Click += new EventHandler(operatorBasic_Click);

            
            lblParenthesis.Location = new Point(40, 100);
            lblParenthesis.Width = 20;
            lblParenthesis.Height = 20;
            lblParenthesis.FlatStyle = FlatStyle.Flat;
            lblParenthesis.BackColor = Color.Transparent;
            lblParenthesis.Hide();
        }

        private void number_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string nameButton = clickedButton.Name;
            if (nameButton == "(-)" && ((ibutton > 0 && ('0' <= sbutton[ibutton - 1][0] && sbutton[ibutton - 1][0] <= '9')) 
                || sbutton[ibutton - 1] == "." || sbutton[ibutton - 1] == "(-)"))
                MessageBox.Show("Bạn nạp sai công thức toán \n(-) chưa đúng vị trí");
            else
                if (nameButton == "." && ibutton > 0 && sbutton[ibutton - 1] == ".")
                MessageBox.Show("Đây không phải là một số, dư '.' ");
            else
            {
                sbutton[ibutton++] = nameButton;
                updateStatus_txbPrime();
                if (numberButtonOperator > 1) updateStatus_txbTemp();
            }
            statusPreviousPressButton = 1;
        }
        private void operatorBasic_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string nameButton = clickedButton.Name;
            if (nameButton == "(")
            {
                countParenthesis++;
                lblParenthesis.Text = countParenthesis.ToString();
                lblParenthesis.Show();
                if (ibutton >0&&!sbutton[ibutton - 1].isOperator())
                    sbutton[ibutton++] = "*";
                sbutton[ibutton++] = nameButton;
                updateStatus_txbTemp();
                statusPreviousPressButton = 2;
            }
            else
            if (nameButton == ")")
            {
                countParenthesis--;
                if (countParenthesis == 0) 
                lblParenthesis.Hide();
                lblParenthesis.Text = countParenthesis.ToString();
                sbutton[ibutton++] = nameButton;
                updateStatus_txbTemp();
                statusPreviousPressButton = 2;
            }
            else
            if (statusPreviousPressButton == 2 && (nameButton != "(" && nameButton != ")" && nameButton != "!")
                && (ibutton>0&& sbutton[ibutton - 1] != "(" && sbutton[ibutton - 1] != ")" && sbutton[ibutton - 1] != "!")) 
                MessageBox.Show("Bạn ấn dư toán tử:" + nameButton + " và " + sbutton[ibutton - 1]);
            else
            {
                if (++numberButtonOperator > 1&& countParenthesis == 0)//(sbutton.Count(oper => oper.isOperator()) > 1)
                {
                    c = new CCalculate(sbutton);
                    updateStatus_txbPrime(c.ReversePolishNotationAndResult().ToString());
                }
                sbutton[ibutton++] = nameButton;
                updateStatus_txbTemp();
                statusPreviousPressButton = 2;
            }
        }
        private void DEL_Click(object sender, EventArgs e)
        {
            if (ibutton>0) ibutton--;
            if (sbutton[ibutton] == "(")
                countParenthesis--;
            else
                if (sbutton[ibutton] == ")")
                countParenthesis++;
            sbutton[ibutton] = null;
            updateStatus_txbPrime();
            if (txbTemp.Text!="")
                updateStatus_txbTemp();
            statusPreviousPressButton = 3;
        }
        private void AC_Click(object sender, EventArgs e)
        {
            sbutton = new string[1000];
            ibutton = 0;
            numberButtonOperator = 0;
            updateStatus_txbPrime("0");
            updateStatus_txbTemp();
            statusPreviousPressButton = 4;
        }
        private void Ans_Click(object sender, EventArgs e)
        {
            string s_temp = ans.ToString();
            int l=s_temp.Length;
            for (int k=0; k<l; k++)
            sbutton[ibutton++]=s_temp[k].ToString();
            statusPreviousPressButton = 5;
        }
        private void Result_Click(object sender, EventArgs e)
        {
            if (statusPreviousPressButton==2&&!sbutton [ibutton -1].isOperatorUnaryPostfix())
            {
                MessageBox.Show("Dư toán tử, bạn phải xóa toán tử cuối mới có thể tính");
                return;
            }
                c = new CCalculate(sbutton);
            if (countParenthesis != 0)
            {
                updateStatus_txbPrime("Dấu ngoặc sai ( / )");
                return;
            }
            ans = c.ReversePolishNotationAndResult();
            updateStatus_txbTemp();
            updateStatus_txbPrime(ans.ToString());
            statusPreviousPressButton = 6;
        }
        private void updateStatus_txbTemp()
        {
            txbTemp.Text = "";
            for (int i = 0; i < ibutton; i++)
                txbTemp.Text += sbutton[i];
            if (statusPreviousPressButton != 3)
                txbTemp.Select(0, txbTemp.TextLength);
        }
        private void updateStatus_txbPrime()
        {
            txbPrime.Text = "";
            int i = ibutton-1;
            while (i>-1&& sbutton[i].isOperator()==sbutton[ibutton-1].isOperator())//&& ibutton -i<22)
            {
                txbPrime.Text = sbutton[i] + txbPrime.Text;
                i--;
            }
                txbPrime.Select(txbPrime.TextLength + 1, 0);
        }
        private void updateStatus_txbPrime(string s)
        {
                txbPrime.Text = s;
                txbPrime.Select(txbPrime.TextLength + 1, 0);
            return;
        }
    }
}
