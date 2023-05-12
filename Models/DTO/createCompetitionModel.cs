using System.ComponentModel.DataAnnotations;
using cineVote.Models.Domain;

namespace cineVote.Models.DTO
{
    public class createCompetitionModel
    {
        [Display(Name = "Name of the Competition")]
        [Required(ErrorMessage ="First Name is required")]
        public string? Name { get; set; }

        [Display(Name = "Status of the Competition")]
        [Required(ErrorMessage = "Status is required")]
        public Boolean isPublic{ get; set; }

        [Display(Name = "Category of the Competition")]
        [Required(ErrorMessage = "Category is required")]
        public Category? category { get; set; }

        [Display(Name = "Start Date of the Competition")]
        [Required(ErrorMessage = "startDate is required")]
        public string startDate { get; set; }

        [Display(Name = "End Date of the Competition")]
        [Required(ErrorMessage = "endDate is required")]
        public string endDate { get; set; }

        [Display(Name ="List of nominiees")]
        [Required(ErrorMessage = "List of Nominees is required")]
        public List<Nominee> nominees { get; set; }

    }
}