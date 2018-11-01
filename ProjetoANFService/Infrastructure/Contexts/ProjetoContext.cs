using ProjetoANFService.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using static ProjetoANFService.Infrastructure.Mapping.ProjetoMapping;

namespace ProjetoANFService.Infrastructure.Contexts
{
    public class ProjetoContext : DbContext
    {
        #region Attributes
        #endregion

        #region Properties

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }


        #endregion

        #region Constants
        #endregion

        #region Constructors

        public ProjetoContext()
            : base("ProjetoContext")
        {
            Database.SetInitializer<ProjetoContext>(null);
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
        #endregion

        #region Protected

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //Remove pluralizing table names when using Entity Framework Migrations and Code First.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new AuthorMap());
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new GenreMap());
        }

        #endregion

        #endregion

        #region Events
        #endregion
    }
}