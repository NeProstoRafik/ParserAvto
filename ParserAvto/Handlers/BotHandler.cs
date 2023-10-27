using ParserAvto.DAL;
using ParserAvto.Models;
using Telegram.Bot;

namespace ParserAvto.Handlers
{
    public class BotHandler
    {
        private readonly Context context;
        private ITelegramBotClient bot;
        private IConfiguration _configuration;
        public BotHandler(Context context, IConfiguration configuration)
        {
            this.context = context;
            _configuration = configuration;
        }

        public async Task BotMessageHandlerAsync(Avto avto)
        {
            string token = _configuration["Token"];
            bot = new TelegramBotClient(token);

            foreach (var item in context.Persons)
            {
                if (avto.DateCreate > item.DateTime)
                {
                    await bot.SendTextMessageAsync(item.ChatId, $"{avto.Url}");
                    item.DateTime = avto.DateCreate;

                }
            }
            await context.SaveChangesAsync();
        }

    }
}
