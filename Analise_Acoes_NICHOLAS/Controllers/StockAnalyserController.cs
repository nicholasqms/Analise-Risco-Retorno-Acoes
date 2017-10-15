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

        //// GET: /StockAnalyser/Buscacompany
        public async Task<IActionResult> BuscaCompany(string companySymbol, string searchString)
        {
            // Use LINQ to get list of companies.
            IQueryable<string> symbolQuery = from m in _context.Company
                                             orderby m.Symbol
                                             select m.Symbol;

            var companies = from m in _context.Company
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                companies = companies.Where(s => s.Nome.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(companySymbol))
            {
                companies = companies.Where(x => x.Symbol == companySymbol);
            }

            var companySymbolVM = new CompanyListModel();
            companySymbolVM.SList = new SelectList(await symbolQuery.Distinct().ToListAsync());
            companySymbolVM.CompanyList = await companies.ToListAsync();
            ViewBag.CompanyList = JsonConvert.SerializeObject(companySymbolVM.CompanyList, new JavaScriptDateTimeConverter());

            return View(companySymbolVM);
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

        public async Task<IEnumerable<DataPoint>> LineChart(string symbol, DateTime start, DateTime end)
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


        public async Task<IEnumerable<DataPoint>> VolumeChart(string symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliary = new Auxiliary(); //Create instance to use auxiliary methods.            
            //var DataPoints = await auxiliary.GetDatapointsAsync("PIH", DateTime.Today.AddMonths(-2), DateTime.Today);
            var DataPoints = await auxiliary.GetVolumeAsync(symbol, start, end);
            return DataPoints;
        }

    }
    }