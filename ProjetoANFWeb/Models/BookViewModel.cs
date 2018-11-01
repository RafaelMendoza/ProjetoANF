using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoANFWeb.Models
{
    public class BookViewModel : BaseViewModel
    {
        /// <summary>
        /// Book Id.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Book Title.
        /// </summary>
        [Required, MaxLength(80)]
        [Display(Name = "Título")]
        public string Title { get; set; }

        /// <summary>
        /// Date when this book was released.
        /// </summary>
        [Required]
        [Display(Name = "Data de Publicação")]
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Book's Language.
        /// </summary>
        [Required, MaxLength(50)]
        [Display(Name = "Idioma")]
        public string Language { get; set; }

        /// <summary>
        /// Book's description.
        /// </summary>
        [Required, MaxLength(200)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Book's Genre.
        /// </summary>
        [Required]
        [Display(Name = "Gênero")]
        public GenreViewModel Genre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Book's Author.
        /// </summary>
        [Required]
        [Display(Name = "Autor")]
        public AuthorViewModel Author { get; set; }

        /// <summary>
        /// Indicates if this book is active or not.
        /// </summary>
        [Required]
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
    }
}