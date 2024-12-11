﻿using AIMS.Controllers.Product;
using AIMS.Models.Entities;
using AIMS.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Views.Product
{
    public partial class DVDProductView : UserControl
    {
        private MediaController mediaController;
        public DVDProductView()
        {
            mediaController = new MediaController();
            InitializeComponent();
        }

        private async void DVDProductView_Load(object sender, EventArgs e)
        {
            await mediaController.LoadMediaListbyCategory(flpDVD_Product, "DVD", "productCard"); 
        }

        private async void sortByPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sortByPrice.Text))
              await mediaController.SortByPrice(flpDVD_Product, "DVD", sortByPrice.SelectedIndex, "productCard");
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.ToLower();
            await mediaController.SearchProductByTitleAsync(searchText, flpDVD_Product, "DVD", "productCard");
        }
    }
}
