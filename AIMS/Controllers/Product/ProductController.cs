using AIMS.Views.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.Controllers.Product
{
    public class ProductController
    {
        public void test1()
        {
            CartView cart = new CartView();
            MainForm.Instance.mainFormPanel.Controls.Add(cart);
        }
    }
}
