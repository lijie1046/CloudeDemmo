using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CloudeDemmo.Models;

namespace CloudeDemmo.Data
{
    public class CloudeDemmoContext : DbContext
    {
        public CloudeDemmoContext (DbContextOptions<CloudeDemmoContext> options)
            : base(options)
        {
        }

        public DbSet<CloudeDemmo.Models.Dataset> Dataset { get; set; }
    }
}
