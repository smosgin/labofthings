﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeOS.Hub.Tools.UpdateManager
{
    public partial class AzureAccountForm : Form
    {
        public AzureAccountForm()
        {
            InitializeComponent();
        }

        private void maskedTextBoxAzureAccountName_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBoxAzureAccountKey_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void buttonAzureAccountAdd_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
