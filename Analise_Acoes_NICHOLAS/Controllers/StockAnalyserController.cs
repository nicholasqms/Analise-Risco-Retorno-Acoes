using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Analise_Acoes_NICHOLAS.Models;
using Analise_Acoes_NICHOLAS.AuxiliaryF;
using Analise_Acoes_NICHOLAS.Data;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using X.PagedList;
using X.PagedList.Mvc;
using Microsoft.AspNetCore.Html;
using System.Web;

namespace Analise_Acoes_NICHOLAS.Controllers
{
    public class StockAnalyserController : Controller
    {
        private readonly Analise_AcoesContext _context;
        //Context for the Project
        public StockAnalyserController(Analise_AcoesContext context)
        {
            _context = context;
        }

        // GET: /StockAnalyser/
        public IActionResult Index()
        {
            Auxiliary auxiliar = new Auxiliary();
            
            List<Company> CompanyList = auxiliar.CarregaLista("companylist.csv");            
            
            ViewBag.Company = CompanyList;
           // ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
            return View("~/Views/StockAnalyser/Index.cshtml");
        }

        
        // GET Method to display the data in line chart model
        // GET: /StockAnalyser/CompareSymbols/
        public IActionResult CompareSymbols()
        {
            Auxiliary auxiliar = new Auxiliary(); //Create instance to use auxiliary methods.

            List<Company> CompanyList = auxiliar.CarregaLista("companylist.csv");
           

            ViewBag.Company = CompanyList;
            ViewBag.ChartType = "Closing Value";

            return View(); //View for Line chart with zooming
        }

        //Controller to Load the IndexChart ViewComponent as jQuery
        public async Task<IEnumerable<DataPoint>> IndexChart (string symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliary = new Auxiliary(); //Create instance to use auxiliary methods.            
            //var DataPoints = await auxiliary.GetDatapointsAsync("PIH", DateTime.Today.AddMonths(-2), DateTime.Today);
            var  DataPoints = await auxiliary.GetDatapointsAsync(symbol, start, end);            
            return DataPoints;
         }

        public async Task<IEnumerable<DataPoint>> ClosingChart(string symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliary = new Auxiliary(); //Create instance to use auxiliary methods.            
            //var DataPoints = await auxiliary.GetDatapointsAsync("PIH", DateTime.Today.AddMonths(-2), DateTime.Today);
            var DataPoints = await auxiliary.GetDatapointsAsync(symbol, start, end);
            return DataPoints;
        }


        public async Task<IEnumerable<DataPoint>> FeedbackChart(string symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliary = new Auxiliary(); //Create instance to use auxiliary methods.            
            //var DataPoints = await auxiliary.GetDatapointsAsync("PIH", DateTime.Today.AddMonths(-2), DateTime.Today);
            var DataPoints = await auxiliary.GetFeedbackAsync(symbol, start, end);
            return DataPoints;
        }


        public async Task<IEnumerable<DataPoint>> LogFeedbackChart(string symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliary = new Auxiliary(); //Create instance to use auxiliary methods.            
            //var DataPoints = await auxiliary.GetDatapointsAsync("PIH", DateTime.Today.AddMonths(-2), DateTime.Today);
            var DataPoints = await auxiliary.GetLogFeedbackAsync(symbol, start, end);
            return DataPoints;
        }

        public async Task<IEnumerable<DataPoint>> VolumeChart(string symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliary = new Auxiliary(); //Create instance to use auxiliary methods.            
            //var DataPoints = await auxiliary.GetDatapointsAsync("PIH", DateTime.Today.AddMonths(-2), DateTime.Today);
            var DataPoints = await auxiliary.GetVolumeAsync(symbol, start, end);
            return DataPoints;
        }

    }
}