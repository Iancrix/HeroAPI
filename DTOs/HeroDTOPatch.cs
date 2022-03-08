using System.ComponentModel.DataAnnotations;

namespace HeroAPI.Models
{
    public class HeroDTOPatch
    {
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string BackgroundStory { get; set; }
    }
}