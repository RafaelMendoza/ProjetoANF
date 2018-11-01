using ProjetoANFService.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoANFService.Models
{
    /// <summary>
    /// Genre entity.
    /// </summary>
    public class Genre : Entity<int>
    {

        #region Attributes
        #endregion

        #region Properties

        /// <summary>
        /// Genre Id.
        /// </summary>
        [Required]
        public override int Id { get; set; }

        /// <summary>
        /// Genre name.
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Indicates if this genre is active or not.
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