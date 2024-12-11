using AIMS.Controllers.Product;
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
    public partial class BookProductView : UserControl
    {
        private MediaController mediaController;
        public BookProductView()
        {
            mediaController = new MediaController();
            InitializeComponent();
        }

        private async void BookProductView_Load(object sender, EventArgs e)
        {
            await mediaController.LoadMediaListbyCategory(flpBook, "Book", "productCard");
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.ToLower();
            await mediaController.FilterAndSortProductsAsync(flpBook, "Book", "productCard", searchBox.Text, sortByPrice.SelectedIndex);
        }

        private async void sortByPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sortByPrice.Text))
                await mediaController.FilterAndSortProductsAsync(flpBook, "Book", "productCard", searchBox.Text, sortByPrice.SelectedIndex);
        }
    }
}
