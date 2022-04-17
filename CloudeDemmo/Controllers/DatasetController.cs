using CloudeDemmo.Models;
using CloudeDemmo.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CloudeDemmo.Controllers
{
    public class DatasetController : Controller
    {
        private readonly IDatasetRepository _datasetRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DatasetController(IDatasetRepository datasetRepository, IWebHostEnvironment hostEnvironment)
        {
            _datasetRepository = datasetRepository;//构造函数初始化model，model再构造函数初始化数据库
            _hostingEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_datasetRepository.GetAll());
        }

        public ObjectResult Get(int id) {
            return new ObjectResult(_datasetRepository.GetById(id));
        }

        //[Route("test/{id?}")]
        public IActionResult Details(int id)
        {
            var model = _datasetRepository.GetById(id);
            if (model == null) return View("DatasetNotFound");

            var viewModel = new DatasetDetailsViewModel
            {
                Dataset = model,
                PageTitle = model.DataName+"的主页"
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()//显示Create页面的函数
        {
            Console.WriteLine("===========");
            return View();
        }

        [HttpPost]
        public IActionResult Create(DatasetCreateViewModel model)//和Create进行交互的函数
        {
            Console.WriteLine("===========");
            if (ModelState.IsValid)
            {
                Console.WriteLine("===========");
                string uniqueFileName = null;

                var dataset = new Dataset
                {
                    DataName = model.Name,
                    Owner = model.Owner,
                    ShortDescription = model.ShortDescription,
                    LongDescription = model.LongDescription,
                };//接收网页传来的数据

                // 处理多文件上传
                if (model.Sets != null && model.Sets.Count > 0)
                {
                    foreach (var data in model.Sets)
                    {
                        ProcessUpload(data ,model.Name);
                    }
                }

                var newDataset = _datasetRepository.Add(dataset);//把网页传输的数据添加到数据库里面
                return RedirectToAction("Index");
            }
            return View();
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file"> 传输文件</param>
        /// <param name="dataname"> 数据集的名字</param>
        /// <returns></returns>
        private string ProcessUpload(IFormFile file,string dataname )
        {
            var uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "Dataset", dataname);
            var uniqueFileName = file.FileName;


            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(uploadDir))
                {
                    // Create the directory it does not exist.
                    Directory.CreateDirectory(uploadDir);
                    saveFileFromStream(file, uploadDir, uniqueFileName);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
            return uniqueFileName;
        }

        /// <summary>
        /// 多线程写入文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="uploadDir"></param>
        /// <param name="uniqueFileName"></param>
        private async void saveFileFromStream(IFormFile file, string uploadDir, string uniqueFileName)
        {
            await Task.Run(() => {
                using (var stream = new FileStream(Path.Combine(uploadDir, uniqueFileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            });
        }


    }
}
