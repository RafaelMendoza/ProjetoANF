using System.ComponentModel.DataAnnotations;

namespace ProjetoANFWeb.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorViewModel : BaseViewModel
    {
        #region Attributes
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "Nome do Autor")]
        public string Name { get; set; }

        /// <summary>
        /// 
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