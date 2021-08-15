using GameStore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Models.Games;
using GameStore.Data.Models;
using GameStore.Services.Games;

namespace GameStore.Models.Games
{
    using static DataConstants;
   
    public class EditGameFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(GameTitleMaxLength, MinimumLength = GameTitleMinLength, ErrorMessage = "Game title should be between {1} and {2} characters long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(GameDescriptionsMaxLenght, MinimumLength = GameDescriptionMinLength, ErrorMessage = "Game description should be between {1} and {2} characters long.")]
        public string Description { get; set; }

        [Required]
        [StringLength(GameRequirementsMaxLenght, MinimumLength = GameRequirementsMinLength, ErrorMessage = "Game requirements should be between {2} and {1} characters long.")]
        public string Requirements { get; set; }

        [Required]
        [Range(0.0, GamePriceMaxValue, ErrorMessage = "Game price should be between {1} and {2}$.")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Trailer URL")]
        public string TrailerUrl { get; set; }

        public IEnumerable<GameGenreServiceModel> Genres { get; set; }

        public IEnumerable<GamePlatformServiceModel> Platforms { get; set; }

        public string Guide { get; set; }

        public int GenreId { get; set; }

        public int PlatformId { get; set; }


       

    }
}
