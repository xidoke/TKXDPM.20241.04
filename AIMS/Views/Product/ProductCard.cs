using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace AIMS.Views.Product
{
    public partial class ProductCard : UserControl
    {
        private int cornerRadius = 10;
        private Color shadowColor = Color.FromArgb(50, 0, 0, 0);
        private int shadowOffset = 15;
        private int shadowBlur = 10;
        public AIMS.Models.Entities.Media currentProduct;
        private readonly DVDDetailsView dvdDetailsView;
        private readonly CDDetailsView cdDetailsView;
        private readonly BookDetailsView bookDetailsView;

        public ProductCard(DVDDetailsView dvdDetailsView, CDDetailsView cdDetailsView, BookDetailsView bookDetailsView)
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
            this.panel1.Paint += PanelContainer_Paint;
            this.panel1.MouseEnter += panel1_MouseEnter;
            this.panel1.MouseLeave += panel1_MouseLeave;
            this.Resize += ProductCard_Resize;
            pictureBox1.Paint += PictureBox1_Paint;
            SetupViewDetailsButton();
            this.dvdDetailsView = dvdDetailsView;
            this.cdDetailsView = cdDetailsView;
            this.bookDetailsView = bookDetailsView;

        }
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                GraphicsPath path = RoundedRect(new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height), cornerRadius, true, true, false, false);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.Clip = new Region(path);
                e.Graphics.DrawImage(pictureBox1.Image, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
                pictureBox1.Region = new Region(path);
            }
        }
        public void LoadProduct(AIMS.Models.Entities.Media product)
        {
            currentProduct = product;
            this.productTitle.Text = currentProduct.Title;
            this.productPrice.Text = currentProduct.Price.ToString();
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
        private void ProductCard_Load(object sender, EventArgs e)
        {
            this.productTitle.Text = TruncateText(currentProduct.Title, 200);
            this.productPrice.Text = $"Giá: {currentProduct.getPriceFormat()} VNĐ";
            productTitle.AutoEllipsis = true;
            productTitle.UseMnemonic = false;
            UpdateViewDetailsButtonPosition();
        }
        private string TruncateText(string text, int maxWidth)
        {
            if (string.IsNullOrEmpty(text)) return "";
            string ellipsis = "...";
            if (text.Length > maxWidth)
                return text.Substring(0, maxWidth) + ellipsis;
            return text;
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

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            /*panel1.BackColor = Color.FromArgb(245, 245, 245);
            panel1.Cursor = Cursors.Hand;*/
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            /* panel1.BackColor = Color.White;
            panel1.Cursor = Cursors.Default;*/
        }

        private void SetupViewDetailsButton()
        {
            btnViewDetails.FlatStyle = FlatStyle.Flat;
            btnViewDetails.FlatAppearance.BorderSize = 0;
            btnViewDetails.Text = "XEM CHI TIẾT";
            btnViewDetails.BackColor = Color.FromArgb(64, 64, 64);
            btnViewDetails.ForeColor = Color.Yellow;
            btnViewDetails.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnViewDetails.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            btnViewDetails.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            UpdateViewDetailsButtonPosition();
            btnViewDetails.Paint += BtnViewDetails_Paint;
        }
        private void UpdateViewDetailsButtonPosition()
        {
            int buttonHeight = 35;
            int margin = 0;
            btnViewDetails.Size = new Size(panel1.Width - 2 * margin, buttonHeight);
            btnViewDetails.Location = new Point(margin, panel1.Height - buttonHeight - margin);
            SetRoundedRegion(btnViewDetails, cornerRadius);
        }
        private void ProductCard_Resize(object sender, EventArgs e)
        {
            UpdateViewDetailsButtonPosition();
        }

        private void BtnViewDetails_Paint(object sender, PaintEventArgs e)
        {
            Button button = (Button)sender;
            Rectangle bounds = new Rectangle(0, 0, button.Width, button.Height);
            int radius = 10;
            GraphicsPath path = RoundedRect(bounds, radius, false, false, true, true);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillPath(new SolidBrush(button.BackColor), path);
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(button.Text, button.Font, new SolidBrush(button.ForeColor), bounds, sf);
            }
        }

        private void SetRoundedRegion(Button button, int radius)
        {
            Rectangle bounds = new Rectangle(0, 0, button.Width, button.Height);
            GraphicsPath path = RoundedRect(bounds, radius, false, false, true, true);
            button.Region = new Region(path);
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius, bool upperLeft, bool upperRight, bool lowerLeft, bool lowerRight)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            if (upperLeft)
                path.AddArc(arc, 180, 90);
            else
                path.AddLine(bounds.Left, bounds.Top, bounds.Left, bounds.Top);
            arc.X = bounds.Right - diameter;
            if (upperRight)
                path.AddArc(arc, 270, 90);
            else
                path.AddLine(bounds.Right, bounds.Top, bounds.Right, bounds.Top);
            arc.Y = bounds.Bottom - diameter;
            if (lowerRight)
                path.AddArc(arc, 0, 90);
            else
                path.AddLine(bounds.Right, bounds.Bottom, bounds.Right, bounds.Bottom);
            arc.X = bounds.Left;
            if (lowerLeft)
                path.AddArc(arc, 90, 90);
            else
                path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Bottom);
            path.CloseFigure();
            return path;
        }
        private void btnViewDetails_Click(object sender, EventArgs e)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}