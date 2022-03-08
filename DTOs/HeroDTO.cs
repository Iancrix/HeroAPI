using System.ComponentModel.DataAnnotations;

namespace HeroAPI.Models
{
    public class HeroDTO
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string BackgroundStory { get; set; }

        public List<HeroSidekick> Sidekicks { get; set; } = new List<HeroSidekick>();
    }
}