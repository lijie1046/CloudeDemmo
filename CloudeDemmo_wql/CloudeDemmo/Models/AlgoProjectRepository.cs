using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.Models
{
    public class AlgoProjectRepository : IAlgoProjectRepository
    {
        private readonly AppDbContext _context;//数据库连接对象
        public AlgoProjectRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }//传入
        public AlgoProject Add(AlgoProject algoproject)
        {
            algoproject.Id = _context.AlgoProjects.Max(s => s.Id) + 1;
            _context.AlgoProjects.Add(algoproject);
            return algoproject;
        }

        public IEnumerable<AlgoProject> GetAll()
        {
            return _context.AlgoProjects;
        }

        public AlgoProject GetById(int id)
        {
            return _context.AlgoProjects.FirstOrDefault(a => a.Id == id);
        }
    }
}
