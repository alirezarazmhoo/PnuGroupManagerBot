using GroupManagerBot.Data;
using GroupManagerBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace GroupManagerBot
{
	class Program
	{
		static string BotToken = "1644426485:AAGj6UdrJACS7oqrIkBzfr3joPgFjZ4jcJo";
		static string ChatId = "-562495052";
		static TelegramBotClient Bot = new TelegramBotClient(BotToken);
		static Message pollMessage = new Message();
		static BotContext botContext = new BotContext();
		static async Task Main(string[] args)
		{
			//var t = await Bot.SendTextMessageAsync(ChatId, "به گروه خوش آمدید");
			Bot.StartReceiving();
			Bot.OnMessage += Bot_Message;
			Bot.OnUpdate += Bot_MessageUpdate;
			Bot.IsReceiving = true;
			Console.ReadLine();
		}
		private static void Bot_MessageUpdate(object sender, UpdateEventArgs e)
		{
			var e3 = e.Update;
			//Bot.StopPollAsync(ChatId, e.Update.Message.MessageId);
			if(e3.Poll != null)
			{
				Model.User UserItem = new Model.User();  

				var PollId = e3.Poll.Id;
				var PollItem = botContext.UserMessages.FirstOrDefault(s => s.PollId.Equals(PollId));
				if(PollItem != null)
				{
			    if(e3.Poll.Options[0].VoterCount >= 2)
				{
					
					UserItem = botContext.Users.FirstOrDefault(s => s.Id == PollItem.UserId);
					Bot.SendTextMessageAsync(ChatId, $"کاربر {UserItem.FullName} به دلیل کسب امتیاز بالا، ارتقا یافت ");
				}
				if(e3.Poll.Options[1].VoterCount >= 2)
				{
					UserItem = botContext.Users.FirstOrDefault(s => s.Id == PollItem.UserId);
					Bot.SendTextMessageAsync(ChatId, $"مطلب کاربر {UserItem.FullName} به دلیل کسب امتیاز منفی، حذف گردید ");
					Bot.DeleteMessageAsync(ChatId, PollItem.Code);
				}
				}
			}
		}
		private static void Bot_Message(object sender, MessageEventArgs e)
		{
			//Bot.SendTextMessageAsync(ChatId, $" میگه {e.Message.From.FirstName + e.Message.From.LastName}  : {e.Message.Text} "  );
			if (e.Message != null && e.Message.Chat !=null)
			{

				if (!botContext.Users.Any(s => s.UserName.Equals(e.Message.From.Username)))
				{
					GroupManagerBot.Model.User user = new Model.User();
					user.FullName = e.Message.From.FirstName + " " + e.Message.From.LastName;
					user.UserName = e.Message.From.Username;
					user.IsSpecialUser = false;
					botContext.Users.Add(user);
					botContext.SaveChanges();
				}
				var y = Bot.DeleteMessageAsync(ChatId, e.Message.MessageId);
				var pollMessage3 = Bot.SendPollAsync(ChatId, $"از {e.Message.From.FirstName + e.Message.From.LastName}   {e.Message.Text}", options: new[]
					  {
		   "مفید بود!",
		   "مفید نبود!"
		   });
				UserMessage userMessage = new UserMessage();
				userMessage.UserId = botContext.Users.FirstOrDefault(s => s.UserName.Equals(e.Message.From.Username)).Id;
				userMessage.Code = pollMessage3.Result.MessageId;
				userMessage.PollId = pollMessage3.Result.Poll.Id;
				botContext.UserMessages.Add(userMessage);
				botContext.SaveChanges();
				//Bot.StopPollAsync(ChatId, e.Message.MessageId);
			}
		}
	}
}
