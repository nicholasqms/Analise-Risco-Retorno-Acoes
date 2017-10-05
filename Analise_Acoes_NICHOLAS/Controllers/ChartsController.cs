using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Analise_Acoes_NICHOLAS.Models;
using Newtonsoft.Json;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Analise_Acoes_NICHOLAS.Controllers
{
    public class ChartsController : Controller
    {
        // GET: /Charts/
        public IActionResult Index()
        {
            List<DataPoint> dataPoints = new List<DataPoint>{
                new DataPoint(10, 22),
                new DataPoint(20, 36),
                new DataPoint(30, 42),
                new DataPoint(40, 51),
                new DataPoint(50, 46),
            };

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }
        public IActionResult ShowChart()
        {
            return View();
        }
    }
}
