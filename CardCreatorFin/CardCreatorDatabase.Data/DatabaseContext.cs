using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardCreatorDatabase.Domain;

namespace CardCreatorDatabase.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Type1> Types { get; set; }


        public DatabaseContext(): base("CardDatabase2"){
            Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
        }
        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        } */
    }
}
