using AIMS.Views.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Views.Cart
{
    public partial class CartView : UserControl
    {
        public CartView()
        {
            InitializeComponent();
        }

        private void CartView_Load(object sender, EventArgs e)
        {
            // Example: Loading some sample products
            List<AIMS.Models.Entities.Media> products = new List<AIMS.Models.Entities.Media>()
        {
            new AIMS.Models.Entities.Media { title = "Chúa tể những chiếc nhẫn", price = 200000 },
            new AIMS.Models.Entities.Media { title = "Đạt ngu 2", price = 150000 },
            new AIMS.Models.Entities.Media { title = "Đạt ngu 3", price = 250000 }
        };

            // Display each product in a ProductCard
            foreach (var product in products)
            {
                ProductCard currentCard = new ProductCard();
                currentCard.LoadProduct(product);
                currentCard.Margin = new Padding(5); // Add spacing around cards
                flowLayoutPanel1.Controls.Add(currentCard); // Add to FlowLayoutPanel in CartView
            }
        }
    }
}
