using CloudeDemmo.Models;
using CloudeDemmo.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace CloudeDemmo.Controllers
{
    public class AlgoProjectController : Controller
    {
        private readonly IAlgoProjectRepository _algoProjectRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private List<string> _outCome;

        public AlgoProjectController(IAlgoProjectRepository algoProjectRepository) {
            _algoProjectRepository = algoProjectRepository;
        }
        public IActionResult Index()
        {
            return View(_algoProjectRepository.GetAll());
        }
        
        public IActionResult Details(int id ) {
            var model = _algoProjectRepository.GetById(id);
            var viewModel = new AlgoProjectDetailsViewModel
            {
                AlgoProject = model,
                PageTitle = model.ItemName + "的主页"

            };
            return View(viewModel);

        }

        public IActionResult TrainPage(int id) {
            var model = _algoProjectRepository.GetById(id);
            if (_outCome == null) {
                _outCome = new List<string>();
                _outCome.Add("准备开始"+id+"项目");
                RunPythonScript(model.ItemName);
            }
            
            var viewModel = new TrainPageViewModel
            {
                Result = _outCome,
                PageTitle = model.ItemName + "的训练主页",
                Id = model.Id

            };
            return View(viewModel);
            


        }
        //刷新功能
        public IActionResult Refresh(int id) {
            var model = _algoProjectRepository.GetById(id);
            return RedirectToAction("TrainPage", new { id = model.Id });
        }

        public  void RunPythonScript(string sArgName, string args = "", params string[] teps)
        {
            Process p = new Process();
            string filepath = @"E:\project\CloudeDemmo\CloudeDemmo\wwwroot\";
            var path = Path.Combine(filepath, "Project", sArgName, "main.py");
            //string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sArgName;// 获得python文件的绝对路径（将文件放在c#的debug文件夹中可以这样操作）
            //path = @"C:\Users\user\Desktop\test\" + sArgName;//(因为我没放debug下，所以直接写的绝对路径,替换掉上面的路径了)
            p.StartInfo.FileName = @"D:\ProgramData\Anaconda3\envs\pytorch\python.exe";//没有配环境变量的话，可以像我这样写python.exe的绝对路径。如果配了，直接写"python.exe"即可
            _outCome.Add(p.StartInfo.FileName);
            string sArguments = path;
            foreach (string sigstr in teps)
            {
                sArguments += " " + sigstr;//传递参数
            }

            sArguments += " " + args;
            _outCome.Add(sArguments);
            p.StartInfo.Arguments = sArguments;

            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardError = true;

            p.StartInfo.CreateNoWindow = true;
            _outCome.Add("程序开始");
            //程序异步运行 出错
            /*
            Process progressTest;
            using (progressTest = Process.Start(p.StartInfo))
            {

                // 异步获取命令行内容
                progressTest.BeginOutputReadLine();
                // 为异步获取订阅事件
                progressTest.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            }*/

            p.Start();
            p.BeginOutputReadLine();
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            //Console.ReadLine();
            p.WaitForExit();
        }
        //输出打印的信息
        public void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                //AppendText(e.Data + Environment.NewLine);
                _outCome.Add(e.Data);
            }
        }
        public delegate void AppendTextCallback(string text);
        public  void AppendText(string text)
        {
            Console.WriteLine(text);     //此处在控制台输出.py文件print的结果

        }
        /*
        /// <summary>
        /// 异步运行python的方法
        /// </summary>
        /// <param name="sArgName"></param>
        /// <param name="args"></param>
        /// <param name="teps"></param>
        public void RunPythonScript(string sArgName, string args = "", params string[] teps)
        {
            //string path = _hostingEnvironment.WebRootPath + sArgName + @"\main.py";//(因为我没放debug下，所以直接写的绝对路径,替换掉上面的路径了)
            string filepath = @"E:\project\CloudeDemmo\CloudeDemmo\wwwroot\Project\";
            var path = Path.Combine(filepath, "Project", sArgName,"main.py");
            //p.StartInfo.FileName = @"D:\Python\envs\python3\python.exe";    //没有配环境变量的话，可以像我这样写python.exe的绝对路径。如果配了，直接写"python.exe"即可
            string sArguments = path;


            foreach (string sigstr in teps)
            {
                sArguments += " " + sigstr;//传递参数
            }

            sArguments += " " + args;
            //Console.WriteLine(sArguments);

            _outCome.Add("");
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"D:\ProgramData\Anaconda3\python.exe";
            start.Arguments = sArgName;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardInput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;
            Process progressTest;
            using (progressTest = Process.Start(start))
            {

                // 异步获取命令行内容
                progressTest.BeginOutputReadLine();
                // 为异步获取订阅事件
                progressTest.OutputDataReceived += new DataReceivedEventHandler(outputDataReceived);
            }

        }

        //输出打印的信息

        public void outputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                
                AppendText(e.Data + Environment.NewLine);
                _outCome.Add(e.Data);
            }
            else
            {
                _outCome.Add("noooooooooooo");
            }

        }

        public delegate void AppendTextCallback(string text);
        public static void AppendText(string text)
        {
            Console.WriteLine(text);     //此处在控制台输出.py文件print的结果


        }
        */

    }
}
