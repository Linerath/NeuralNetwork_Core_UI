﻿using Neural_Network.Core.Extra;
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
    public partial class InputProjectForm : Form
    {
        public int NetworkIndex { get; set; }
        public int InputProjIndex { get; private set; }
        public TableViewSettings ViewSettings { get; private set; }

        public InputProjectForm(int networkIndex, int inputProjIndex)
        {
            InitializeComponent();

            NetworkIndex = networkIndex;
            InputProjIndex = inputProjIndex;
            ViewSettings = new TableViewSettings();

            MinimumSize = Size;
        }

        #region Events
        #endregion

        #region Methods
        public void FullRefresh()
        {
            RefreshProjects();
            RefreshFont();
            RefreshCellsAutoSize();
        }
        public void RefreshProjects()
        {
            var proj = UIRepository.Project.InputProjects[InputProjIndex];

            DGVInputFields.RowCount = 0;
            DGVOutputFields.RowCount = 0;
             
            DGVInputFields.RowCount = proj.InputFieldsCount;
            DGVOutputFields.RowCount = proj.OutputFieldsCount;

            for (int i = 0; i < DGVInputFields.RowCount; i++)
                DGVInputFields[0, i].Value = (i + 1).ToString();
            for (int i = 0; i < DGVOutputFields.RowCount; i++)
                DGVOutputFields[0, i].Value = (i + 1).ToString();
        }
        public void RefreshFont()
        {
            TableHandler.RefreshFont(DGVInputFields, ViewSettings);
        }
        public void RefreshCellsAutoSize()
        {
            //TableHandler.RefreshCellsAutoSize(DGVInputFields, ViewSettings);
        }
        #endregion
    }
}
