using GameStore.Data;
using GameStore.Data.Models;
using GameStore.Services.Carts;
using MlkPwgen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Orders
{
    using static DataConstants;

    public class OrderingService : IOrderingService
    {
        private readonly ApplicationDbContext data;
        private readonly ICartService cart;


        public OrderingService(ApplicationDbContext data, ICartService cart)
        {
            this.data = data;
            this.cart = cart;
        }

        public int CreateOrder(string userId)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
               
            };

            this.data.Orders.Add(order);
            this.data.SaveChanges();

            return order.Id;
            
        }

        public void FinishOrder(string userId)
        {
            var cartGames = this.data.CartItems.Where(x => x.UserId == userId).ToList();

            var id = this.data.Orders.Where(x => x.UserId == userId).OrderBy(x => x.OrderDate).Select(x => x.Id).LastOrDefault();

            

            foreach (var game in cartGames)
            {
                var gameId = game.GameId;
                var quantity = game.Quantity;

                data.OrderGames.Add(new OrderGame
                {
                    OrderId = id,
                    GameId = gameId,
                    Quantity = quantity,
                });

                var itemCart = data.CartItems
                    .Where(p => p.UserId == userId && p.GameId == gameId)
                    .FirstOrDefault();

              

                data.CartItems.Remove(itemCart);
                data.SaveChanges();
            }

            
        }



        public int GetOrderId(string userId)
        {
            var order = this.data.Orders.Where(x => x.UserId == userId).Select(x => x.Id).LastOrDefault();

            return order;
        }

        public void SendKeyAsync(string userId)
        {

            Task
                .Run(async () =>
                {
                    var email = this.data.Users.Where(x => x.Id == userId).Select(x => x.Email).FirstOrDefault().ToString();

                    var cart = this.cart.UsersCart(userId).ToList();

                    var stringBulder = new StringBuilder();

                    var body = "$" + EmailBody;
                    stringBulder.AppendLine(body);

                    foreach (var game in cart)
                    {
                        var key = PasswordGenerator.Generate(length: 20, allowed: Sets.Alphanumerics);
                        stringBulder.AppendLine($"{game.GameName} - {key}");
                    }

                    var ending = EmailEnding;
                    stringBulder.AppendLine(ending);

                    var message = new MailMessage();
                    message.To.Add(new MailAddress("marian3455@gmail.com"));  // replace with valid value 
                    message.From = new MailAddress("letthisshiitwork@gmail.com");  // replace with valid value
                    message.Subject = EmailSubject;
                    message.Body = stringBulder.ToString();
                    message.IsBodyHtml = false;

                    using var smtp = new SmtpClient();
                    var credential = new NetworkCredential
                    {
                        UserName = EmailUsernameCredential,  // replace with valid value
                         Password = EmailPasswordCredential // replace with valid value
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

