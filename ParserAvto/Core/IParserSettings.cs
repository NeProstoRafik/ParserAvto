namespace ParserAvto.Core
{
    public interface IParserSettings
    {
        public  string BaseUrl { get;  }
        public string Prefix { get; set; }
        public static string SecondUrl { get;  }
    }
}
