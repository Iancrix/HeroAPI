using System.ComponentModel.DataAnnotations;

namespace HeroAPI.Models
{
    public class Hero
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(maximumLength: 120, ErrorMessage = "Attribute {0} cannot have more than {1} characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 120, ErrorMessage = "Attribute {0} cannot have more than {1} characters")]
        public string LastName { get; set; }

        [Required]
        [Range(0, 100)]
        public int Age { get; set; }

        [Required]
        public string HeroName { get; set; }
        public string ImageUrl { get; set; }
        public string BackgroundStory { get; set; }

        public List<HeroSidekick> Sidekicks { get; set; }
    }
}