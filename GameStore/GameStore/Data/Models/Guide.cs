using GamingWebAppDb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace GameStore.Data.Models
{
    public class Guide
    {
        public int Id { get; set; }

        [Required]
        public string StartingTips { get; set; }

        [Required]
        public string Walkthrough { get; set; }

        [Required]
        public string EasterEgg { get; set; }

        [Required]
        public string FAQ { get; set; }

        public Game Game { get; set; }

        public int GameId { get; set; }
    }
}
