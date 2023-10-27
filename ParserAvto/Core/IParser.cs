using AngleSharp.Html.Dom;
using ParserAvto.Models;

namespace ParserAvto.Core
{
    public interface IParser
    { 
        void Parse(IHtmlDocument document);
    }
}
