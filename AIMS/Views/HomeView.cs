using AIMS.Services;
using AIMS.Views.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Views
{
    public partial class HomeView : UserControl
    {
        private List<AIMS.Models.Entities.Media> dvdList = new List<Models.Entities.Media>();
        private List<AIMS.Models.Entities.Media> cdList = new List<Models.Entities.Media>();
        private List<AIMS.Models.Entities.Media> bookList = new List<Models.Entities.Media>();
        private MediaService mediaService = new MediaService();
        public HomeView()
        {
            InitializeComponent();
        }

        private void HomeView_Load(object sender, EventArgs e)
        {

        }

        private void LoadProductCards(FlowLayoutPanel flp, List<AIMS.Models.Entities.Media> products)
        {
            if (flp == null)
            {
                Console.WriteLine("FlowLayoutPanel is null.");
                return;
            }
            flp.Controls.Clear();
            if (products == null || products.Count == 0)
            {
                Console.WriteLine("No products to display.");
                return;
            }
            foreach (var product in products)
            {
                ProductMiniCard productCard = new ProductMiniCard();
                productCard.LoadProduct(product);
                flp.Controls.Add(productCard);
            }
        }

        private void lblViewMoreDVD_Click(object sender, EventArgs e)
        {
            DVDProductView dvdProductView = new DVDProductView();
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(dvdProductView);
            dvdProductView.Show();
        }

        private void lblViewMoreCD_Click(object sender, EventArgs e)
        {
            CDProductView cDProductView = new CDProductView();
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(cDProductView);
            cDProductView.Show();
        }

        private void lblViewMoreBOOK_Click(object sender, EventArgs e)
        {

        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (dvdList.Count <= 0)
            {
                dvdList = await mediaService.getListDVD();
                LoadProductCards(flpDVD, dvdList);
                return;
            }
            if (cdList.Count <= 0)
            {
                cdList = await mediaService.getListCD();
                LoadProductCards(flpCD, cdList);
                return;
            }
            if (bookList.Count <= 0)
            {
                bookList = await mediaService.getListBook();
                LoadProductCards(flpBook, bookList);
                return;
            }
        }
    }
}
