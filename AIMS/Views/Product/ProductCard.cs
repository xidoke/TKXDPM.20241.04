using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public ProductCard()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
            this.panel1.Paint += PanelContainer_Paint;
            this.panel1.MouseEnter += panel1_MouseEnter;
            this.panel1.MouseLeave += panel1_MouseLeave;
        }

        public void LoadProduct(AIMS.Models.Entities.Media product)
        {
            currentProduct = product;
            this.productTitle.Text = currentProduct.title;
            this.productPrice.Text = currentProduct.price.ToString();
        }
        private void ProductCard_Load(object sender, EventArgs e)
        {
            this.productTitle.Text = TruncateText(currentProduct.title, productTitle.Width);
            this.productPrice.Text = $"Giá: {currentProduct.price}";
            productTitle.AutoEllipsis = true;
            productTitle.UseMnemonic = false;
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
    }
}