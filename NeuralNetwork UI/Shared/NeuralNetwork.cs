﻿using Neural_Network.Core.Extra;
using Neural_Network.Core.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_UI.Shared
{
    public class NeuralNetwork
    {
        public static FeedforwardNetworkSHL Network { get; set; } = null;
        public static NeuralNetworkInputProject InputProject { get; set; } = null;
    }
}
