namespace AIMS.Models.Entities
{
    public class DVD : Media
    {
        private string _discType;
        private string _director;
        private int _runtime;
        private string _studio;
        private string _subtitle;
        private DateTime _releaseDate;

        public string DiscType
        {
            get { return _discType; }
            set { _discType = value; }
        }

        public string Director
        {
            get { return _director; }
            set { _director = value; }
        }

        public int Runtime
        {
            get { return _runtime; }
            set { _runtime = value; }
        }

        public string Studio
        {
            get { return _studio; }
            set { _studio = value; }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; }
        }

        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set { _releaseDate = value; }
        }
    }
}
