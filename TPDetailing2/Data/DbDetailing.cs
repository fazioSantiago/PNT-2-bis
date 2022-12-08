using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TPDetailing2.Models;

    public class DbDetailing : IdentityDbContext
    {
        public DbDetailing (DbContextOptions<DbDetailing> options)
            : base(options)
        {
        }

    public DbSet<TPDetailing2.Models.Cliente> Cliente { get; set; } = default!;

        public DbSet<TPDetailing2.Models.Servicio> Servicio { get; set; }

        public DbSet<TPDetailing2.Models.Turno> Turno { get; set; }
    }

    //public class DetailingContext : DbContext
    //{
    //    public virtual DbSet<Cliente> Clientes { get; set; }
    //    public virtual DbSet<Turno> Turnos { get; set; }
    //    public virtual DbSet<Servicio> Servicios { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        //Santi
    //        optionsBuilder.UseSqlServer("Data Source=DESKTOP-5MUUJS2\\SQLEXPRESS; Initial Catalog = ORTDetailingF; Integrated Security = true");

    //        //javier
    //        //optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=ORTDetailing; Integrated Security=true;");

    //    }
    //}
