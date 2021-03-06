﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnaliseAcoesNicholas.Models;
using AnaliseAcoesNicholas.AuxiliaryF;
using AnaliseAcoesNicholas.Data;
using System.Threading.Tasks;
using AnaliseAcoesNicholas.AsyncLoaders;


namespace AnaliseAcoesNicholas.Controllers
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
            List<Company> CompanyList = Auxiliary.ListLoader("companylist.csv");            
            
            if (CompanyList == null)
            {
                return Content("No company list was found on the server, please refer to the service's support.");
            }


            ViewBag.Company = CompanyList;
            return View("~/Views/StockAnalyser/Index.cshtml");
        }
        
        // GET Method to display the data in line chart model
        // GET: /StockAnalyser/CompareSymbols/
        public IActionResult CompareSymbols()
        {           
            List<Company> CompanyList = Auxiliary.ListLoader("companylist.csv");           

            ViewBag.Company = CompanyList;            
            return View();
        }

        //Controllers to Load the Ajax Objects
        public async Task<IEnumerable<DataPoint>> IndexChart (string symbol, DateTime start, DateTime end)
        {
            var  DataPoints = await AsyncLoadersF.GetDatapointsAsync(symbol, start, end);            
            return DataPoints;
         }

        public async Task<IEnumerable<DataPoint>> ClosingChart(string symbol, DateTime start, DateTime end)
        {
            var DataPoints = await AsyncLoadersF.GetDatapointsAsync(symbol, start, end);
            return DataPoints;
        }


        public async Task<IEnumerable<DataPoint>> FeedbackChart(string symbol, DateTime start, DateTime end)
        {
            var DataPoints = await AsyncLoadersF.GetFeedbackAsync(symbol, start, end);
            return DataPoints;
        }


        public async Task<IEnumerable<DataPoint>> LogFeedbackChart(string symbol, DateTime start, DateTime end)
        {
            var DataPoints = await AsyncLoadersF.GetLogFeedbackAsync(symbol, start, end);
            return DataPoints;
        }

        public async Task<IEnumerable<DataPoint>> VolumeChart(string symbol, DateTime start, DateTime end)
        {
            var DataPoints = await AsyncLoadersF.GetVolumeAsync(symbol, start, end);
            return DataPoints;
        }

        public async Task<IEnumerable<DataPoint>> HistogramChart(string symbol, DateTime start, DateTime end)
        {
            var DataPoints = await AsyncLoadersF.GetHistogramAsync(symbol, start, end);
            return DataPoints;
        }
    }
}