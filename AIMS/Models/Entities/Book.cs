namespace AIMS.Models.Entities
{
    public class Book : Media
    {
        private string _author;
        private string _coverType;
        private string _publisher;
        private DateTime _publishDate;
        private int _numberOfPages;
        private string _language;

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public string CoverType
        {
            get { return _coverType; }
            set { _coverType = value; }
        }

        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        public DateTime PublishDate
        {
            get { return _publishDate; }
            set { _publishDate = value; }
        }

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set { _numberOfPages = value; }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }
    }
}
