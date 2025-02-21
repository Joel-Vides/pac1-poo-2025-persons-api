using Microsoft.EntityFrameworkCore;
using Persons.API.Database.Entities;

namespace Persons.API.Database
{
    public class PersonsDBContext : DbContext
    {
        public PersonsDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
    }

}