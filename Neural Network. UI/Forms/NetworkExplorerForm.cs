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
    public partial class NetworkExplorerForm : Form
    {
        public NetworkExplorerForm()
        {
            InitializeComponent();
        }

        #region Events
        private void TVNetworks_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }
        private void BRemove_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In development", "Warning");
            return;
            if (TVNetworks.SelectedNode == null)
                    return;
            MessageBox.Show(TVNetworks.SelectedNode.Text);

            var dialogResult = MessageBox.Show(
                "Are you sure want to delete selected network (" + TVNetworks.SelectedNode.Text + ")", 
                "Deleteing", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button2
                );

            if (dialogResult == DialogResult.Yes)
            {

            }
        }
        #endregion

        #region Methods
        public void RefreshTree()
        {
            // Readable
            //TreeNode[] nodes = new TreeNode[Project.NetworksCount];
            //for (int i = 0; i < Project.NetworksCount; i++)
            //{
            //    var projects = (NeuralNetworkService.GetInputProjectsForNetwork(Project.InputProjects.ToArray(), Project.Networks[i])).Select(x => new TreeNode(x.Name)).ToArray();
            //    nodes[i] = new TreeNode(Project.Networks[i].Name, projects);
            //}
            //TVNetworks.Nodes.AddRange(nodes);

            TVNetworks.Nodes.Clear();

            TVNetworks.Nodes.AddRange(
                UIRepository.Project.Networks.Select(n => new TreeNode(n.Name,
                    (NeuralNetworkService.GetInputProjectsForNetwork(UIRepository.Project.InputProjects.ToArray(), n))
                        .Select(p => new TreeNode(p.Name)).ToArray()
                )).ToArray());
        }
        #endregion
    }
}