using ProjetoANFService.Models.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoANFService.Models
{
    /// <summary>
    /// Book entity.
    /// </summary>
    public class Book : Entity<int>
    {

        #region Attributes
        #endregion

        #region Properties

        /// <summary>
        /// Book Id.
        /// </summary>
        [Required]
        public override int Id { get; set; }

        /// <summary>
        /// Book Title.
        /// </summary>
        [Required, MaxLength(80)]
        public string Title { get; set; }

        /// <summary>
        /// Date when this book was released.
        /// </summary>
        [Required]
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Book's Language.
        /// </summary>
        [Required, MaxLength(50)]
        public string Language { get; set; }

        /// <summary>
        /// Book's description.
        /// </summary>
        [Required, MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public int GenreId { get; set; }

        /// <summary>
        /// Book's Genre.
        /// </summary>
        public virtual Genre Genre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public int AuthorId { get; set; }

        /// <summary>
        /// Book's Author.
        /// </summary>
        public virtual Author Author { get; set; }

        /// <summary>
        /// Indicates if this book is active or not.
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        #endregion

        #region Constants
        #endregion

        #region Constructors
        #endregion

        #region Actions

        #region Get
        #endregion

        #region Post
        #endregion

        #region Put
        #endregion

        #endregion

        #region Methods

        #region Private
        #endregion

        #region Public
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}