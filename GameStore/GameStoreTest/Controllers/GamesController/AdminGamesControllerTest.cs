using GameStore.Data.Models;
using GameStore.Models;
using GameStore.Models.Games;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace GameStoreTest
{
    public class AdminGamesControllerTest
    {
        [Fact]
        public void AddGameShouldBeForAuthorizedUsersAndReturnView()
        {
            
              MyController<GameStore.Areas.Administration.Controllers.GamesController>
                  .Instance(controller => controller.WithUser("Administrator"))
                  .Calling(c => c.Add(With.Default<AddGameFormModel>()))
                  .ShouldHave()
                  .ActionAttributes(attributes => attributes
                      .RestrictingForAuthorizedRequests()
                      .RestrictingForHttpMethod(HttpMethod.Post));

        }

        [Fact]
        public void DeleteShouldMapToTheRightView()
        {
            MyRouting.Configuration()
            .ShouldMap(x => x.WithLocation("/Administration/Games/Delete/1")
            .WithUser("Adminsitrator"))
            .To<GameStore.Areas.Administration.Controllers.GamesController>(x => x.Delete(1));
        }

        [Fact]
        public void AddShouldReturnGenresAndPlatforms()
        {
            MyController<GameStore.Areas.Administration.Controllers.GamesController>
                 .Instance(controller => controller.WithUser("Administrator"))
                 .Calling(c => c.Add())
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests()
                     .RestrictingForHttpMethod(HttpMethod.Get))
                 .AndAlso()
                 .ShouldReturn()
                      .View(result => result
                          .WithModelOfType<AddGameFormModel>()); 
        }


        [Fact]
        public void PostAddShouldReturnViewWithTheSameModelWhenModelStateIsInvalid()
        {
            MyController<GameStore.Areas.Administration.Controllers.GamesController>
                      .Instance(controller => controller.WithUser("Administrator"))
                      .Calling(c => c.Add(With.Default<AddGameFormModel>()))
                      .ShouldHave()
                      .ActionAttributes(attributes => attributes
                      .RestrictingForAuthorizedRequests()
                      .RestrictingForHttpMethod(HttpMethod.Post))
                      .AndAlso()
                      .ShouldHave()
                      .InvalidModelState()
                      .AndAlso()
                      .ShouldReturn()
                      .View(result => result
                          .WithModelOfType<AddGameFormModel>());
        }

      

        [Fact]
        public void AllShouldReturnAllGames()
        {
            MyController<GameStore.Controllers.GamesController>
               .Instance()
               .WithData(new Game
               {
                   Id = 1,
                   Title = "Test",
                   Description = "Test",
                   Requirements = "test",
                   ImageUrl = "test",
                   TrailerUrl = "test",
                   Genre = new Genre
                   {
                       Id = 1,
                       Name = "Shooter"
                   },
                   Platform = new Platform
                   {
                       Id = 1,
                       Name = "PC"
                   },
                   PlatformId = 1,
                   GenreId = 1,
                   Guide = "sadasdasda",
                   Price = 20
               })
               .Calling(x => x.All(new AllGamesQueryModel
               {
                   CurrentPage = 1,
                   TotalGames = 3,
                   SearchTerm = "Title",
                   Sorting = 0,
                   Title = "Test"
               }))
               .ShouldReturn()
               .View(x => x.WithModelOfType<AllGamesQueryModel>());

        }
        [Fact]
        public void EditGameShouldBeOnlyForAuthorizedAndReturnView()
        {

            MyController<GameStore.Areas.Administration.Controllers.GamesController>
                .Instance(controller => controller.WithUser("Administrator"))
                .Calling(c => c.Edit(5))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Get));

        }

        [Fact]
        public void EditAddShouldReturnViewWithTheSameModelWhenModelStateIsInvalid()
        {
            MyController<GameStore.Areas.Administration.Controllers.GamesController>
                   .Instance(controller => controller.WithUser("Administrator"))
                   .Calling(c => c.Edit(5, new EditGameFormModel()))
                   .ShouldHave()
                   .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests()
                   .RestrictingForHttpMethod(HttpMethod.Post))
                   .AndAlso()
                   .ShouldHave()
                   .InvalidModelState()
                   .AndAlso()
                   .ShouldReturn()
                   .View(result => result
                       .WithModelOfType<EditGameFormModel>());
        }

      
    }
}
