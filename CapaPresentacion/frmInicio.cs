﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();

            timer1.Start();
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            LABELhora.Text = DateTime.Now.ToString("HH:mm:ss");

            LABELdia.Text = DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy");
        }
    }
}
