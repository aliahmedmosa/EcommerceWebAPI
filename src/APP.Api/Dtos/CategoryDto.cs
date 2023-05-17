using System.ComponentModel.DataAnnotations;

namespace APP.Api.Dtos
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
