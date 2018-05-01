﻿using Neural_Network.Core.Implementation;
using NeuralNetwork_UI.Constants;
using NeuralNetwork_UI.Forms.Constants;
using NeuralNetwork_UI.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork_UI.Forms
{
    public partial class LayerForm : Form
    {
        private Layers layer;
        public int NetworkIndex { get; private set; }
        public LayerFormViewSettings ViewSettings { get; private set; }

        public LayerForm(Layers layer, int networkIndex)
        {
            InitializeComponent();

            this.layer = layer;
            NetworkIndex = networkIndex;
            ViewSettings = new LayerFormViewSettings();
        }

        #region Events
        private void FollowedForm_Move(object sender, EventArgs e)
        {
            if (ViewSettings.FollowedForm == null)
                return;

            switch (ViewSettings.FollowedFormRelativeLayout)
            {
                case FormRelativeLayout.TopLeft:
                    break;
                case FormRelativeLayout.TopRight:
                    break;

                case FormRelativeLayout.RightTop:
                    Location = new Point(ViewSettings.FollowedForm.Location.X + ViewSettings.FollowedForm.ClientSize.Width, ViewSettings.FollowedForm.Location.Y);
                    break;
                case FormRelativeLayout.RightBottom:
                    break;

                case FormRelativeLayout.BottomLeft:
                    break;
                case FormRelativeLayout.BottomRight:
                    break;

                case FormRelativeLayout.LeftTop:
                    Location = new Point(ViewSettings.FollowedForm.Location.X - ClientSize.Width, ViewSettings.FollowedForm.Location.Y);
                    break;
                case FormRelativeLayout.LeftBottom:
                    break;
            }
        }
        #endregion

        #region Methods
        public void FullLayerRefresh()
        {
            RefreshLayerValues(true);
            RefreshFontSize();
            RefreshCellsAutoSize();
        }
        public void RefreshLayerValues(bool rewriteValues = false)
        {
            switch (layer)
            {
                case Layers.Input:
                    DGVLayer.RowCount = Project.Networks[NetworkIndex].InputLayerSize;
                    DGVLayer.ColumnCount = 1;

                    if (rewriteValues)
                    {
                        for (int i = 0; i < Project.Networks[NetworkIndex].InputLayer.Length; i++)
                            DGVLayer[0, i].Value = Project.Networks[NetworkIndex].InputLayer[i][0];
                    }
                    break;
                case Layers.Hidden:
                    DGVLayer.RowCount = Project.Networks[NetworkIndex].HiddenLayerSize;
                    DGVLayer.ColumnCount = Project.Networks[NetworkIndex].HiddenLayer.Max(x => x.InputCount);

                    if (rewriteValues)
                    {
                        for (int i = 0; i < Project.Networks[NetworkIndex].HiddenLayerSize; i++)
                            for (int j = 0; j < Project.Networks[NetworkIndex].HiddenLayer[i].InputCount; j++)
                                DGVLayer[j, i].Value = Math.Round(Project.Networks[NetworkIndex].HiddenLayer[i][j], ViewSettings.DecimalPlaces);
                    }
                    break;
                case Layers.Output:
                    DGVLayer.RowCount = Project.Networks[NetworkIndex].OutputLayerSize;
                    DGVLayer.ColumnCount = Project.Networks[NetworkIndex].OutputLayer.Max(x => x.InputCount);

                    if (rewriteValues)
                    {
                        for (int i = 0; i < Project.Networks[NetworkIndex].OutputLayerSize; i++)
                            for (int j = 0; j < Project.Networks[NetworkIndex].OutputLayer[i].InputCount; j++)
                                DGVLayer[j, i].Value = Math.Round(Project.Networks[NetworkIndex].OutputLayer[i][j], ViewSettings.DecimalPlaces);
                    }
                    break;
            }
        }
        public void RefreshFontSize()
        {
            foreach (DataGridViewColumn col in DGVLayer.Columns)
            {
                col.DefaultCellStyle.Font = ViewSettings.Font;
            }
        }
        public void RefreshCellsAutoSize()
        {
            foreach (DataGridViewColumn col in DGVLayer.Columns)
            {
                if (ViewSettings.AdaptCellsSize)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                else
                {
                    int oldWidth = col.Width;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    col.Width = oldWidth;
                }
            }
        }
        public void SetFollowedForm(Form form, FormRelativeLayout relativeLayout)
        {
            ViewSettings.FollowedForm = form ?? throw new ArgumentNullException(Exceptions.NULL_ARGUMENT + "(Form: " + Name + ")");
            ViewSettings.FollowedFormRelativeLayout = relativeLayout;

            if (ViewSettings.FollowedForm != null)
            {
                ViewSettings.FollowedForm.Move -= FollowedForm_Move;
                ViewSettings.FollowedForm.Resize -= FollowedForm_Move;
            }

            ViewSettings.FollowedForm.Move += FollowedForm_Move;
            ViewSettings.FollowedForm.Resize += FollowedForm_Move;
        }
        #endregion
    }


}
