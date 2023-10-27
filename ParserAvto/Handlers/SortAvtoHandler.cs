using ParserAvto.Models;

namespace ParserAvto.Handlers
{
    public class SortAvtoHandler
    {
        public static List<Avto> SortAvto(List<Avto> avtos)
        {
            var newList = new List<Avto>();
            string nameAvto = "Лада";
            foreach (var avto in avtos)
            {
                if (avto.Name.Contains(nameAvto))
                {
                    newList.Add(avto);
                }
            }
            return newList;
        }
    }
}
