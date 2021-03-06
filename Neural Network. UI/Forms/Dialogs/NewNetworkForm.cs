﻿using Neural_Network.Core.Implementation;
using Neural_Network.UI.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neural_Network.UI.Forms.Dialogs
{
    public partial class NewNetworkForm : Form
    {
        public NewNetworkForm()
        {
            InitializeComponent();
        }

        #region Events
        private void NewNetworkForm_Load(object sender, EventArgs e)
        {
            TBName.Text += "_" + UIRepository.Project.NetworksCount;
        }
        private void BOk_Click(object sender, EventArgs e)
        {
            String name = TBName.Text.Trim();
            if (name == String.Empty)
            {
                MessageBox.Show("Invalid name.", "Error");
                TBName.Focus();
                return;
            }

            int inputCount = (int)NUDInput.Value;
            int hiddenCount = (int)NUDHidden.Value;
            int outputCount = (int)NUDOutput.Value;

            var network = new FeedforwardNetworkSHL(name, inputCount, hiddenCount, outputCount);
            UIRepository.Project.Networks.Add(network);

            if (CBRandomWeights.Checked)
                network.SetAllRandomWeights();

            var owner = Owner as MainMenuForm;
            owner?.ShowNetwork(network);

            if (CBCreateAnother.Checked)
            {
                TBName.Text = "Net_" + UIRepository.Project.NetworksCount;
                Focus();
            }
            else
            {
                Close();
            }
        }
        private void BCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Methods
        #endregion
    }
}
