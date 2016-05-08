using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PuzzleImage
{
    public partial class Form1 : Form
    {
        int row=2, column=3;
        int imageWidth = 200, imageHeight = 200;

        int iImageNull, jImageNull;
        Bitmap [,]bitmapArr;
        PictureBox [,]pictureBoxArr;
        PictureBox pictureBoxSample;
        int countClick = 0;
        Label lblCountClick;
        Label lblCountClickShow;

        public Form1()
        {
            InitializeComponent();
            lblCountClick = new Label();
            lblCountClick.Text = "Số lần Click:";
            lblCountClick.Font = new Font("",13);
            lblCountClick.Width = 80;
            lblCountClick.Location = new Point(850, 170);
            this.Controls.Add(lblCountClick);

            lblCountClickShow = new Label();
            lblCountClickShow.Text = countClick.ToString();
            lblCountClickShow.Location = new Point(930, 170);
            lblCountClickShow.Font = new Font("", 13);
            lblCountClickShow.Width = 150;
            this.Controls.Add(lblCountClickShow);

            pictureBoxSample = new PictureBox();
            pictureBoxSample.Height = 450;
            pictureBoxSample.Width = 450;
            pictureBoxSample.Location = new Point(820,270);
            pictureBoxSample.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxSample.Click += new EventHandler(pictureBoxSample_Click);
            pictureBoxSample.DoubleClick += new EventHandler(pictureBoxSample_DoubleClick);
            this.Controls.Add(pictureBoxSample);
        }

        private void pictureBoxSample_DoubleClick(object sender, EventArgs e)
        {
            OpenFileImage();
            CropImage();
            creatPictureBox();
            RandomImage();
            countClick = 0;
            lblCountClickShow.Text = countClick.ToString();
        }

        private void pictureBoxSample_Click(object sender, EventArgs e)
        {
            RandomImage();
            countClick = 0;
            lblCountClickShow.Text = countClick.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileImage();
            CropImage();
            creatPictureBox();
          //  RandomImage();
        }
        private void OpenFileImage()
        {
            // OpenFileDialog ofd = new OpenFileDialog();
            if (true)//ofd.ShowDialog() == DialogResult.OK)
                try
                {
                    pictureBoxSample.Image = Image.FromFile(@"D:\Desktop\muc-luong-lap-trinh-vien.jpg"); // ofd.FileName);
                    if (false) ;//ofd.FileName == "")
                                // MessageBox.Show("Bạn quên mở file ảnh đẹp của bạn, hay là bạn mở thiếu ảnh rồi!");
                }
                catch (System.OutOfMemoryException)
                {
                    MessageBox.Show("Bạn phải mở file ảnh!");
                }
        }
        private void CropImage()
        {
            Bitmap bitmapPictureSamp = new Bitmap(pictureBoxSample.Image, new Size( imageWidth * column, imageHeight * row));
            bitmapArr = new Bitmap[row, column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    bitmapArr[i, j] = new Bitmap(imageWidth, imageHeight);
                    bitmapArr[i, j] = bitmapPictureSamp.Clone(new Rectangle( j* imageWidth, i * imageHeight, imageWidth, imageHeight), bitmapPictureSamp.PixelFormat);
                }
            }
            bitmapPictureSamp.Dispose();
        }


       private void creatPictureBox()
        {
            pictureBoxArr = new PictureBox[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    pictureBoxArr[i, j] = new PictureBox();
                    pictureBoxArr[i, j].Name = "" + i + "+" + j;
                    pictureBoxArr[i, j].Height = imageHeight;
                    pictureBoxArr[i, j].Width = imageWidth;
                    pictureBoxArr[i, j].Location = new Point( j * imageWidth + 50, i * imageHeight + 50);
                    pictureBoxArr[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBoxArr[i, j].Image = bitmapArr[i, j];

                    this.Controls.Add(pictureBoxArr[i, j]);
                    pictureBoxArr[i, j].Click += new EventHandler(pictureBoxArr_Click);
                }
            }
        }

        
        private void RandomImage()
        {
            Random r = new Random();

            int loop = r.Next(row<column?row:column, row * column);

            while (loop>0)
            {
                swapPictureBox(pictureBoxArr[r.Next(row), r.Next(column)], pictureBoxArr[r.Next(row), r.Next(column)]);
                loop--;
            }

            iImageNull = r.Next(row);
            jImageNull = r.Next(column);
            pictureBoxArr[iImageNull, jImageNull].Image = null;
            pictureBoxArr[iImageNull, jImageNull].BorderStyle = BorderStyle.Fixed3D;
        }

        private void swapPictureBox(PictureBox x, PictureBox y)
        {
            Image tempImage;
            tempImage = x.Image;
            x.Image = y.Image;
            y.Image = tempImage;
            string tempName = x.Name;
            x.Name = y.Name;
            y.Name = tempName;
        }
        private void pictureBoxArr_Click(object sender, EventArgs e)
        {
           PictureBox pictureBoxClicked= sender as PictureBox;
            Point p=pictureBoxClicked.Location;
            int i = (p.X - 50) / imageHeight, j = (p.Y - 50) / imageWidth;
            if (i-1==iImageNull&&j==jImageNull|| i + 1 == iImageNull && j == jImageNull
                || i == iImageNull && j-1 == jImageNull|| i == iImageNull && j + 1 == jImageNull)
            {
                swapPictureBox(pictureBoxArr[i, j], pictureBoxArr[iImageNull, jImageNull]);
                iImageNull = i;
                jImageNull = j;
                counClick++;
                lblCountClickShow.Text = countClick.ToString();
                if (checkSucceed())
                    pictureBoxArr[iImageNull, jImageNull].Image = bitmapArr[iImageNull, jImageNull];
            }
        }
        private bool checkSucceed()
        {
            for (int i=0; i< row; i++)
            {
                for (int j=0; j< column; j++)
                {
                    if (pictureBoxArr[i,j].Name!=(i+"+"+"j"))
                        return false;
                }
            }
            return true;
        }
    }
}