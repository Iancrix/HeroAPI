using System.ComponentModel.DataAnnotations;

namespace HeroAPI.Models
{
    public class HeroSidekick
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(maximumLength: 120, ErrorMessage = "Attribute {0} cannot have more than {1} characters")]
        public string SidekickName { get; set; }
        public string ImageUrl { get; set; }

        public Hero Hero { get; set; }
    }
}