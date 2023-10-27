namespace ParserAvto.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 12;
        public int CountItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)CountItems / PageSize); }
        }
    }
}
