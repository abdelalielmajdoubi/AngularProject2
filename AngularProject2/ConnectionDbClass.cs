using AngularProject2.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject2
{
    public class ConnectionDbClass : DbContext
    {
        public ConnectionDbClass(DbContextOptions<ConnectionDbClass> options) : base(options)
        {

        }

        public DbSet<Utilisateur> Users { get; set; }
    }
}
