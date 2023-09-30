using AngleSharp.Html.Dom;
using ParserAvto.Models;

namespace ParserAvto.Core
{
    public interface IParser
    {
        Task<IHtmlDocument> HtmlLoad(IParserSettings settings);
        List<Avto> Parse(IHtmlDocument document);
    }
}
