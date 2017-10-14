﻿using System;
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
    public class CompanyListController : Controller
    {
        private readonly Analise_AcoesContext _context;
        //Context for the Project
        public CompanyListController(Analise_AcoesContext context)
        {
            _context = context;
        }

        // GET: /CompanyList/
        public IActionResult Index()
        {
            Auxiliary auxiliar = new Auxiliary();
            
            List<Company> CompanyList = auxiliar.CarregaLista("companylist.csv");            
            
            ViewBag.Company = CompanyList;
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.DataPoints = JsonConvert.SerializeObject(CompanyList);
            return View("~/Views/CompanyList/Index.cshtml");
        }

        //// GET: /CompanyList/Buscacompany
        public async Task<IActionResult> BuscaCompany(string companySymbol, string searchString)
        {
            // Use LINQ to get list of genres.
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
            //ViewBag.CompanyList = JsonConvert.SerializeObject(auxiliar.CarregaListaPath("companylist.csv"),, new JavaScriptDateTimeConverter());
            ViewBag.CompanyList = JsonConvert.SerializeObject(companySymbolVM.CompanyList, new JavaScriptDateTimeConverter());

            return View(companySymbolVM);
        }




        // GET Method to display the data in line chart model
        // GET: /CompanyList/Charts/
        public IActionResult Charts2(string Symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliar = new Auxiliary(); //Create instance to use auxiliary methods.

            List<Double> Eod = auxiliar.YahooLoadData(Symbol, start, end);
            //List<DateTime> Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);
            List<DateTime> Dates = auxiliar.YahooLoadDates(Symbol, start, end);

            
            List<DataPoint_Date> dataPoints = new List<DataPoint_Date> { };
            for (int i = 0; i < Eod.Count; i++)
            {
                dataPoints.Add(
                    new DataPoint_Date(Dates[i], Eod[i]));
             }

            ViewBag.symbol = Symbol;
            ViewBag.Labels = JsonConvert.SerializeObject(Dates, new JavaScriptDateTimeConverter());
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Data = JsonConvert.SerializeObject(Eod);
            ViewBag.ChartType = "Closing Value";
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints,new JavaScriptDateTimeConverter());

            return View("~/Views/CompanyList/Charts2.cshtml"); //View for Line chart with zooming
            }


        // GET Method to display the data in line chart model
        // GET: /CompanyList/Charts/
        public IActionResult Charts(string Symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliar = new Auxiliary(); //Create instance to use auxiliary methods.

            List<Double> Eod = auxiliar.YahooLoadData(Symbol, start, end);
            //List<DateTime> Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);
            List<DateTime> Dates = auxiliar.YahooLoadDates(Symbol, start, end);


            List<DataPoint> dataPointsL = new List<DataPoint> { };
            for (int i = 0; i < Eod.Count; i++)
            {
                dataPointsL.Add(
                    new DataPoint(Dates[i].ToString("yyyy-MM-dd"), Eod[i]));
            }
            IQueryable<DataPoint> dataPoints = dataPointsL.AsQueryable();

            ViewBag.symbol = Symbol;
            ViewBag.MaxValue = (int) dataPoints.Max(t => t.Y);
            ViewBag.MinValue = (int) dataPoints.Min(t => t.Y);
            ViewBag.Start = start.ToString("yyyy-MM-dd");
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.ChartType = "Closing Value";
            ViewBag.DataPoints = dataPoints;
            //ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View("~/Views/CompanyList/Charts.cshtml"); //View for Line chart with zooming
        }

        // GET Method to display the data in line chart model
        // GET: /CompanyList/Charts/
        public IActionResult ChartFeedback(string Symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliar = new Auxiliary(); //Create instance to use auxiliary methods.

            //List<Double> Eod = auxiliar.YahooLoadData(Symbol, start, end);
            //List<DateTime> Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);
            List<DateTime> Dates = auxiliar.YahooLoadDates(Symbol, start, end);
            List<Double> FeedBack = auxiliar.GetStockFeedback(Symbol, start, end);

            List<DataPoint_Date> dataPoints = new List<DataPoint_Date> { };
            for (int i = 0; i < FeedBack.Count; i++)
            {
                dataPoints.Add(
                    //new DataPoint((Double)Dates[i].DayOfYear, Eod[i]));
                    new DataPoint_Date(Dates[i], FeedBack[i]));
            }
            ViewBag.symbol = Symbol;
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.ChartType = "Symbol Feedback";
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints, new JavaScriptDateTimeConverter());

            return View("~/Views/CompanyList/Charts2.cshtml"); //View for Line chart with zooming
        }

        public IActionResult ChartLogFeedback(string Symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliar = new Auxiliary(); //Create instance to use auxiliary methods.

            //List<Double> Eod = auxiliar.YahooLoadData(Symbol, start, end);
            //List<DateTime> Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);
            List<DateTime> Dates = auxiliar.YahooLoadDates(Symbol, start, end);
            List<Double> FeedBack = auxiliar.GetLogFeedback(Symbol, start, end);

            List<DataPoint_Date> dataPoints = new List<DataPoint_Date> { };
            for (int i = 0; i < FeedBack.Count; i++)
            {
                dataPoints.Add(
                    //new DataPoint((Double)Dates[i].DayOfYear, Eod[i]));
                    new DataPoint_Date(Dates[i], FeedBack[i]));
            }
            ViewBag.symbol = Symbol;
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.ChartType = "Symbol Log Feedback";
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints, new JavaScriptDateTimeConverter());

            return View("~/Views/CompanyList/Charts2.cshtml"); //View for Line chart with zooming
        }



        //Controller to Load the IndexChart ViewComponent as jQuery
        public async Task<IEnumerable<DataPoint>> IndexChart (string symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliary = new Auxiliary(); //Create instance to use auxiliary methods.            
            //var DataPoints = await auxiliary.GetDatapointsAsync("PIH", DateTime.Today.AddMonths(-2), DateTime.Today);
            var DataPoints = await auxiliary.GetDatapointsAsync(symbol,start,end);
            return DataPoints;
        //    return ViewComponent("IndexChart", new { symbol = "PIH", start = DateTime.Today.AddMonths(-2), end = DateTime.Today });
        }
    }
}

