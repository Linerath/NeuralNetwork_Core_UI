﻿using NeuralNetwork_UI.Forms.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork_UI.Shared
{
    public class LayerFormViewSettings
    {
        [Category("Appearance")]
        [DisplayName("Adapt cells size")]
        public bool AdaptCellsSize { get; set; } = true;

        [Category("Appearance")]
        [DisplayName("Decimal places")]
        public int DecimalPlaces { get; set; } = 2;

        [Category("Appearance")]
        public Font Font { get; set; } = new Font("Consolas", 13, GraphicsUnit.Point);

        [Browsable(false)]
        public Form FollowedForm { get; set; }

        [Browsable(false)]
        public FormRelativeLayout FollowedFormRelativeLayout { get; set; }
    }
}