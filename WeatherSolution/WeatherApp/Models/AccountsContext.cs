namespace WeatherApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AccountsContext : DbContext
    {
        
        public AccountsContext()
            : base("name=AccountsContext")           
        {
        }

        public AccountsContext(string connectionString) {
            base.Database.Connection.ConnectionString = connectionString;
        }

        public virtual DbSet<Account> Accounts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {          
        }
    }
}
