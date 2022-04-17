using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.Models
{
    public class DatabaseDbContext:DbContext
    {
        public DbSet<Database> databases { get; set; }
    }
}
