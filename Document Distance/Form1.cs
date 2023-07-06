using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace Document_Distance
{
    public partial class Form1 : Form
    {
        double[] output = new double[8];
        double time;
        string header1;
        string header2;
        string header_1;
        string header_2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                {
                    // Update the Label text with the selected file path
                    textBox1.Text = openFileDialog.FileName;
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                {
                    // Update the Label text with the selected file path
                    textBox2.Text = openFileDialog.FileName;
                }
            }

        }

        #region program logic
        public static double[] CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            //string document1 = File.ReadAllText(doc1FilePath).ToLower();
            //string document2 = File.ReadAllText(doc2FilePath).ToLower();

            string document1 = new StreamReader(doc1FilePath).ReadToEnd().ToLower();
            string document2 = new StreamReader(doc2FilePath).ReadToEnd().ToLower();
            string cWord;
            double c;
            double numerator = 0, firstTerm = 0, secondTerm = 0, angle = 0;
            int WordsOfDocument1 = 0, WordsOfDocument2 = 0, lettersOfDocument1 = 0, lettersOfDocument2 = 0;
            double[] finalOutput = new double[6];
            //string word = null;
            StringBuilder word = new StringBuilder();

            Dictionary<string, double> d1Vector = new Dictionary<string, double>();
            Dictionary<string, double> d2Vector = new Dictionary<string, double>();

            for (int i = 0; i < document1.Length; i++)
            {
                if (char.IsLetterOrDigit(document1[i]))
                {
                    word.Append(document1[i]);
                    lettersOfDocument1++;
                }
                else
                {
                    cWord = word.ToString();
                    WordsOfDocument1++;
                    if (cWord.Length == 0)
                    {
                        WordsOfDocument1--;
                        continue;
                    }

                    if (!d1Vector.TryGetValue(cWord, out c))
                    {
                        d1Vector.Add(cWord, 1);
                    }
                    else
                    {
                        d1Vector[cWord]++;
                    }

                    word.Clear();
                }

            }
            cWord = word.ToString();
            if (cWord.Length != 0)
            {
                WordsOfDocument1++;
                if (!d1Vector.TryGetValue(cWord, out c))
                {
                    d1Vector.Add(cWord, 1);
                }
                else
                {
                    d1Vector[cWord]++;
                }
            }

            word.Clear();


            for (int i = 0; i < document2.Length; i++)
            {
                if (char.IsLetterOrDigit(document2[i]))
                {
                    word.Append(document2[i]);
                    lettersOfDocument2++;
                }
                else
                {
                    cWord = word.ToString();
                    WordsOfDocument2++;
                    if (cWord.Length == 0)
                    {
                        WordsOfDocument2--;
                        continue;
                    }
                    if (!d2Vector.ContainsKey(cWord))
                    {
                        d2Vector.Add(cWord, 1);
                    }
                    else
                    {
                        d2Vector[cWord]++;
                    }

                    word.Clear();

                }

            }
            cWord = word.ToString();
            if (cWord.Length != 0)
            {
                WordsOfDocument2++;
                if (!d2Vector.ContainsKey(cWord))
                {
                    d2Vector.Add(cWord, 1);
                }
                else
                {
                    d2Vector[cWord]++;
                }
            }


            foreach (var i in d1Vector.Keys)
            {
                firstTerm += Math.Pow(d1Vector[i], 2);
                if (d2Vector.ContainsKey(i))
                {
                    numerator += d1Vector[i] * d2Vector[i];
                }

            }
            foreach (var i in d2Vector.Keys)
            {
                secondTerm += Math.Pow(d2Vector[i], 2);
            }


            angle = numerator / (Math.Sqrt(Math.Abs(firstTerm) * Math.Abs(secondTerm)));
            angle = Math.Acos(angle) * (180 / Math.PI);
            double OldRange = (90 - 0);
            double NewRange = (0 - 100);
            double NewValue = (((angle - 0) * NewRange) / OldRange) + 100;
            finalOutput[0] = angle;
            finalOutput[1] = NewValue;
            finalOutput[2] = WordsOfDocument1;
            finalOutput[3] = lettersOfDocument1;
            finalOutput[4] = WordsOfDocument2;
            finalOutput[5] = lettersOfDocument2;
            return finalOutput;

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                MessageBox.Show("Please Enter The Two Pathes for the documents!");
            }
            else
            {
                string path1 = textBox1.Text;
                string path2 = textBox2.Text;

                header1 = textBox1.Text;
                string[] words1 = header1.Split('\\');
                header_1 = words1[words1.Length - 1];

                header2 = textBox2.Text;
                string[] words2 = header2.Split('\\');
                header_2 = words2[words2.Length - 1];


                Stopwatch sw = Stopwatch.StartNew();
                output = CalculateDistance(path1, path2);
                sw.Stop();
                time = sw.ElapsedMilliseconds;
                time /= 1000.0;
               
                textBox3.Text = Convert.ToString(output[0]); //angle
                textBox4.Text = Convert.ToString(output[1]); // NewValue --> similarity
                textBox5.Text = Convert.ToString(output[2]); //wd1
                textBox6.Text = Convert.ToString(output[3]); //ld1
                textBox7.Text = Convert.ToString(output[4]); //wd2
                textBox8.Text = Convert.ToString(output[5]); //ld2
                textBox9.Text = Convert.ToString(time);
            }
        }

        

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Print_document print_Document = new Print_document(output,time,header_1,header_2);
            print_Document.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox7.Text = String.Empty;
            textBox8.Text = String.Empty;
            textBox9.Text = String.Empty;
            time = 0.0;
            for (int i = 0; i < 6; i++)
            {
                output[i] = 0.0;
            }
        }

       
    }
}
