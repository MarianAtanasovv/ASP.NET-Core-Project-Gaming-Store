using GameStore.Data.Models;
using GameStore.Services.Articles;
using GameStoreTest.Data;
using System;
using System.Linq;
using Xunit;

namespace GameStoreTest.Services
{
    public class ArticleServiceTests
    {
        [Fact]
        public void ArticleAddShouldAddArticleToDatabase()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var article = CreateArticle();

            var gameService = new ArticleService(data);

            //Act
            var result = gameService.Add(
                article.Id, 
                article.Title, 
                article.Content, 
                article.ImageUrl,
                article.TrailerUrl, 
                article.CreatedOn); 

            data.SaveChanges();

            //Assert
            Assert.Equal(1, data.Articles.Count());

        }

        [Fact]
        public void ArticleDetailsShouldReturnTheRightDetails()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var article = CreateArticle();
            data.Articles.Add(article);

            data.SaveChanges();
            var articleService = new ArticleService(data);
            var articleId = article.Id;

            //Act
            var articleData = data.Articles.Find(articleId);

            //Assert
            Assert.NotNull(articleData);

        }
      
        [Fact]
        public void ArticleDeleteShouldDeleteTheGame()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            var id = 1;

            data.Articles.Add(CreateArticle());

            data.SaveChanges();
            var articleService = new ArticleService(data);

            //Act
            var result = articleService.Delete(id);

            //Assert
            Assert.Equal(0, data.Articles.Count());

        }

        [Fact]
        public void RightDetailsShouldBeReturned()
        {
            var data = DatabaseMock.Instance;
            var article = CreateArticle();
            data.Articles.Add(article);
            data.SaveChanges();

            var articleService = new ArticleService(data);

            var result = articleService.Details(article.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public void IfDetailsNotFoundReturnNull()
        {
            var data = DatabaseMock.Instance;
            var article = CreateArticle();
            data.Articles.Add(article);
            data.Articles.Remove(article);
            data.SaveChanges();

            var articleService = new ArticleService(data);

            var result = articleService.Details(article.Id);

            Assert.Null(result);
        }


        [Fact]
        public void EditShouldEditTheArticle()
        {
                var data = DatabaseMock.Instance;
                var id = 1;
                var title = "Title";
                var content = "Description";
                var imageUrl = "sdasdasdas";
                var trailerUrl = "sdasdsadas";


            data.Articles.Add(CreateArticle());

            data.SaveChanges();
            var articleService = new ArticleService(data);
            var result = articleService.Edit(id, title, content, imageUrl, trailerUrl);
            Assert.True(result);

        }

        [Fact]
        public void EditShouldFindTheArticle()
        {
            var data = DatabaseMock.Instance;

            var article = CreateArticle();
            data.Articles.Add(article);

            data.SaveChanges();
            var articleService = new ArticleService(data);
            var articleId = article.Id;

            var gameData = data.Articles.Find(articleId);
            Assert.NotNull(gameData);

        }

        [Fact]
        public void AllArticlesShouldReturnAllGames()
        {
            var data = DatabaseMock.Instance;
            var article = CreateArticle();
            data.Articles.Add(article);

            data.SaveChanges();

            var articleService = new ArticleService(data);
            var articleData = articleService.AllArticles();

            Assert.Equal(data.Articles.Count(), articleData.Count());
        }

        [Fact]
        public void AllArticlesShouldReturn0IfNotFound()
        {
            var data = DatabaseMock.Instance;
            var article = CreateArticle();
            data.Articles.Add(article);
            data.Articles.Remove(article);

            data.SaveChanges();

            var articleService = new ArticleService(data);
            var articleData = articleService.AllArticles();

            Assert.Equal(0, articleData.Count());
        }

        [Fact]
        public void AllArticlesQuery()
        {
            var data = DatabaseMock.Instance;
            var article = CreateArticle();
            data.Articles.Add(article);
            data.SaveChanges();

            var articleService = new ArticleService(data);
            var articleData = articleService.All("Title", 0, 1, 5, "TestGame");

            Assert.NotNull(articleData);
        }

        [Fact]
        public void EditShouldReturnNullIfArticleNotFound()
        {
            var data = DatabaseMock.Instance;

            var article = CreateArticle();
            data.Articles.Add(article);

            data.SaveChanges();
            var articleService = new ArticleService(data);

            var articleData = data.Articles.Find(5);


            Assert.Null(articleData);
        }

       
        public static Article CreateArticle()
        {
            return new Article
            {
                Id = 1,
                Title = "Title",
                Content = "Description",
                CreatedOn = DateTime.UtcNow.ToString("r"),
                ImageUrl = "sdasdasdas",
                TrailerUrl = "sdasdsadas"
             
            };
        }
    }
}