namespace ParserAvto.Core
{
    public interface IParserFactory
    {
        IParser CreateParser();
    }

    public class ParserFactory : IParserFactory
    {
        private readonly IServiceProvider _provider;

        public ParserFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IParser CreateParser()
        {
            return _provider.GetRequiredService<IParser>();
        }
    }

}
