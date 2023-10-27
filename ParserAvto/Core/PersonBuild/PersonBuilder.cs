using ParserAvto.DAL;
using ParserAvto.Models;

namespace ParserAvto.Core.PersonBuild
{
    public class PersonBuilder
    {
        private readonly Context _context;
        public PersonBuilder(Context context)
        {
            _context = context;
        }
        public void Create(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();

        }
        public Person Get(Person person)
        {
            var getPerson = _context.Persons.FirstOrDefault(x => x.Name == person.Name);
            if (getPerson != null)
            {
                return getPerson;
            }
            else
            {
                return null;
            }
        }
        public Person Get(User name)
        {
            var getPerson = _context.Persons.FirstOrDefault(x => x.Name == name.username);
            if (getPerson != null)
            {
                return getPerson;
            }
            else
            {
                return null;
            }
        }
        public void Edit(Person person)
        {
            _context.Persons.Update(person);
            _context.SaveChanges();
        }
    }
}
