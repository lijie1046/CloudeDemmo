using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.Models
{
    public interface IDatasetRepository
    {
        Dataset GetById(int id);
        Dataset Add(Dataset dataset);
        IEnumerable<Dataset> GetAll();
        Dataset Delet(int id);
    }
}
