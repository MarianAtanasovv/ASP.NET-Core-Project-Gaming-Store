using GameStore.Data;
using GameStore.Services.Carts;
using MlkPwgen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Emails
{
    using static DataConstants;

    public class EmailSenderService : IEmailSenderService
    {
        private readonly ApplicationDbContext data;
        private readonly ICartService cart;

        public EmailSenderService(ApplicationDbContext data, ICartService cart)
        {
            this.data = data;
            this.cart = cart;
        }

        public void SendKeyAsync(string userId)
        {
            Task
               .Run(async () =>
               {
                   var email = this.data.Users.Where(x => x.Id == userId).Select(x => x.Email).FirstOrDefault();

                   var cart = this.cart.UsersCart(userId).ToList();

                   var stringBulder = new StringBuilder();

                   var body = $"Greetings, {email}! Thank you for your purchase! To activate your games simply enter the activation keys for each game! Have fun!"; ;
                   stringBulder.AppendLine(body);

                   foreach (var game in cart)
                   {
                       var key = PasswordGenerator.Generate(length: 20, allowed: Sets.Alphanumerics);
                       stringBulder.AppendLine($"{game.GameName} - {key}");
                   }

                   var ending = EmailEnding;
                   stringBulder.AppendLine(ending);

                   var message = new MailMessage();
                   message.To.Add(new MailAddress(email));
                   message.From = new MailAddress("letthisshiitwork@gmail.com");
                   message.Subject = EmailSubject;
                   message.Body = stringBulder.ToString();
                   message.IsBodyHtml = false;

                   using var smtp = new SmtpClient();
                   var credential = new NetworkCredential
                   {
                       UserName = "letthisshiitwork@gmail.com",
                       Password = "gwwzqgmgokhcrsee"
                   };

                   smtp.Credentials = credential;
                   smtp.Host = EmailHost;
                   smtp.Port = 587;
                   smtp.EnableSsl = true;
                   await smtp.SendMailAsync(message);

               })
               .GetAwaiter()
               .GetResult();
        }
    }
}
