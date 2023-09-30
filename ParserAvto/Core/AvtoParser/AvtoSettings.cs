﻿namespace ParserAvto.Core.AvtoParser
{
    public class AvtoSettings : IParserSettings
    {
        public  string BaseUrl { get;  } = "https://auto.drom.ru/region66/all/?maxprice=300000&minyear=2007&damaged=1";
        public string Prefix { get; set; } = "current";
        public static int PageId { get; set; }= 1;
        public static string SecondUrl { get; } = "https://auto.drom.ru/region66/all/pagecurrent/?maxprice=300000&minyear=2007&damaged=1";
    }
}