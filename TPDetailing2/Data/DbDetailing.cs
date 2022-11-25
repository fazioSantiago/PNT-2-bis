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

        public DbSet<TPDetailing2.Models.Empleado> Empleado { get; set; }

        public DbSet<TPDetailing2.Models.Servicio> Servicio { get; set; }

        public DbSet<TPDetailing2.Models.Turno> Turno { get; set; }
    }
