using ParserAvto.DAL;
using ParserAvto.Models;

namespace ParserAvto.Handlers
{
    public class SaveAvtoForDb
    {
        private readonly BotHandler botHandler;    
        private readonly Context context;

        public SaveAvtoForDb(Context context, BotHandler botHandler)
        {
            this.context = context;
            this.botHandler = botHandler;
        }

        public async Task AddForBD(Avto avto)
        {
            var GetAvto = context.Avtos.FirstOrDefault(x => x.Url == avto.Url);
            if (GetAvto == null)
            {
                await botHandler.BotMessageHandlerAsync(avto);
                await context.Avtos.AddAsync(avto);
                await context.SaveChangesAsync();
            }
        }
    }
}
