using System.ComponentModel.DataAnnotations;

namespace cineVote.Models.DTO
{
    public class CreateCategoryModel
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Discription")]
        [Required(ErrorMessage = "Category Description is required")]
        public string CategoryDescription { get; set; }
    }
}
