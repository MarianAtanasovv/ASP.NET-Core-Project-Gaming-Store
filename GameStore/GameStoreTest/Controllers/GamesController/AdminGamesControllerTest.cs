﻿using FluentAssertions;
using GameStore.Controllers;
using GameStore.Models;
using GameStore.Models.Games;
using GameStore.Services.Games;
using Moq;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GameStore.Areas.Administration.Controllers;
using GameStore;
using Microsoft.EntityFrameworkCore;
using GameStore.Data.Models;

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

        [Theory]
        [InlineData(1, "Test Title", "TestTestRequirementsDescription", "TestRequirementsTestRequirementsTestRequirements", "TestTestRequirementsGuide", 20.00,
            "https://i.stack.imgur.com/GsDIl.jpg", "https://www.youtube.com/watch?v=_LcT5sLwmiw")]
        public void PostAddShouldReturnRedirectAndSaveTheGame(
              int id,
                string title,
               string description,
               string requirements,
               string guide,
                decimal price,
               string imageUrl,
               string trailerUrl)
          => MyController<GameStore.Areas.Administration.Controllers.GamesController>
              .Instance(x => x.WithUser("Administrator"))

              .Calling(c => c.Add(new AddGameFormModel
              {
                  Id = id,
                  Title = title,
                  Description = description,
                  Requirements = requirements,
                  Guide = guide,
                  Price = price,
                  ImageUrl = imageUrl,
                  TrailerUrl = trailerUrl

              }))
              .ShouldReturn()
              .Redirect(result => result
                  .To<GameStore.Areas.Administration.Controllers.GamesController>(c => c.All(With.Any<AllGamesQueryModel>())));


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

        [Theory]
        [InlineData(1, "Test Title", "TestTestRequirementsDescription", "TestRequirementsTestRequirementsTestRequirements", "TestTestRequirementsGuide", 20.00, 
            "https://i.stack.imgur.com/GsDIl.jpg", "https://www.youtube.com/watch?v=_LcT5sLwmiw")]

        public void EditShouldEditGameAndReturnView(
                  int id,
                    string title,
                   string description,
                   string requirements,
                   string guide,
                    decimal price,
                   string imageUrl,
                   string trailerUrl)
        {

              MyController<GameStore.Areas.Administration.Controllers.GamesController>
                  .Instance(x => x.WithUser("Administrator"))

                  .Calling(c => c.Edit(5, new EditGameFormModel
                  {
                      Id = id,
                      Title = title,
                      Description = description,
                      Requirements = requirements,
                      Guide = guide,
                      Price = price,
                      ImageUrl = imageUrl,
                      TrailerUrl = trailerUrl

                  }))
                  .ShouldReturn()
                  .Redirect(result => result
                      .To<GameStore.Areas.Administration.Controllers.GamesController>(c => c.All(With.Any<AllGamesQueryModel>())));
        }

    }
}
