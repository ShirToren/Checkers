using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserInterface
{
    public partial class EntryForm : Form
    {
        private int m_SelectedRadioSize;

        public EntryForm()
        {
            InitializeComponent();
        }

        public bool RudioiButton6x6Checked
        {
            get { return rudioiButton6x6.Checked; }
        }

        public bool TwoPlayers
        {
            get { return checkBoxPlayer2.Checked; }
        }

        public bool RudioiButton8x8Checked
        {
            get { return rudioiButton8x8.Checked; }
        }

        public string Player1Name
        {
            get { return textBoxPlayer1.Text; }
        }

        public string Player2Name
        {
            get { return textBoxPlayer2.Text; }
        }

        public bool RudioiButton10x10Checked
        {
            get { return rudioiButton10x10.Checked; }
        }

        public int SelectedRadioSize
        {
            get { return m_SelectedRadioSize; }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(sender.Equals(rudioiButton10x10))
            {
                m_SelectedRadioSize = 10;
            }
            else if(sender.Equals(rudioiButton6x6))
            {
                m_SelectedRadioSize = 6;
            }
            else
            {
                m_SelectedRadioSize = 8;
            }
        }

        private void checkBoxPlayer2_Checked(object sender, EventArgs e)
        {
            if(checkBoxPlayer2.Checked == true)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = "";
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "Computer";
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
