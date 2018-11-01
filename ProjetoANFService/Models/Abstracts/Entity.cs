using System;
using ProjetoANFService.Models.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoANFService.Models.Abstracts
{
    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
        object IEntity.Id
        {
            get { return this.Id; }
        }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] RowVersion { get; set; }
        [NotMapped]
        public string EntityName { get; set; }
    }
}