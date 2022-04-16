using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.Models
{
    public class DatasetRepository : IDatasetRepository
    {
        private readonly AppDbContext _context;
        public DatasetRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public Dataset Add(Dataset dataset)
        {
            
            dataset.Id = _context.Datasets.Max(s => s.Id) + 1;
            _context.Datasets.Add(dataset);
            return dataset;
        }

        public Dataset Delet(int id)
        {
            var dataset = _context.Datasets.FirstOrDefault(s => s.Id == id);
            if (dataset != null)
            {
                _context.Datasets.Remove(dataset);
            }
            //并在项目中删除文件

            return dataset;
        }

        public IEnumerable<Dataset> GetAll()
        {
            return _context.Datasets;
        }

        public Dataset GetById(int id)
        {
            return _context.Datasets.FirstOrDefault(d => d.Id == id);
        }
    }
}
