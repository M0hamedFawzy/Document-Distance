using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfiumViewer;

namespace Document_Distance
{
    public partial class Info_form : Form
    {
        int imagePointer =0;
        string[] imgPathes = {
            "D:\\6th Semester\\Algo\\Assignements\\Assignemnet1 [New Version]\\Document Distance\\Document Distance\\assets\\img1.png",
            "D:\\6th Semester\\Algo\\Assignements\\Assignemnet1 [New Version]\\Document Distance\\Document Distance\\assets\\img2.png",
            "D:\\6th Semester\\Algo\\Assignements\\Assignemnet1 [New Version]\\Document Distance\\Document Distance\\assets\\img3.png",
            "D:\\6th Semester\\Algo\\Assignements\\Assignemnet1 [New Version]\\Document Distance\\Document Distance\\assets\\img4.png"
        };

        public Info_form()
        {
            InitializeComponent();
        }

        private void Info_form_Load(object sender, EventArgs e)
        {
            string imagePath = "D:\\6th Semester\\Algo\\Assignements\\Assignemnet1 [New Version]\\Document Distance\\Document Distance\\assets\\img1.png";
            Image image = Image.FromFile(imagePath);
            pictureBox1.Image = image;
        }

        private void button2_Click(object sender, EventArgs e) //next btn
        {
            imagePointer++;
            if (imagePointer % 4 == 0)
                imagePointer = 0;
            string imagePath = imgPathes[imagePointer];
            Image image = Image.FromFile(imagePath);
            pictureBox1.Image = image;
        }

        private void prevBtn_Click(object sender, EventArgs e) //prev btn
        {
            imagePointer--;
            if (imagePointer < 0)
                imagePointer = 3;
            string imagePath = imgPathes[imagePointer];
            Image image = Image.FromFile(imagePath);
            pictureBox1.Image = image;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
