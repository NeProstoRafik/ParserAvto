using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ParserAvto.Core.AvtoParser;
using System.Net.Http;

namespace ParserAvto.Core
{
    public class PageLoader
    {
        private readonly HttpClient _httpClient;

        readonly string _url = AvtoSettings.SecondUrl;
        private HtmlParser domParser = new HtmlParser();
        public PageLoader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHtmlDocument> GetFromPageId()
        {

            var pageNumber = ++AvtoSettings.PageId;
            return await GetDocument(pageNumber);
        }
        public async Task<IHtmlDocument> GetFromPreviousPageId()
        {
            var pageNumber = --AvtoSettings.PageId;
            return await GetDocument(pageNumber);
        }
        public async Task<IHtmlDocument> GetDocument(int pageNumber)
        {
            var currentUrl = _url.Replace("current", pageNumber.ToString());
            var response = await _httpClient.GetAsync(currentUrl);
            string source = null;
            if (response != null)
            {
                source = await response.Content.ReadAsStringAsync();

            }
            var document = await domParser.ParseDocumentAsync(source);
            _httpClient.Dispose();
            return document;
        }
    }
}
