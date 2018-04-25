﻿using System;
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
using Neural_Network.Core.Extra;
using Neural_Network.Core.Implementation;
using Neural_Network_WF__Graduate_Work_.Forms.Dialogs;

namespace Neural_Network_WF__Graduate_Work_.Forms
{
    /*
     * TODO: 
     * Optimize file reading (FileStream)
    */

    public partial class PracticeNetworkForm : Form
    {
        private FeedforwardNetworkSHL network;
        private EditInputSignalsForm signalsForm;

        public NeuralNetworkInputProject inputProject;
        private double[] signals = null;

        public PracticeNetworkForm(FeedforwardNetworkSHL network)
        {
            this.network = network;

            InitializeComponent();
        }

        #region Events
        private void PracticeNetworkForm_Move(object sender, EventArgs e)
        {
            if (signalsForm == null)
                return;

            signalsForm.Location = new Point(Location.X + ClientSize.Width, Location.Y);
        }
        private void BCreateProject_Click(object sender, EventArgs e)
        {
            EnterProjectNameForm enterProjectName = new EnterProjectNameForm();
            enterProjectName.ShowDialog(this);
        }
        private void BOpenProject_Click(object sender, EventArgs e)
        {

        }
        private void BInput_Click(object sender, EventArgs e)
        {
            if (signals == null)
            {
                signals = new double[network.InputLayerSize];
            }
            if (signalsForm != null)
                signalsForm.Focus();
            else
                ShowManualInputForm();
        }
        private void BLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory()
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    signals = ReadSignals(ofd.FileName);
                    if (signals != null)
                    {
                        LFile.Text = Path.GetFileName(ofd.FileName);
                        if (signalsForm != null)
                            signalsForm.Close();
                        ShowManualInputForm();
                    }
                }
                catch (IOException exp)
                {
                    //MessageBox.Show("IOException was thrown. See log file for details");
                    MessageBox.Show("IOexception was thrown. Message: " + exp.Message);
                }
            }
        }
        private void BBack_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Methods
        private void ShowManualInputForm()
        {
            signalsForm = new EditInputSignalsForm(network, signals);
            signalsForm.FormClosed += (Sender, E) =>
            {
                Move -= PracticeNetworkForm_Move;
            };
            Move += PracticeNetworkForm_Move;
            signalsForm.Show(this);
            signalsForm.Location = new Point(Location.X + ClientSize.Width, Location.Y);
        }
        private double[] ReadSignals(String filePath)
        {
            if (!File.Exists(filePath))
                return null;

            String text = File.ReadAllText(filePath);
            try
            {
                text = text.Substring(text.IndexOf('{') + 1);
                text = text.Remove(text.IndexOf('}'));
            }
            catch (ArgumentException exp)
            {
                MessageBox.Show($"Incorrect file format. (0)\n{exp.Message}", "Error");
                return null;
            }

            String[] parts = text.Split(',');
            double[] signals = null;
            try
            {
                signals = Array.ConvertAll(parts, Double.Parse);
            }
            catch (FormatException exp)
            {
                MessageBox.Show($"Incorrect file format. (1)\n{exp.Message}", "Error");
                return null;
            }

            int signalsCount = signals.Count();
            int inputCount = network[0].Count();
            if (signalsCount < inputCount)
            {
                var answer = MessageBox.Show("Signals count less than network input neurons count. Press 'Yes' for random filling empty signals. Press 'No' for zero (0) empty input signals. Press 'Cancel' for close this file.", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                MessageBox.Show(answer.ToString());
                return null;
            }
            else if (signalsCount > inputCount)
            {
                var answer = MessageBox.Show("Signals count more than network input neurons count. Press 'Yes' for truncating extra signals. Press 'No' for closing this file.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (answer)
                {
                    case DialogResult.Yes:
                        Array.Resize(ref signals, network[0].Count());
                        break;
                    case DialogResult.No:
                        return null;
                }
            }

            return signals;
        }
        #endregion
    }
}