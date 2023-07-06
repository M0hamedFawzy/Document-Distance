using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Document_Distance
{
    public partial class Mian_Form_GUI_ : Form
    {
        private Form currentChildForm;
        public Mian_Form_GUI_()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void Mian_Form_GUI__Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }


        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Info_form());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            exit_form exit_Form = new exit_form();
            exit_Form.Show();
        }
    }
}
