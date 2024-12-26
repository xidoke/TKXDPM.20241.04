using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace AIMS.Views.Product
{
    public partial class ProductMiniCard : UserControl
    {
        private int cornerRadius = 10;
        private Color shadowColor = Color.FromArgb(50, 0, 0, 0);
        private int shadowOffset = 15;
        private int shadowBlur = 10;
        public AIMS.Models.Entities.Media currentProduct;
        private readonly DVDDetailsView dvdDetailsView;
        private readonly CDDetailsView cdDetailsView;
        private readonly BookDetailsView bookDetailsView;
        public ProductMiniCard(DVDDetailsView dvdDetailsView, CDDetailsView cdDetailsView, BookDetailsView bookDetailsView)
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
            this.panel1.Paint += PanelContainer_Paint;
            this.panel1.MouseEnter += panel1_MouseEnter;
            this.panel1.MouseLeave += panel1_MouseLeave;
            this.dvdDetailsView = dvdDetailsView;
            this.cdDetailsView = cdDetailsView;
            this.bookDetailsView = bookDetailsView;

        }

        private void ProductMiniCard_Load(object sender, EventArgs e)
        {
            this.productTitle.Text = TruncateText(currentProduct.Title, productTitle.Width);
            productTitle.AutoEllipsis = true;
            productTitle.UseMnemonic = false;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel1.Cursor = Cursors.Default;
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(245, 245, 245);
            panel1.Cursor = Cursors.Hand;
        }
        public void LoadProduct(AIMS.Models.Entities.Media product)
        {
            currentProduct = product;
            this.productTitle.Text = currentProduct.Title;
            if (!string.IsNullOrEmpty(product.ImgURL))
            {
                using (WebClient webClient = new WebClient())
                {
                    try
                    {
                        byte[] data = webClient.DownloadData(product.ImgURL);
                        using (MemoryStream mem = new MemoryStream(data))
                        {
                            if (Image.FromStream(mem) != null)
                            {
                                pictureBox1.Image = Image.FromStream(mem);
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        if (ex.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.NotFound)
                        {
                            Console.WriteLine($"Image not found: {product.ImgURL}");
                        }
                        else
                        {
                            Console.WriteLine($"Error downloading image: {ex.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading image: {ex.Message}");
                    }
                }
            }
        }
        private string TruncateText(string text, int maxWidth)
        {
            if (string.IsNullOrEmpty(text)) return "";
            using (Graphics g = productTitle.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(text, productTitle.Font);
                if (textSize.Width <= maxWidth)
                    return text;
            }
            string ellipsis = "...";
            int charactersToKeep = text.Length;
            while (charactersToKeep > 0)
            {
                string truncatedText = text.Substring(0, charactersToKeep) + ellipsis;
                using (Graphics g = productTitle.CreateGraphics())
                {
                    SizeF textSize = g.MeasureString(truncatedText, productTitle.Font);
                    if (textSize.Width <= maxWidth)
                        return truncatedText;
                }
                charactersToKeep--;
            }
            return ellipsis;
        }
        private void PanelContainer_Paint(object sender, PaintEventArgs e)
        {
            CreateRoundedRectangle(e, panel1, cornerRadius);
        }
        private void CreateRoundedRectangle(PaintEventArgs e, Panel panel, int radius)
        {
            Rectangle bounds = new Rectangle(0, 0, panel.Width, panel.Height);
            using (GraphicsPath path = CreateRoundedRectanglePath(bounds, radius))
            {
                panel.Region = new Region(path);
            }
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(bounds.Left, bounds.Top, radius * 2, radius * 2, 180, 90);
            path.AddLine(bounds.Left + radius, bounds.Top, bounds.Right - radius, bounds.Top);
            path.AddArc(bounds.Right - radius * 2, bounds.Top, radius * 2, radius * 2, 270, 90);
            path.AddLine(bounds.Right, bounds.Top + radius, bounds.Right, bounds.Bottom - radius);
            path.AddArc(bounds.Right - radius * 2, bounds.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddLine(bounds.Right - radius, bounds.Bottom, bounds.Left + radius, bounds.Bottom);
            path.AddArc(bounds.Left, bounds.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.AddLine(bounds.Left, bounds.Bottom - radius, bounds.Left, bounds.Top + radius);
            path.CloseFigure();
            return path;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (currentProduct.Category == "DVD")
            {
                MainForm.Instance.mainFormPanel.Controls.Clear();
                MainForm.Instance.mainFormPanel.Controls.Add(dvdDetailsView);
                dvdDetailsView.Show();
            }
            if (currentProduct.Category == "CD")
            {
                MainForm.Instance.mainFormPanel.Controls.Clear();
                MainForm.Instance.mainFormPanel.Controls.Add(cdDetailsView);
                cdDetailsView.Show();
            }
            if (currentProduct.Category == "Book")
            {
                MainForm.Instance.mainFormPanel.Controls.Clear();
                MainForm.Instance.mainFormPanel.Controls.Add(bookDetailsView);
                bookDetailsView.Show();
            }
        }
    }
}
