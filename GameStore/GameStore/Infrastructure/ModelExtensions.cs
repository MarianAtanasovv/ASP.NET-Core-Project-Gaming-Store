using GameStore.Services.Articles.Models;
using GameStore.Services.Games.Models;

namespace GameStore.Infrastructure
{
   

    public static class ModelExtensions
    {
        public static string GetInformationGame(this IGameModel game)
        {
            return game.Title + "-" + game.Platform + "-" + game.Genre; 
        }

        public static string GetInformationArticle(this IArticleModel article)
        {
            return article.Title;
        }
    }
}
