using AIMS.Models.Entities;

namespace AIMS.Models
{
    public class HomeViewModel
    {
        public List<CD> CDs { get; set; }
        public List<DVD> DVDs { get; set; }
        public List<Book> Books { get; set; }
    }
}
