using System.ComponentModel.DataAnnotations;
using cineVote.Models.Domain;

namespace cineVote.Models.DTO
{
    public class createNomineeModel
    {
        [Display(Name = "Nominee Name")]
        [Required(ErrorMessage = "Category Name is required")]
        public string NomineeName { get; set; }

        [Display(Name = "Category Discription")]
        [Required(ErrorMessage = "Category Description is required")]
        public string NomineeDescription { get; set; }
        public List<Dictionary<string, object>> Movies { get; set; }
    }
}
