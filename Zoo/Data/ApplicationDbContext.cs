using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Zoo.Models;

namespace Zoo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        #region Public Constructors

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<Dragon> Dragons { get; set; }
        public DbSet<Room> Rooms { get; set; }

        #endregion Public Properties
    }
}