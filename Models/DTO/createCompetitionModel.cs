using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Display(Name = "Start Date of the Competition")]
        [Required(ErrorMessage = "startDate is required")]
        public DateTime startDate { get; set; }

        [Display(Name = "End Date of the Competition")]
        [Required(ErrorMessage = "endDate is required")]
        public DateTime endDate { get; set; }



        [Display(Name = "Category of the Competition")]
        [Required(ErrorMessage = "Category is required")]
        public string category { get; set; }

        public string NomineeDBId {get;set;}



        [NotMapped]
        public List<Category> ? categoryList { get; set; }

        
        [NotMapped]
        public List<Dictionary<string, object>> nominees { get; set; }
        
    }
}