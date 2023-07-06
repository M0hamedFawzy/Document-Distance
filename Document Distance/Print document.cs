using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Document_Distance
{
    public partial class Print_document : Form
    {
        string angle;
        string similarity;
        string wd1;
        string ld1;
        string wd2;
        string ld2;
        string header1;
        string header2;
        string time;
        public Print_document(double[] output, double t, string h1, string h2)
        {
            InitializeComponent();
            angle = Convert.ToString(output[0]);
            similarity = Convert.ToString(output[1]);
            wd1 = Convert.ToString(output[2]);
            ld1 = Convert.ToString(output[3]);
            wd2 = Convert.ToString(output[4]);
            ld2 = Convert.ToString(output[5]);
            header1 = h1;
            header2 = h2;
            time = Convert.ToString(t);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string path = folderBrowserDialog.SelectedPath.ToString();
                    string fileName = "Palagarism Report.txt";
                    textBox1.Text = Path.Combine(path, fileName);
                }
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filepath = textBox1.Text.ToString();
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.Write("===============================================" + "\n");
                writer.Write("                                     Palagarism Report:                \n");
                writer.Write("===============================================" + "\n");
                writer.Write("Date : " + DateTime.Now.ToString("yyyy-MM-dd") + "                                              Time : " + DateTime.Now.ToString("HH:mm:ss\n"));
                writer.Write("===============================================" + "\n");
                writer.Write("Document1 : " + header1 +"\n");
                writer.Write("Number of Words is   " + wd1 + "\n");
                writer.Write("Number of letters is   " + ld1 + "\n");
                writer.Write("===============================================" + "\n");
                writer.Write("Document2 : " + header2 + "\n");
                writer.Write("Number of Words is   " + wd2 + "\n");
                writer.Write("Number of letters is   " + ld2 + "\n");
                writer.Write("===============================================" + "\n");
                writer.Write("Angle (Distance) Between Two documents : " + angle + "\n");
                writer.Write("Degree of Similarity is : " + similarity + " %" + "\n");
                writer.Write("===============================================" + "\n");
                writer.Write("Excution time is : " + time + "\n");
            }
            MessageBox.Show("Text saved successfully to the file!");
            this.Close();
        }
    }
}
