using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.Models
{
    public interface IAlgoProjectRepository
    {
        AlgoProject GetById(int id);
        AlgoProject Add(AlgoProject algoproject);
        IEnumerable<AlgoProject> GetAll();
       
    }
}
