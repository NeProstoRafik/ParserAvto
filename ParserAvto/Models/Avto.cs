namespace ParserAvto.Models
{
    public class Avto
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Name { get; set; }
        public string? Price { get; set; } = "НЕТ цены";
        public string? Image { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
      

    }
}
