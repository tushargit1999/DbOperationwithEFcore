namespace DbOperationwithEFcoreApp.Data
{
    public class Auther
    {
        public int Id { get; set; }
        public string AutherName { get; set; }
        public string AutherBook { get; set; }

        public ICollection<Book> Books { get; set; }

    }
}
