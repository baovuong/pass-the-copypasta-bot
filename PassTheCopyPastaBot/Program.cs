using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace PassTheCopyPastaBot
{
    class Program
    {
        static ITelegramBotClient botClient;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            botClient = new TelegramBotClient(configuration.GetSection("Settings").GetSection("TelegramBotToken").Value);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var pasta = new CopyPasta { CharacterLimit = 4000 };
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}: {e.Message.Text}");
                PostFilter postFilter;
                switch (e.Message.Text.ToLower())
                {
                    case "/top":
                        Console.WriteLine($"Sending Top Copypasta to {e.Message.Chat.Id}.");
                        postFilter = PostFilter.TOP;
                        break;
                    case "/hot":
                        Console.WriteLine($"Sending Hot Copypasta to {e.Message.Chat.Id}.");
                        postFilter = PostFilter.HOT;
                        break;
                    case "/new":
                        Console.WriteLine($"Sending New Copypasta to {e.Message.Chat.Id}.");
                        postFilter = PostFilter.NEW;
                        break;
                    default:
                        Console.WriteLine($"Unknown command from {e.Message.Chat.Id}.");
                        postFilter = PostFilter.INVALID;
                        break;
                }

                if (postFilter != PostFilter.INVALID)
                {
                    var post = pasta.GetRandomPasta(postFilter);
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: post.Selftext,
                        replyMarkup: new InlineKeyboardMarkup(new[]
                        {
                            InlineKeyboardButton.WithUrl("Go to Post", post.Url.ToString())
                        })
                    );

                }
            }
        }
    }
}
