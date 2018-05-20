﻿using Neural_Network.Core.Implementation;
using Neural_Network.UI.Constants;
using Neural_Network.UI.Constants.Form;
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
    public partial class LayerForm : Form
    {
        private Layers layer;
        public int NetworkIndex { get; private set; }
        public TableViewSettings ViewSettings { get; private set; }

        public LayerForm(Layers layer, int networkIndex)
        {
            InitializeComponent();

            this.layer = layer;
            NetworkIndex = networkIndex;
            ViewSettings = new TableViewSettings();
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
        private void DGVLayer_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double value = Double.Parse(DGVLayer[e.ColumnIndex, e.RowIndex].Value.ToString());
            UIRepository.Project.Networks[NetworkIndex][layer][e.RowIndex][e.ColumnIndex] = value;
        }
        #endregion

        #region Methods
        public void FullLayerRefresh()
        {
            RefreshLayerValues();
            RefreshFont();
            RefreshCellsAutoSize();
        }
        public void RefreshLayerValues()
        {
            switch (layer)
            {
                case Layers.Input:
                    if (ViewSettings.NeuronsSorting == NeuronsSorting.Horizontal)
                    {
                        DGVLayer.RowCount = UIRepository.Project.Networks[NetworkIndex].InputLayerSize;
                        DGVLayer.ColumnCount = 1;
                        for (int i = 0; i < UIRepository.Project.Networks[NetworkIndex].InputLayer.Length; i++)
                            DGVLayer[0, i].Value = UIRepository.Project.Networks[NetworkIndex].InputLayer[i][0];
                    }
                    else
                    {
                        DGVLayer.RowCount = 1;
                        DGVLayer.ColumnCount = UIRepository.Project.Networks[NetworkIndex].InputLayerSize;
                        for (int i = 0; i < UIRepository.Project.Networks[NetworkIndex].InputLayer.Length; i++)
                            DGVLayer[i, 0].Value = UIRepository.Project.Networks[NetworkIndex].InputLayer[i][0];
                    }
                    break;
                case Layers.Hidden:
                    if (ViewSettings.NeuronsSorting == NeuronsSorting.Horizontal)
                    {
                        DGVLayer.RowCount = UIRepository.Project.Networks[NetworkIndex].HiddenLayerSize;
                        DGVLayer.ColumnCount = UIRepository.Project.Networks[NetworkIndex].HiddenLayer.Max(x => x.InputCount);
                        for (int i = 0; i < UIRepository.Project.Networks[NetworkIndex].HiddenLayerSize; i++)
                            for (int j = 0; j < UIRepository.Project.Networks[NetworkIndex].HiddenLayer[i].InputCount; j++)
                                DGVLayer[j, i].Value = Math.Round(UIRepository.Project.Networks[NetworkIndex].HiddenLayer[i][j], ViewSettings.DecimalPlaces);
                    }
                    else
                    {
                        DGVLayer.RowCount = UIRepository.Project.Networks[NetworkIndex].HiddenLayer.Max(x => x.InputCount);
                        DGVLayer.ColumnCount = UIRepository.Project.Networks[NetworkIndex].HiddenLayerSize;
                        for (int i = 0; i < UIRepository.Project.Networks[NetworkIndex].HiddenLayerSize; i++)
                            for (int j = 0; j < UIRepository.Project.Networks[NetworkIndex].HiddenLayer[i].InputCount; j++)
                                DGVLayer[i, j].Value = Math.Round(UIRepository.Project.Networks[NetworkIndex].HiddenLayer[i][j], ViewSettings.DecimalPlaces);
                    }
                    break;
                case Layers.Output:
                    if (ViewSettings.NeuronsSorting == NeuronsSorting.Horizontal)
                    {
                        DGVLayer.RowCount = UIRepository.Project.Networks[NetworkIndex].OutputLayerSize;
                        DGVLayer.ColumnCount = UIRepository.Project.Networks[NetworkIndex].OutputLayer.Max(x => x.InputCount);
                        for (int i = 0; i < UIRepository.Project.Networks[NetworkIndex].OutputLayerSize; i++)
                            for (int j = 0; j < UIRepository.Project.Networks[NetworkIndex].OutputLayer[i].InputCount; j++)
                                DGVLayer[j, i].Value = Math.Round(UIRepository.Project.Networks[NetworkIndex].OutputLayer[i][j], ViewSettings.DecimalPlaces);
                    }
                    else
                    {
                        DGVLayer.RowCount = UIRepository.Project.Networks[NetworkIndex].OutputLayer.Max(x => x.InputCount);
                        DGVLayer.ColumnCount = UIRepository.Project.Networks[NetworkIndex].OutputLayerSize;
                        for (int i = 0; i < UIRepository.Project.Networks[NetworkIndex].OutputLayerSize; i++)
                            for (int j = 0; j < UIRepository.Project.Networks[NetworkIndex].OutputLayer[i].InputCount; j++)
                                DGVLayer[i, j].Value = Math.Round(UIRepository.Project.Networks[NetworkIndex].OutputLayer[i][j], ViewSettings.DecimalPlaces);
                    }
                    break;
            }
        }
        public void RefreshFont()
        {
            TableHandler.RefreshFont(DGVLayer, ViewSettings);
        }
        public void RefreshCellsAutoSize()
        {
            TableHandler.RefreshCellsAutoSize(DGVLayer, ViewSettings);
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