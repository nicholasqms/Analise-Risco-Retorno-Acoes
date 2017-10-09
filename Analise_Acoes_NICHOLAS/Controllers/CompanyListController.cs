using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Analise_Acoes_NICHOLAS.Models;
using Analise_Acoes_NICHOLAS.Auxiliar;
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
            Auxiliares auxiliar = new Auxiliares();
            
            List<Company> CompanyList = auxiliar.CarregaLista("companylist.csv");            
            
            ViewBag.Price = auxiliar.CarregaDadosYahoo("AAPL");
            ViewBag.Company = CompanyList;
            //ViewBag.Company = auxiliar.CarregaLista("companylist.csv");
            //ViewBag.ShowDropDown = new SelectList(auxiliar.CarregaLista("companylist.csv"), "ID", "Sigla", "Nome");
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.DataPoints = JsonConvert.SerializeObject(CompanyList);
            return View("~/Views/CompanyList/Index.cshtml");
        }

        //// GET: /CompanyList/Buscacompany
        public async Task<IActionResult> BuscaCompany(string companySymbol, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> symbolQuery = from m in _context.Company
                                             orderby m.Sigla
                                             select m.Sigla;

            var companies = from m in _context.Company
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                companies = companies.Where(s => s.Nome.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(companySymbol))
            {
                companies = companies.Where(x => x.Sigla == companySymbol);
            }

            var companySymbolVM = new CompanyListModel();
            companySymbolVM.SList = new SelectList(await symbolQuery.Distinct().ToListAsync());
            companySymbolVM.CompanyList = await companies.ToListAsync();
            //ViewBag.CompanyList = JsonConvert.SerializeObject(auxiliar.CarregaListaPath("companylist.csv"),, new JavaScriptDateTimeConverter());
            ViewBag.CompanyList = JsonConvert.SerializeObject(companySymbolVM.CompanyList, new JavaScriptDateTimeConverter());

            return View(companySymbolVM);
            //return View("~/Views/CompanyList/DataTables.cshtml");
        }




        // GET Method to display the data in line chart model
        // GET: /CompanyList/Charts/
        public IActionResult Charts(string Symbol, DateTime start, DateTime end)
        {
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.

            List<Double> Eod = auxiliar.CarregaDadosYahooP(Symbol, start, end);
            //List<DateTime> Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);
            List<DateTime> Dates = auxiliar.CarregaDatasYahooP(Symbol, start, end);

            //List<Closings> ClosingList = new List<Closings> { };
            //for (int i = 0; i < Eod.Count; i++)
            //{
            //    ClosingList.Add(
            //        new Closings(Eod[i], Dates[i]));
            //}
            List<DataPoint_Date> dataPoints = new List<DataPoint_Date> { };
            for (int i = 0; i < Eod.Count; i++)
            {
                dataPoints.Add(
                    //new DataPoint((Double)Dates[i].DayOfYear, Eod[i]));
                    new DataPoint_Date(Dates[i], Eod[i]));
             }
            ViewBag.symbol = Symbol;
            ViewBag.Labels = JsonConvert.SerializeObject(Dates, new JavaScriptDateTimeConverter());

            ViewBag.Data = JsonConvert.SerializeObject(Eod);

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints, new JavaScriptDateTimeConverter());
            
            return View("~/Views/CompanyList/Charts2.cshtml"); //View for Line chart with zooming
            }
        
    
        // GET Method to display the data in line chart model
        // GET: /CompanyList/Charts/
        public IActionResult MultipleCharts(string Symbol1, int months1, string Symbol2, int months2)
        {
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.

            List<Double> Eod1 = auxiliar.CarregaDadosYahoo(Symbol1);
            List<DateTime> Dates1 = auxiliar.CarregaDatasYahooPeriod(Symbol1, months1);

            List<Double> Eod2 = auxiliar.CarregaDadosYahoo(Symbol2);
            List<DateTime> Dates2 = auxiliar.CarregaDatasYahooPeriod(Symbol2, months2);

            //List<Closings> ClosingList1 = new List<Closings> { };
            //List<Closings> ClosingList2 = new List<Closings> { };

            //for (int i = 0; i < Eod1.Count; i++)
            //{
            //    ClosingList1.Add(
            //        new Closings(Eod1[i], Dates1[i]));
            //}
            //for (int i = 0; i < Eod2.Count; i++)
            //{ 
            //    ClosingList2.Add(
            //        new Closings(Eod2[i], Dates2[i]));
            //}
            List<DataPoint_Date> dataPoints1 = new List<DataPoint_Date> { };
            List<DataPoint_Date> dataPoints2 = new List<DataPoint_Date> { };

            //List<DataPoint> dataPoints1 = new List<DataPoint> { };
            //List<DataPoint> dataPoints2 = new List<DataPoint> { };

            for (int i = 0; i < Eod1.Count; i++)
            {
                dataPoints1.Add(
                    //new DataPoint((Double)Dates1[i].DayOfYear, Eod1[i]));
                    new DataPoint_Date(Dates1[i], Eod1[i]));

                    //new DataPoint_Date(("new Date(" + Dates1[i].ToString("yyyy,MM,dd") + ")"), Eod1[i]));
            }
            for (int i = 0; i < Eod2.Count; i++)
            {
                dataPoints2.Add(
                    //new DataPoint((Double)Dates2[i].DayOfYear, Eod2[i]));
                    new DataPoint_Date(Dates2[i], Eod2[i]));

                    //new DataPoint_Date(("new Date(" + Dates2[i].ToString("yyyy,MM,dd") + ")"), Eod2[i]);

                //new DataPoint_Date(Dates2[i].ToString("yyyy,MM,dd"), Eod2[i]));
            }
            ViewBag.symbol1 = Symbol1;
            ViewBag.symbol2 = Symbol2;
            //ViewBag.DataPoints1 = dataPoints1;
            //ViewBag.DataPoints2 = dataPoints2;

            //ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1, new JavaScriptDateTimeConverter());
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2, new JavaScriptDateTimeConverter());
            //ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);

            return View("~/Views/CompanyList/ChartsDual2.cshtml"); //View for Line chart with zooming
        }


    }
}

