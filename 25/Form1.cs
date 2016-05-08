using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace _25
{

    public partial class Form1 : Form
    {

        int column = 5, row = 5, hight=5;
        int count = 0, countResull = 0;
        protected bool selecShape = true;
        public static Button[,] L = new Button[101, 101];
        public Form1()
        {
            InitializeComponent();
        }
        private void resetAllStatus()
        {
            button1.Enabled = true;
            button2.Enabled = false;
            radioButton1.Enabled = radioButton2.Enabled = true;
            radioButton1.AutoCheck = true;

            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = true;
            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
            button5.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (selecShape)
            {
                creatQuadrilateral();
                radioButton1.Checked = true;
            }
            else
            {
                button5.Hide();
                creatTriangle();
                radioButton2.Checked = true;
            }
        }
        private void creatQuadrilateral()
        {
            try
            {
                column = int.Parse(textBox1.Text);
                row = int.Parse(textBox2.Text);
                if (row > 100 || column > 100)
                {
                    resetAllStatus();
                    MessageBox.Show("Bạn nhập số lượng ít thôi chứ lượng sức mình đi, nhập lại dùm cái kẻo máy bạn cháy đấy!");
                }
                count = countResull = 0;
                textBox4.Text = countResull + "/" + row * column;
                textBox5.Text = count + "/" + row * column;
                for (int i = 0; i < column; i++)
                    for (int j = 0; j < row; j++)
                    {
                        L[i, j] = AddButtonDynamic(i, j);
                    }

                button1.Enabled = false;
                button2.Enabled = true;
                radioButton1.Enabled = radioButton2.Enabled = false;
                textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = false;
            }
            catch (System.IO.IOException)
            {
                resetAllStatus();
                MessageBox.Show("Bạn chưa nhập số cột và số dòng!");
            }
            catch (System.FormatException)
            {
                resetAllStatus();
                MessageBox.Show("Bạn phải nhập số cột và số dòng!");
            }
            button5.Show();
        }
        public Button AddButtonDynamic(int co, int ro)
        {
            Button txt = new Button();
            this.Controls.Add(txt);
            txt.Width = 50;
            txt.Height = 50;
            txt.Top = 400 - (row / 2 + row % 2) * 50 + ro * 50 + 50 * row / 10;
            txt.Left = 500 - (column / 2 + row % 2) * 50 + co * 50;
            txt.Text = "";// + this.cLeft.ToString();
            txt.BackColor = Color.AliceBlue;
            txt.Click += new EventHandler(b_Click);
            return txt;
        }
        private void b_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton == null) // just to be on the safe side
                return;
            count++;
            changeColorButton(clickedButton);
            if (countResull == row * column)
                MessageBox.Show("Chúc mừng bạn đã thắng cuộc !<->_<->!");
            else
                if (count >= row * column)
                MessageBox.Show("Bạn đã thua, hẹn lần sau thắng nhe, ấn Reset Color chơi lại!");
            else
            {
                textBox4.Text = countResull + "/" + row * column;
                textBox5.Text = count + "/" + row * column;
            }
        }
        private void creatTriangle()
        {
            try
            {
                hight = int.Parse(textBox3.Text);
                if (hight > 100)
                {
                    resetAllStatus();
                    MessageBox.Show("Bạn nhập số lượng ít thôi chứ lượng sức mình đi, nhập lại dùm cái kẻo máy bạn cháy đấy!");
                }
                label5.Hide();
                textBox4.Hide();
                button5.Hide();

                count =  0;  
                textBox5.Text = count + "/" + hight*hight;
                for (int i = 1; i <=hight; i++)
                    for (int j = 1; j < 2*i; j++)
                    {
                        L[i, hight-i +j] = AddButtonDynamicTriangle(i, j);
                    }

                button1.Enabled = false;
                button2.Enabled = true;
                radioButton1.Enabled = radioButton2.Enabled = false;
                textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = false;
            }
            catch (System.IO.IOException)
            {
                resetAllStatus();
                MessageBox.Show("Bạn chưa nhập số số dòng!");
            }
            catch (System.FormatException)
            {
                resetAllStatus();
                MessageBox.Show("Bạn phải nhập số dòng!");
            }
        }
        public Button AddButtonDynamicTriangle(int i, int j)
        {
            Button txt = new Button();
            this.Controls.Add(txt);
            txt.Width = 50;
            txt.Height = 50;
            txt.Top = 400 - hight*25 + i * 50;
            txt.Left = 500 - i*50+25 + (j-1) * 50+(hight/10)*50;
            txt.Text = "";// + this.cLeft.ToString();
            txt.BackColor = Color.AliceBlue;
            txt.Click += new EventHandler(b_Click2);
            return txt;
        }

        
        private void b_Click2(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton == null) // just to be on the safe side
                return;
            count++;
            changeColorButton2(clickedButton);
            if (countResull == hight*hight)
                MessageBox.Show("Chúc mừng bạn đã thắng cuộc !<->_<->!");
            else
                if (count >= hight * hight)
                MessageBox.Show("Bạn đã thua, hẹn lần sau thắng nhe, ấn Reset Color chơi lại!");
            else
            {
                textBox5.Text = count + "/" + hight * hight;
            }
        }
        private void changeColorButton2(Button clickedButton)
        {
            for (int i = 1; i <= hight; i++)
                for (int j = 1; j < 2 * i; j++)
                {
                    if (clickedButton == L[i, hight - i + j])
                    {
                        if (clickedButton.BackColor == Color.AliceBlue)
                        {
                            L[i, hight - i + j].BackColor = Color.Red;
                            countResull++;
                        }
                        else
                        {
                            L[i, hight - i + j].BackColor = Color.AliceBlue;
                            countResull--;
                        }
                        switch (count % 3)
                        {
                            case 0:
                                if (j > 1)
                                    if (L[i, hight - i + j - 1].BackColor == Color.AliceBlue)
                                    {
                                        L[i, hight - i + j - 1].BackColor = Color.Red;
                                        countResull++;
                                    }
                                    else
                                    {
                                        L[i, hight - i + j - 1].BackColor = Color.AliceBlue;
                                        countResull--;
                                    }
                                break;
                            case 1:
                                if (i > 1&&j>1&&j<i*2-1)
                                    if (L[i - 1, hight - i + j].BackColor == Color.AliceBlue)
                                    {
                                        L[i - 1, hight - i + j].BackColor = Color.Red;
                                        countResull++;
                                    }
                                    else
                                    {
                                        L[i - 1, hight - i + j].BackColor = Color.AliceBlue;
                                        countResull--;
                                    }
                                break;
                            case 2:

                                if (j + 1 < 2 * i)
                                    if (L[i, hight - i + j + 1].BackColor == Color.AliceBlue)
                                    {
                                        L[i, hight - i + j + 1].BackColor = Color.Red;
                                        countResull++;
                                    }
                                    else
                                    {
                                        L[i, hight - i + j + 1].BackColor = Color.AliceBlue;
                                        countResull--;
                                    }
                                break;
                        }                       
                        i = hight;
                        break;
                    }
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resetColorFill();
        }

        private void resetColorFill()
        {
            count = countResull = 0;
            if (selecShape)
            {
                for (int i = 0; i < column; i++)
                    for (int j = 0; j < row; j++)
                        L[i, j].BackColor = Color.AliceBlue;
                textBox4.Text = countResull + "/" + row * column;
            }
            else
                for (int i = 1; i <= hight; i++)
                    for (int j = 1; j < 2 * i; j++)
                        L[i, hight - i + j].BackColor = Color.AliceBlue;
           
            textBox5.Text = count + "/" + row * column;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            selecShape = false; //false hình tam giác
           // radioButton2.Checked  = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resetAllStatus();
            
            if (selecShape)
                for (int i = 0; i < column; i++)
                    for (int j = 0; j < row; j++)
                        Controls.Remove(L[i, j]);
            else
                for (int i = 1; i <= hight; i++)
                    for (int j = 1; j < 2 * i; j++)
                        Controls.Remove(L[i, hight - i + j]);
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            selecShape = true;
            //radioButton1.Checked = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) textBox2.Focus();
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control || e.Shift) solutionColor();
            ShowDialog();
        }
        private void solutionColor()
        {
            for (int i = 0; i < column; i++)
                for (int j = 0; j < row; j++)
                    L[i, j].BackColor = Color.Red;
             
            countResull = row * column;
            
            Random t=new Random();
            int tmp = t.Next(1, t.Next(row,(row+column)));
            count = row * column -tmp-5;
            while (tmp>0)
            {
                changeColorButton(L[t.Next(1, column), t.Next(1, column)]);
                    tmp--;
             }
            textBox4.Text = countResull + "/" + row * column;
            textBox5.Text = count + "/" + row * column;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            solutionColor();
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) button1.Focus();
        }
        private void changeColorButton(Button clickedButton)
        {
            for (int i = 0; i < column; i++)
                for (int j = 0; j < row; j++)
                {
                    if (clickedButton == L[i, j])
                    {
                        if (clickedButton.BackColor == Color.AliceBlue)
                        {
                            L[i, j].BackColor = Color.Red;
                            countResull++;
                        }
                        else
                        {
                            L[i, j].BackColor = Color.AliceBlue;
                            countResull--;
                        }
                        if (i + 1 < column)
                            if (L[i + 1, j].BackColor == Color.AliceBlue)
                            {
                                L[i + 1, j].BackColor = Color.Red;
                                countResull++;
                            }
                            else
                            {
                                L[i + 1, j].BackColor = Color.AliceBlue;
                                countResull--;
                            }
                        if (j + 1 < row)
                            if (L[i, j + 1].BackColor == Color.AliceBlue)
                            {
                                L[i, j + 1].BackColor = Color.Red;
                                countResull++;
                            }
                            else
                            {
                                L[i, j + 1].BackColor = Color.AliceBlue;
                                countResull--;
                            }
                        if (i > 0)
                            if (L[i - 1, j].BackColor == Color.AliceBlue)
                            {
                                L[i - 1, j].BackColor = Color.Red;
                                countResull++;
                            }
                            else
                            {
                                L[i - 1, j].BackColor = Color.AliceBlue;
                                countResull--;
                            }
                        if (j > 0)
                            if (L[i, j - 1].BackColor == Color.AliceBlue)
                            {
                                L[i, j - 1].BackColor = Color.Red;
                                countResull++;
                            }
                            else
                            {
                                L[i, j - 1].BackColor = Color.AliceBlue;
                                countResull--;
                            }
                        i = column;
                        break;
                    }
                }
        }
    }
}
