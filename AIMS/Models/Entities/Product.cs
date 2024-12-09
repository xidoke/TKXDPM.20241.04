using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.Models.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public double Value { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public DateTime WarehouseEntryDate { get; set; }
        public string Dimensions { get; set; }
        public double Weight { get; set; }

        // Thuộc tính riêng cho từng loại
        public string Authors { get; set; } // Book
        public string CoverType { get; set; } // Book
        public string Publisher { get; set; } // Book
        public DateTime PublicationDate { get; set; } // Book
        public int Pages { get; set; } // Book
        public string BookGenre { get; set; } // Book

        public string Artist { get; set; } // CD, LP
        public string RecordLabel { get; set; } // CD, LP
        public string Tracklist { get; set; } // CD, LP
        public string MusicGenre { get; set; } // CD, LP
        public DateTime ReleaseDate { get; set; } // CD, LP, DVD

        public string DiscType { get; set; } // DVD
        public string Director { get; set; } // DVD
        public int Runtime { get; set; } // DVD
        public string Studio { get; set; } // DVD
        public string Subtitles { get; set; } // DVD
        public string DvdGenre { get; set; } // DVD
                                             // ... (thêm các thuộc tính khác nếu cần)
    }
}
