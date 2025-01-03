namespace AIMS.Models.Entities
{
    public class CD : Media
    {
        private string _artist;
        private string _recordLabel;
        private string _tracklist;
        private DateTime _releaseDate;

        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }

        public string RecordLabel
        {
            get { return _recordLabel; }
            set { _recordLabel = value; }
        }

        public string Tracklist
        {
            get { return _tracklist; }
            set { _tracklist = value; }
        }

        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set { _releaseDate = value; }
        }
    }
}
