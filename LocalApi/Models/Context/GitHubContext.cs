using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalApi.Models.Context
{
    public class GitHubContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }

        public GitHubContext(DbContextOptions<GitHubContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlite("DataSource = db\\GitHub.db");
            }
        }
    }
}
