using AIMS.Controllers.Product;
using System;
using System.Windows.Forms;

namespace AIMS.Views
{
    public partial class SearchMediaResultView : UserControl
    {
        private string searchContent = string.Empty;
        private string category = null;
        private readonly MediaController mediaController;
        private readonly NavBar navBar;
        public SearchMediaResultView(string searchContent, string category, NavBar navBar, MediaController mediaController)
        {
            InitializeComponent();
            this.searchContent = searchContent;
            this.category = category;
            this.navBar = navBar;
            this.mediaController = mediaController;
        }

        public string SearchContent 
        {
            get { return searchContent; }
            set { searchContent = value; }
        }

        public string Category 
        {
            get { return category; }
            set { category = value; }
        }

        private async void buttonShowSearchResults_Click(object sender, EventArgs e)
        {
            decimal? minPrice = null;
            decimal? maxPrice = null;
            if (decimal.TryParse(txtPriceFrom.Text, out decimal parsedMinPrice))
            {
                minPrice = parsedMinPrice;
            }
            else if (!string.IsNullOrEmpty(txtPriceFrom.Text))
            {
                MessageBox.Show("Invalid 'Price From' value. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (decimal.TryParse(txtPriceTo.Text, out decimal parsedMaxPrice))
            {
                maxPrice = parsedMaxPrice;
            }
            else if (!string.IsNullOrEmpty(txtPriceTo.Text))
            {
                MessageBox.Show("Invalid 'Price To' value. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(category))
            {
                string categoryFilter = mediaController.getCategory(comboBoxFilterbyCategory.SelectedIndex);
                try
                {
                    await mediaController.FilterAndSortProductsAdvancedAsync(
                        flpSearchResults,
                        "productCard",
                        searchContent,
                        minPrice,
                        maxPrice,
                        categoryFilter,
                        comboBoxSortbyPrice.SelectedIndex == 0
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    await mediaController.FilterAndSortProductsAdvancedAsync(
                        flpSearchResults,
                        "productCard",
                        searchContent,
                        minPrice,
                        maxPrice,
                        this.category,
                        comboBoxSortbyPrice.SelectedIndex == 0
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void SearchMediaResultView_Load(object sender, EventArgs e)
        {
            this.flpNavBar.Controls.Add(navBar);
            navBar.Show();
            if (!string.IsNullOrEmpty(this.category))
            {
                this.labelCategory.Visible = false;
                this.comboBoxFilterbyCategory.Visible = false;
            }
            await mediaController.FilterAndSortProductsAdvancedAsync(flpSearchResults, "productCard", searchContent, null, null, category, false);
        }
    }
}
