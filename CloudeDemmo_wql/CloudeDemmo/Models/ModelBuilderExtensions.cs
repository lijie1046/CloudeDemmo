using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CloudeDemmo.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlgoProject>().HasData(
               new AlgoProject { Id = 1, ItemName = "Item1", Owner = "User1", ShortDescription = "It's shortdescription", LongDescription = "It's a longdescription" },
               new AlgoProject { Id = 2, ItemName = "Item2", Owner = "User2", ShortDescription = "It's shortdescription", LongDescription = "It's a longdescription" },
               new AlgoProject { Id = 3, ItemName = "DANN", Owner = "User1", ShortDescription = "It's shortdescription", LongDescription = "It's a longdescription" },
               new AlgoProject { Id = 4, ItemName = "AjaxTest", Owner = "User1", ShortDescription = "It's shortdescription", LongDescription = "It's a longdescription" }
               );
            modelBuilder.Entity<Dataset>().HasData(
                new Dataset { Id = 1, DataName = "西储数据集", Owner = "用户1", ShortDescription = "凯斯西储大学轴承故障数据", LongDescription = "无" },
                new Dataset { Id = 2, DataName = "数据2", Owner = "用户1", ShortDescription = "测试", LongDescription = "无" }
                );
        }
    }
}
