using System.ComponentModel.DataAnnotations;

namespace ProjetoANFWeb.Models
{
    public class GenreViewModel : BaseViewModel
    {
        /// <summary>
        /// Genre Id.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Genre name.
        /// </summary>
        [Required, MaxLength(50), MinLength(3)]
        [Display(Name = "Nome do Gênero")]
        public string Name { get; set; }

        /// <summary>
        /// Indicates if this genre is active or not.
        /// </summary>
        [Required]
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
    }
}