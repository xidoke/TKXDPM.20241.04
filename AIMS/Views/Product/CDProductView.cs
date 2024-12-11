﻿using AIMS.Controllers.Product;
using AIMS.Models.Entities;
using AIMS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Views.Product
{
    public partial class CDProductView : UserControl
    {
        private MediaController mediaController;
        public CDProductView()
        {
            mediaController = new MediaController();
            InitializeComponent();
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.ToLower();
            await mediaController.SearchProductByTitleAsync(searchText, flpCD, "CD", "productCard");
        }

        private async void CDProductView_Load(object sender, EventArgs e)
        {
            await mediaController.LoadMediaListbyCategory(flpCD, "CD", "productCard");
        }
     
        private async void sortByPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sortByPrice.Text))
                await mediaController.SortByPrice(flpCD, "CD", sortByPrice.SelectedIndex, "productCard");
        }
    }
}
