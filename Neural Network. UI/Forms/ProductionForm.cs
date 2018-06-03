﻿using Neural_Network.UI.Shared;
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
    public partial class ProductionForm : Form
    {
        private Production production;

        public ProductionForm(Production production)
        {
            InitializeComponent();

            NUDDetails.Value = production.Details;
            NUDSpeed.Value = production.Speed;
            NUDEmployeees.Value = production.Employees;
            NUDRhythm.Value = production.Rhythm;
            NUDTact.Value = production.Tact;
        }

        #region Events
        #endregion

        #region Methods
        #endregion
    }
}
