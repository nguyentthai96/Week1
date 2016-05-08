using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ProjectImage
{   
    public partial class Form1 : Form
    {
        int range = 4;
        int size = 150;
        int height, with;

        Image imageSample;
        Bitmap bitmapImageSample;
        public PictureBox[,] imagesArray = new PictureBox[5, 5];
        public Form1()
        {
            InitializeComponent();
          //  pictureBoxSample_DoubleClick();
        }
              
        private void pictureBoxSample_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void addRandom()
        {

            bitmapImageSample = imageSample as Bitmap;
            height = bitmapImageSample.Height;
            with = bitmapImageSample.Width;
            if (bitmapImageSample == null)
                throw new ArgumentException("No valid bitmap");

            for (int i = 0; i < range; i++)
                for (int j = 0; j < range; j++)
                {
                    imagesArray[i, j] = initializationImage(i, j);
                    // imageSample= pictureBoxSample.Image;
                }
        }

        private PictureBox initializationImage(int i, int j)
        {
            PictureBox pictureBox = new PictureBox();
            Controls.Add(pictureBox);
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
            pictureBox.Location = new Point(70 + i * size, 70 + j * size);
            pictureBox.Name = i + "+" + j;
            pictureBox.Size = new Size(size, size);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Click += new EventHandler(pictureBox_Click);
           
            
            Rectangle selection = new Rectangle(i * height/range, j * with/range, height / range, with / range);
            Bitmap cropBmp = bitmapImageSample.Clone(selection, bitmapImageSample.PixelFormat);
            pictureBox.Image = cropBmp.Graphics.DrawImage(cloneBitmap, 0, 0);
            imageSample.Dispose();
         
            return pictureBox;
        }

        private void pictureBoxSample_Click(object sender, EventArgs e)
        {
            //if (d.open == true)
            //    openImage();
            //else
            //{
            //    removeImage();
            //    addRandom();
            //}

        }

        void removeImage()
        {
            for (int i = 0; i < range; i++)
                for (int j = 0; j < range; j++)
                    this.Controls.Remove(imagesArray[i, j]);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            //PictureBox picture = sender as PictureBox;
            //for (int i = 0; i < d.range; i++)
            //    for (int j = 0; j < d.range; j++)
            //        if (picture == imagesArray[i, j])
            //        {
            //            Image itmp = imagesArray[i, j].Image;
            //            BorderStyle btmp = imagesArray[i, j].BorderStyle;
            //            if (j > 0 && imagesArray[i, j - 1].Image == null)
            //            {
            //                imagesArray[i, j].Image = imagesArray[i, j - 1].Image;
            //                imagesArray[i, j - 1].Image = itmp;
            //                imagesArray[i, j].BorderStyle = imagesArray[i, j - 1].BorderStyle;
            //                imagesArray[i, j - 1].BorderStyle = btmp;
            //            }
            //            if (j < d.range-1 && imagesArray[i, j + 1].Image == null)
            //            {
            //                imagesArray[i, j].Image = imagesArray[i, j + 1].Image;
            //                imagesArray[i, j + 1].Image = itmp;
            //                imagesArray[i, j].BorderStyle = imagesArray[i, j + 1].BorderStyle;
            //                imagesArray[i, j + 1].BorderStyle = btmp;
            //            }
            //            if (i > 0 && imagesArray[i - 1, j].Image == null)
            //            {
            //                imagesArray[i, j].Image = imagesArray[i - 1, j].Image;
            //                imagesArray[i - 1, j].Image = itmp;
            //                imagesArray[i, j].BorderStyle = imagesArray[i - 1, j].BorderStyle;
            //                imagesArray[i - 1, j].BorderStyle = btmp;
            //            }
            //            if (i < d.range-1 && imagesArray[i + 1, j].Image == null)
            //            {
            //                imagesArray[i, j].Image = imagesArray[i + 1, j].Image;
            //                imagesArray[i + 1, j].Image = itmp;
            //                imagesArray[i, j].BorderStyle = imagesArray[i + 1, j].BorderStyle;
            //                imagesArray[i + 1, j].BorderStyle = btmp;
            //            }

            //            i = d.range;
            //            break;
            //        }
        }
    }
}
