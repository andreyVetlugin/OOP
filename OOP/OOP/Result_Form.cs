﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OOP
{
    public partial class Result_Form : Form
    {
        private Bitmap bmp;

        public Result_Form(string result_file_name)
        {
            InitializeComponent();
            bmp = new Bitmap(result_file_name);
            label_end.Location = new Point(label_end.Location.X, bmp.Height - label_end.Height);
            pictureBox.Image = bmp;
        }

        private void Result_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            bmp.Dispose();
        }
    }
}
