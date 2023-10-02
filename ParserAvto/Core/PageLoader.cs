using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ParserAvto.Core.AvtoParser;
using System.Net.Http;
using System.Text;

namespace ParserAvto.Core
{
    public class PageLoader
    {
        private readonly HttpClient _httpClient;
       
        private HtmlParser domParser = new HtmlParser();
        public PageLoader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
      
        public async Task<IHtmlDocument> GetDocument(int pageNumber, IParserSettings settings)
        {
            var currentUrl=settings.BaseUrl;
            if (pageNumber >1)
            {

                currentUrl = settings.SecondUrl.Replace("current", pageNumber.ToString());
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var response = await _httpClient.GetStringAsync(currentUrl);
            var domParser = new HtmlParser();
            var document = await domParser.ParseDocumentAsync(response);

            return document;

            //var response = await _httpClient.GetAsync(currentUrl);
            //string source = null;
            //if (response != null)
            //{
            //    source = await response.Content.ReadAsStringAsync();
            //}
            //var document = await domParser.ParseDocumentAsync(source);

            //return document;
        }
    }
}
