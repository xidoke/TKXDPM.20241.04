using AIMS.Controllers.Address;
using AIMS.Controllers.Cart;
using AIMS.Controllers.Order;
using AIMS.Controllers.Payment;
using AIMS.Controllers.Product;
using AIMS.Services;
using AIMS.Services.impl;
using AIMS.Views;
using AIMS.Views.Cart;
using AIMS.Views.Order;
using AIMS.Views.Payment;
using AIMS.Views.Product;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<ICDService, CDService>();
            services.AddSingleton<IDistrictService, DistrictService>();
            services.AddSingleton<IDVDService, DVDService>();
            services.AddSingleton<IMediaService, MediaService>();
            services.AddSingleton<IOrderMediaService, OrderMediaService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IProvinceService, ProvinceService>();
            services.AddSingleton<IWardService, WardService>();
            services.AddSingleton<VnPayService>();

            services.AddSingleton<AddressController>();
            services.AddSingleton<CartController>();
            services.AddSingleton<MediaController>();
            services.AddSingleton<PlaceOrderController>();
            services.AddSingleton<PaymentController>();

            services.AddSingleton<PlaceOrderView>();

            services.AddSingleton<PaymentResultView>();
            services.AddSingleton<PaymentView>();

            services.AddSingleton<BookDetailsView>();
            services.AddSingleton<CDDetailsView>();
            services.AddSingleton<DVDDetailsView>();
            services.AddSingleton<ProductCard>();
            services.AddSingleton<ProductMiniCard>();

            services.AddSingleton<SearchMediaResultView>();
            services.AddSingleton<NavBar>();

            services.AddSingleton<CartView>();
            services.AddSingleton<HomeView>();

            services.AddSingleton<MainForm>();

            var serviceProvider = services.BuildServiceProvider();

            var mainForm = serviceProvider.GetRequiredService<MainForm>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mainForm);
        }
    }
}
