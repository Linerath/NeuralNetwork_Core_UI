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

namespace Neural_Network.UI.Forms
{
    public partial class NewProductionProjectForm : Form
    {
        public NewProductionProjectForm()
        {
            InitializeComponent();
        }

        private void NewProductionProjectForm_Load(object sender, EventArgs e)
        {
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

            bool details = CBDetails.Checked,
                speed = CBSpeed.Checked,
                employees = CBEmployees.Checked,
                rhythm = CBRhythm.Checked,
                tact = CBTact.Checked;
            bool ordering = CBOrdering.Checked,
                forecasting = CBForecasting.Checked;

            int inputCount = Convert.ToInt32(details) +
                Convert.ToInt32(speed) +
                Convert.ToInt32(employees) +
                Convert.ToInt32(rhythm) +
                Convert.ToInt32(tact);

            //!!!!!!
            int detailsTypesCount = 3;
            //!!!!!!

            FeedforwardNetworkSHL orderingNetwork, forecastingNetwork;
            orderingNetwork = forecastingNetwork = null;
            if (CBOrdering.Checked)
            {
                orderingNetwork = new FeedforwardNetworkSHL(name + "_Ordering", inputCount, inputCount * 2, detailsTypesCount, Core.Functions.Sigmoid, 0.05);
                orderingNetwork.SetAllRandomWeights();
                UIRepository.Project.Networks.Add(orderingNetwork);
            }
            if (CBForecasting.Checked)
            {
                forecastingNetwork = new FeedforwardNetworkSHL(name + "_Forecasting", inputCount, inputCount * 2, 2, Core.Functions.Sigmoid, 0.05);
                forecastingNetwork.SetAllRandomWeights();
                UIRepository.Project.Networks.Add(forecastingNetwork);
            }
            var owner = Owner as MainMenuForm;
            var production = new Production
            {
                Name = name,
                OrderingNetwork = orderingNetwork,
                ForecastingNetwork = forecastingNetwork
            };

            if (orderingNetwork != null)
                owner?.ShowNetwork(orderingNetwork);
            if (forecastingNetwork != null)
                owner?.ShowNetwork(forecastingNetwork);
            owner?.ShowProductionForm(production);

            Close();
        }
    }
}