using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NotePadClone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // This is needed so we can listen for inputs and therefore automate
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown); // This is needed to listen for key strokes and automatically update the points total
        }

        // Customer shorcut keys and automatic calculation
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space || e.KeyCode.ToString() == "-" || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
            {
                CalculatePoints();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Shows the openFileDialog
            openFileDialog1.ShowDialog();
            //Reads the text file
            System.IO.StreamReader OpenFile = new System.IO.StreamReader(openFileDialog1.FileName);
            //Displays the text file in the textBox
            txtMain.Text = OpenFile.ReadToEnd();
            //Closes the process
            OpenFile.Close();
            CalculatePoints(); // This will calculate the points for the new file that was opened
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Determines the textfile to save to
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(saveFileDialog1.FileName);
            //Writes the text to the file
            SaveFile.WriteLine(txtMain.Text);
            //Closes the process
            SaveFile.Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open the saveFileDialog
            saveFileDialog1.ShowDialog();
            //Determines the text file to save to
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(saveFileDialog1.FileName);
            //Writes the text to the file
            SaveFile.WriteLine(txtMain.Text);
            //Closes the process
            SaveFile.Close();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Declare prntDoc as a new PrintDocument
            System.Drawing.Printing.PrintDocument prntDoc = new System.Drawing.Printing.PrintDocument();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Undo(); //This is incorrect
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.SelectAll();
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalculatePoints();
        }

        private void CalculatePoints()
        {
            string substring; // used to hold the sections of the strings we will be working with
            int numVal; // used to hold the conversion of the string to an int
            int points = 0;
            char[] delimiterChars = { '\n' }; // preparation for dividing txtMain's contents into a string for each line
            string[] data = txtMain.Text.Split(delimiterChars); // splits txtMain.Text into strings in "data" string array

            foreach (string s in data)
            {
                if (s.StartsWith("1") || s.StartsWith("2") || s.StartsWith("3") || s.StartsWith("4") || s.StartsWith("5") || s.StartsWith("6") || s.StartsWith("7") || s.StartsWith("8") || s.StartsWith("9"))
                {
                    substring = s.Split(' ', '\t', '-')[0];
                    if (Int32.TryParse(substring, out numVal))
                    {
                        points = points + numVal;
                    }
                }
            }

            toolStripStatusLabel.Text = "Total Points: " + points.ToString(); // Writes the total points in the status bar
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Instructions:" + '\n' + "Type out your army list with the point cost listed first.");
        }
    }
}
