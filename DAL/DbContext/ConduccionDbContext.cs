using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.DAL.DbContext
{
    using Microsoft.EntityFrameworkCore;
    using PracticaDTOFluentVaidation.DAL.Entities;

    public class ConduccionDbContext : DbContext
    {
        public ConduccionDbContext(DbContextOptions<ConduccionDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Matriculas> Matriculas { get; set; }

        public virtual DbSet<Conductores> Conductores { get; set; }

        public virtual DbSet<Sanciones> Sanciones { get; set; }
    }
}
