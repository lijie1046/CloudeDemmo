using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.Models
{
    public class MockAlgoProjectRepository : IAlgoProjectRepository
    {
        private List<AlgoProject> _algoProjects;
 
        public MockAlgoProjectRepository() {
            if (_algoProjects == null) {
                InitializeAlgoProject();
            }
                    
        }


        private void InitializeAlgoProject()
        {
            _algoProjects = new List<AlgoProject>
            {
                new AlgoProject{Id = 1,ItemName = "Item1",Owner = "User1",ShortDescription = "It's shortdescription",LongDescription = "It's a longdescription" },
                new AlgoProject{Id = 2,ItemName = "Item2",Owner = "User2",ShortDescription = "It's shortdescription",LongDescription = "It's a longdescription" },
                new AlgoProject{Id = 3,ItemName = "DANN",Owner = "User1",ShortDescription = "It's shortdescription",LongDescription = "It's a longdescription" },
                new AlgoProject{Id = 4,ItemName = "AjaxTest",Owner = "User1",ShortDescription = "It's shortdescription",LongDescription = "It's a longdescription"}
            };
        }
       
        public AlgoProject Add(AlgoProject algoproject)
        {
            algoproject.Id = _algoProjects.Max(s => s.Id) + 1;
            _algoProjects.Add(algoproject);
            return algoproject;
        }

        public IEnumerable<AlgoProject> GetAll()
        {
            return _algoProjects;
        }

        public AlgoProject GetById(int id)
        {
            return _algoProjects.FirstOrDefault(s => s.Id == id);

        }
       
    }
}
