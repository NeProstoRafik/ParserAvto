using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ParserAvto.Models;
using System.Text;

namespace ParserAvto.Core.AvtoParser
{
    public class AvtoParser : IParser
    {
        private readonly HttpClient httpClient ;

        public AvtoParser(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        readonly string _url;

        private Avto Avto;
        public async Task<IHtmlDocument> HtmlLoad(IParserSettings settings)
        {    
            var url = settings.BaseUrl;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var response = await httpClient.GetStringAsync(url);            
            var domParser = new HtmlParser();
            var document = await domParser.ParseDocumentAsync(response);
            httpClient.Dispose();
            return document;
        }
        public List<Avto> Parse(IHtmlDocument document)
        {
           
            var List = new List<Avto>();
            var items = document.QuerySelectorAll("body> div> div.css-1iexluz.e1m0rp603 > div.css-1f36sr9.e1m0rp604 > div.css-0.e1m0rp605 >div.css-1173kvb.eojktn00> div.css-1nvf6xk.eojktn00>div> .css-xb5nz8.e1huvdhj1");//.Where(a => a.ClassName != null && a.ClassName.Contains("")); // Исправлено условие проверки ClassName
            foreach (var item in items)
            {
                Avto avto = new Avto();
                avto.Url = item.GetAttribute("href");
                var imageElement = item.QuerySelector(".css-9xa2an.e17rxqpm0>div.css-ocloca.e1e9ee560>picture>img");
                if (imageElement != null)
                {
                   // avto.Description = imageElement.GetAttribute("alt");
                    avto.Image = imageElement.GetAttribute("data-src");
                  
                }
                var nameCar= item.QuerySelector(" div.css-13ocj84.e1icyw250 > div:nth-child(1) > div.css-1wgtb37.e3f4v4l2");
                if (nameCar != null)
                {
                    avto.Name = nameCar.TextContent;
                }
                else
                {
                    avto.Name = "Машина продана";
                }
                avto.Description = item.QuerySelector(".css-13ocj84.e1icyw250>div.css-1fe6w6s.e162wx9x0").TextContent;
                avto.Price = item.QuerySelector(" div.css-1dkhqyq.e1f2m3x80 > div:nth-child(1) > div > div > span > span").TextContent;

                List.Add(avto);
            }
            return List;
        }

    }
}
