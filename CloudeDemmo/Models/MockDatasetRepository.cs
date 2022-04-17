using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.Models
{
    public class MockDatasetRepository : IDatasetRepository
    {
        private List<Dataset> _datasets;

        public MockDatasetRepository()//构造函数调用InitializeDataset()初始化数据库
        {
            if (_datasets == null)
            {
                InitializeDataset();
            }
        }

        private void InitializeDataset()//初始化数据库
        {
            _datasets = new List<Dataset>
            {
                new Dataset{ Id = 1,DataName="西储数据集",Owner="用户1",ShortDescription="凯斯西储大学轴承故障数据",LongDescription="无"},
                new Dataset{ Id = 2,DataName="数据2",Owner="用户1",ShortDescription="测试",LongDescription="无"}
            };
        }

        
        public Dataset Add(Dataset dataset)//往dataset列表里面添加新的数据
        {
            dataset.Id = _datasets.Max(s => s.Id) + 1;
            _datasets.Add(dataset);
            return dataset;
        }

        public Dataset Delet(int id)
        {
            var dataset = _datasets.FirstOrDefault(s => s.Id == id);//Lambda表达式，找到第一个ID位id的dataset，然后噶掉。
            if (dataset != null)
            {
                _datasets.Remove(dataset);
            }
            //并在项目中删除文件

            return dataset;
        }

        public Dataset GetById(int id)
        {
            return _datasets.FirstOrDefault(s => s.Id == id);//Lambda表达式，返回第一个ID位id的dataset
        }

        public IEnumerable<Dataset> GetAll()
        {
            return _datasets;
        }
    }
}
