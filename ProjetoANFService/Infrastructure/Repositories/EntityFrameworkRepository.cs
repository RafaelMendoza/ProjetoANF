using LMB.PropertyExtension;
using ProjetoANFCore.Entities;
using ProjetoANFCore.Extensions;
using ProjetoANFService.Infrastructure.Contracts;
using ProjetoANFService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoANFService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class EntityFrameworkRepository<TContext> : EntityFrameworkReadOnlyRepository<TContext>, IRepository
        where TContext : DbContext
    {
        #region Attributes
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors

        public EntityFrameworkRepository(TContext _context)
            :base(_context)
        {

        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="createdBy"></param>
        public virtual void Create<T>(T entity, string createdBy = null)
            where T : class, IEntity
        {
            entity.CreatedDate = DateTime.Now;
            //TODO: Teste para criação de entidade. Este campo deve ser substituido por um Id do usuário logado.
            entity.CreatedBy = createdBy ?? "Anonymous";
            context.Set<T>().Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="modifiedBy"></param>
        public virtual void Update<T>(T entity, string modifiedBy = null)
            where T : class, IEntity
        {
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = modifiedBy;
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public virtual void Delete<T>(T entity)
            where T : class, IEntity
        {
            var dbSet = context.Set<T>();

            if(context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public virtual void Delete<T>(object id)
            where T : class, IEntity
        {
            var entity = context.Set<T>().Find(id);
            Delete(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void SaveChanges()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async virtual Task SaveChangesAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }
            catch (DbUpdateConcurrencyException e)
            {
                ThrowEnhancedValidationException(e);
            }
            catch (DbUpdateException dbUpdtEx)
            {
                ThrowEnhancedValidationException(dbUpdtEx);
            }
            catch(UpdateException updtEx)
            {
                ThrowEnhancedValidationException(updtEx);
            }
            catch(Exception e)
            {
                ThrowEnhancedValidationException(e);
            }

            //return Task.FromResult(0);
        }

        #endregion

        #region Protected

        //protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        //{
        //    var errorMessages = e.EntityValidationErrors
        //        .SelectMany(x => x.ValidationErrors)
        //        .Select(x => x.ErrorMessage);

        //    var fullErrorMessage = string.Join("; ", errorMessages);
        //    var exceptionMessage = string.Concat(e.Message, "Validation Errors: ", fullErrorMessage);
        //    throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        //}

        protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        {
            var allErrors = new List<string>();

            var errorMessages = e.EntityValidationErrors
                .SelectMany(x => x.ValidationErrors)
                .Select(x => x.ErrorMessage);

            foreach (var validationResult in e.EntityValidationErrors)
            {
                var entityName = (validationResult.Entry.Entity is IEntity)
                    ? ((IEntity) validationResult.Entry.Entity).EntityName
                    : validationResult.Entry.Entity.GetType().Name;

                allErrors.Add(string.Join(". ", validationResult.ValidationErrors
                    .Select(error => string.Format("Entidade: [{0}]. Campo: [{1}]. Erro: [{2}].",
                                     entityName, validationResult.Entry.GetDisplayAttributeValue(error.PropertyName), errorMessages))));
            }

            throw new ApiException("A validação falhou para uma ou mais entidades.", string.Join(". ", allErrors));

        }


        protected virtual void ThrowEnhancedValidationException(DbUpdateException e)
        {
            var sqlEx = e.SqlException();

            if(sqlEx != null)
            {
                throw sqlEx;
            }

            var entities = e.Entries.Where(entry => (entry.Entity is IEntity))
                .Select(entry => ((IEntity)entry.Entity).EntityName);

            if(entities == null || !entities.Any())
            {
                entities = e.Entries.SelectMany(entry => e.Entries)
                    .Select((entry => entry.Entity.GetType().Name));

                var errorComplement = "Verifique os dados e tente novamente, caso o problema persista, contate o administrador do site.";
                var errorMessage = (entities != null && entities.Count() > 1)
                    ? String.Format($"A validação falhou para as seguintes entidades: [{0}] : [{1}]", String.Join(" | ", entities, errorComplement))
                    : String.Join($"A validação falhou para as seguintes entidades: [{0}] : [{1}]", entities.SingleOrDefault(), errorComplement);

                throw new ApiException("Erro na gravação dos dados.", errorMessage);
            }
        }

        protected virtual void ThrowEnhancedValidationException(UpdateException e)
        {
            var sqlEx = e.SqlException();

            if (sqlEx != null)
            {
                throw sqlEx;
            }

            var entities = e.StateEntries.Where(entry => (entry.Entity is IEntity))
                .Select(entry => ((IEntity)entry.Entity).EntityName);

            if (entities == null || !entities.Any())
            {
                entities = e.StateEntries.SelectMany(entry => e.StateEntries)
                    .Select((entry => entry.Entity.GetType().Name));

                var errorComplement = "Verifique os dados e tente novamente, caso o problema persista, contate o administrador do site.";
                var errorMessage = (entities != null && entities.Count() > 1)
                    ? String.Format($"A validação falhou para as seguintes entidades: [{0}] : [{1}]", String.Join(" | ", entities, errorComplement))
                    : String.Join($"A validação falhou para as seguintes entidades: [{0}] : [{1}]", entities.SingleOrDefault(), errorComplement);

                throw new ApiException("Erro na gravação dos dados.", errorMessage);
            }
        }

        protected virtual void ThrowEnhancedValidationException(Exception e)
        {
            throw new ApiException("Erro", e.Message);
        }

        #endregion

        #endregion

        #region Events
        #endregion
    }
}