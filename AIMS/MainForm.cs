using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Views;
using AIMS.Views.Cart;
using AIMS.Views.Product;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
        }
        string connectionString = "User ID=postgres.dwsijitgwefuomejoime;Password=9Tb9eeaw1vsmClOd;Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;Pooling=true;";

        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        private void connection()
        {
            vCon = new NpgsqlConnection();
            vCon.ConnectionString = connectionString;
            if (vCon.State == ConnectionState.Closed)
            {
                vCon.Open();
            }
        }
        public DataTable getdata(string sql)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = sql;
            NpgsqlDataReader dataReader = vCmd.ExecuteReader();
            dt.Load(dataReader);
            return dt;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            HomeView homeViewUC = new HomeView();
            mainFormPanel.Controls.Add(homeViewUC);
            homeViewUC.Visible = true;
            homeViewUC.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CartView cartView = new CartView();
            DVDDetailsView productDetailsView = new DVDDetailsView(12);
            mainFormPanel.Controls.Add(cartView);
            mainFormPanel.Controls.Add(productDetailsView);
            cartView.Visible = false;
            productDetailsView.Visible = true;
            productDetailsView.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CartView cartView = new CartView();
            mainFormPanel.Controls.Clear();
            mainFormPanel.Controls.Add(cartView);
            cartView.Dock = DockStyle.Fill;
            cartView.Show();
        }
        public void PrintDataTableToFile(DataTable dataTable, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write column headers (optional)
                    string headerLine = string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName));
                    writer.WriteLine(headerLine);

                    // Write each row
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string rowLine = string.Join(",", row.ItemArray.Select(item => item.ToString()));
                        writer.WriteLine(rowLine);
                    }

                    Console.WriteLine($"Data successfully written to {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }

    }
}
