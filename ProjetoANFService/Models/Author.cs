using ProjetoANFService.Models.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace ProjetoANFService.Models
{
    /// <summary>
    /// Author entity.
    /// </summary>
    public class Author : Entity<int>
    {
        #region Attributes
        #endregion

        #region Properties

        /// <summary>
        /// Author's Id.
        /// </summary>
        [Required]
        public override int Id { get; set; }

        /// <summary>
        /// Author's Name.
        /// </summary>
        [Required]
        [Display(Name = "Nome do Autor"), MaxLength(60)]
        public string Name { get; set; }

        /// <summary>
        /// Indicates if this Author is active or not.
        /// </summary>
        [Required]
        [Display(Name = "Status")]
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