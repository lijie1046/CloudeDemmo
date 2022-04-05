using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.ViewModels
{
    public class DatasetCreateViewModel
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<IFormFile> Sets { get; set; }
    }

}
