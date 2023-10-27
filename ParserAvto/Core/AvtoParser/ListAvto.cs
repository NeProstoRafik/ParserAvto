using ParserAvto.DAL;
using ParserAvto.Models;

namespace ParserAvto.Core.AvtoParser
{
    public class ListAvto
    {
        private Context context;
        public ListAvto(Context context)
        {
            this.context = context;
        }
        public List<Avto> ReadFromBd(int page, int pageSize)
        {
            return context.Avtos.OrderByDescending(date => date.DateCreate.Day)
            .ThenByDescending(date => date.DateCreate.Hour).ToList().Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public int CountFromBd()
        {
            return context.Avtos.Count();
        }
    }
}
