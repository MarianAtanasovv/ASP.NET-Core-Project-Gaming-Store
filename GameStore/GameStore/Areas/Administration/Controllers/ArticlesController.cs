using GameStore.Models.Articles;
using GameStore.Services.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Administration.Controllers
{
    public class ArticlesController : AdministrationController
    {
        
        private readonly IArticleService articles;

        public ArticlesController(IArticleService articles)
        {
            
            this.articles = articles;
        }

        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
       
        public IActionResult Add(AddArticleFormModel model)
        {
          
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.articles.Add(
                model.Id,
                model.Title,
                model.Content,
                model.ImageUrl,
                model.TrailerUrl,
                model.CreatedOn.ToString("r"));

            return RedirectToAction("All", "Articles", new { area = "" });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            
            var article = this.articles.Delete(id);

            if (article == 0)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = this.articles.Details(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(new EditArticleFormModel
            {
                Title = article.Title,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                TrailerUrl = article.TrailerUrl,
            });

        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, EditArticleFormModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.articles.Edit(
                id,
                model.Title,
                model.Content,
                model.ImageUrl,
                model.TrailerUrl);

            return RedirectToAction("Index", "Home");
        }
    }
}
