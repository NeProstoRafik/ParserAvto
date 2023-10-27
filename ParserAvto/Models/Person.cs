namespace ParserAvto.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsRole { get; set; } = true;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int ChatId { get; set; } = 0;
        public string CarName { get; set; } = "";
    }
}
