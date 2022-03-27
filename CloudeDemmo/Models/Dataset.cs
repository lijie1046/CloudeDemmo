using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.Models
{
    public class Dataset
    {
        public int Id { get; set; }
        public string DataName { get; set; }
        public string Owner { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }


    }
}
