using AngleSharp.Html.Dom;
using ParserAvto.Handlers;
using ParserAvto.Models;

namespace ParserAvto.Core.AvtoParser
{
    public class AvtoParser : IParser
    {
        private readonly SaveAvtoForDb _saveAvtoForDb;

        public AvtoParser(SaveAvtoForDb saveAvtoForDb)
        {
            _saveAvtoForDb = saveAvtoForDb;
        }

        public async void Parse(IHtmlDocument document)
        {

            var items = document.QuerySelectorAll("body > div > div.css-1iexluz.e1m0rp603> div.css-chb7it.e1m0rp604 > div.css-0.e1m0rp605 > div.css-1173kvb.eojktn00 > div.css-1nvf6xk.eojktn00> div > .css-xb5nz8.e1huvdhj1");
            foreach (var item in items)
            {
                Avto avto = new Avto();
                avto.Url = item.GetAttribute("href");
                var imageElement = item.QuerySelector(".css-9xa2an.e17rxqpm0>div.css-ocloca.e1e9ee560>picture>img");
                if (imageElement != null)
                {
                    avto.Image = imageElement.GetAttribute("data-src");
                    if (avto.Image == null || avto.Image=="#")
                    {
                        avto.Image = "https://yandex.ru/images/search?img_url=https%3A%2F%2Fimages.wbstatic.net%2Fbig%2Fnew%2F42720000%2F42725679-1.jpg&lr=54&pos=0&rpt=simage&source=serp&text=картинка%20нет%20фото";
                    }
                }
                var nameCar = item.QuerySelector(" div.css-13ocj84.e1icyw250 > div:nth-child(1) > div.css-1wgtb37.e3f4v4l2>span");
                if (nameCar != null)
                {
                    avto.Name = nameCar.TextContent;
                }
                else
                {
                    avto.Name = "Машина продана";
                }

                var discriptionElement = item.QuerySelectorAll(".css-13ocj84.e1icyw250>div.css-1fe6w6s.e162wx9x0>.css-1l9tp44.e162wx9x0");

                for (int i = 0; i < discriptionElement.Count(); i++)
                {
                    if (i == 5)
                    {
                        break;
                    }
                    avto.Description += discriptionElement[i].TextContent + " ";
                }

                avto.Price = item.QuerySelector(" div.css-1dkhqyq.e1f2m3x80 > div:nth-child(1) > div > div > span > span")?.TextContent;

                await _saveAvtoForDb.AddForBD(avto);

            }

        }







        //public async Task<IHtmlDocument> HtmlLoad(IParserSettings settings)
        //{    
        //    var url = settings.BaseUrl;
        //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //    var response = await httpClient.GetStringAsync(url);            
        //    var domParser = new HtmlParser();
        //    var document = await domParser.ParseDocumentAsync(response);
        //    httpClient.Dispose();
        //    return document;
        //}
    }
}
