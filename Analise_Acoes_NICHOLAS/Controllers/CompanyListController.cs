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

            ViewBag.Price = auxiliar.CarregaDadosYahoo("AAPL");
            ViewBag.Company = auxiliar.CarregaLista("companylist.csv");
            //ViewBag.ShowDropDown = new SelectList(auxiliar.CarregaLista("companylist.csv"), "ID", "Sigla", "Nome");

            return View();
        }

        // GET */CompanyList/CompanySearch/
        //Try to make dropdown menu with selector for Symbol visualization 
        public async Task<IActionResult> CompanySearch(string CSymbol, string searchSymbol)
        {
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.
            
            var CompanyListVM = new CompanyListModel();
            CompanyListVM.CompanyList = auxiliar.CarregaLista("companylist.csv");
            CompanyListVM.SList = new SelectList(CompanyListVM.CompanyList, "ID", "Sigla");
            
            //Creation of a Company instance
            Company Selecionado = CompanyListVM.CompanyList.Last();

            //viewBag definitions
            ViewBag.symbol = Selecionado.Sigla;
            ViewBag.Price = auxiliar.CarregaDadosYahoo(Selecionado.Sigla);
            ViewBag.CompanyList = CompanyListVM.CompanyList;
            
            return View(CompanyListVM);

        }

        //Try to make dropdown menu with selector for Symbol visualization 
        public async Task<IActionResult> IndexSearch()
        {
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.


            //Company_LM.CompanyList = auxiliar.CarregaLista("companylist.csv");
            //Company_LM.SList = new SelectList(Company_LM.CompanyList, "ID", "Sigla");

            var symbols = _context.Company.OrderBy(c => c.Sigla).Select(x => new { Id = x.ID, Value = x.Sigla });
            var CLViewModel = new CompanyListModel();

            //List <string> SymbolList = auxiliar.CarregaSiglas("companylist.csv");
            //SelectListItem Selecionado = Company_Listing.SelectedValue();


            //List<Company> Company_Listing = auxiliar.CarregaLista("companylist.csv");
            //Company Selecionado = Company_Listing.Last();
            //ViewBag.symbol = Selecionado.Sigla;
            //ViewBag.Price = auxiliar.CarregaDadosYahoo(Selecionado.Sigla);
            //ViewBag.CompanyList = Company_LM.CompanyList;
            //ViewBag.CompanyList = new SelectList(Company_Listing, "ID", "Sigla");           

            return View(CLViewModel);
        }

        //POST method to obtain the selection value
        [HttpPost]
        public IActionResult IndexSearchResult(string SearchSymbol)
        {
            if (SearchSymbol == null)
            {
                return NotFound();
            }

            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.

            ViewBag.symbol = SearchSymbol;
            ViewBag.Price = auxiliar.CarregaDadosYahoo(SearchSymbol);
            ViewBag.Company = auxiliar.CarregaLista("companylist.csv");
            //ViewBag.ShowDropDown = new SelectList(auxiliar.CarregaLista("companylist.csv"), "ID", "Sigla", "Nome");

            return View("~/Views/CompanyList/IndexSearch.cshtml");
        }

        //View to test the YahooAPI, printing a table with the close and the date values
        public async Task<IActionResult> SymbolEod(string Symbol, int months)
        {
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.

            //Symbol.Trim(new Char[] { '"' });
            if (Symbol == null) 
            {
                Symbol = "APPL";
                
            }
            if  (months == null)
            {
                months = 1;
            }

            List<Double> Eod = auxiliar.CarregaDadosYahoo(Symbol);
            List < DateTime > Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);

            List<Closings> ClosingList = new List<Closings> { };
            for (int i = 1; i < Eod.Count; i++)
            {
                ClosingList.Add(
                    new Closings(Eod[i], Dates[i]));
            }
            ViewBag.symbol = Symbol;

            ViewBag.Closings = ClosingList;
            
            return View();

        }

        // GET Method to display the data in line chart model
        // GET: /CompanyList/Charts/
        public IActionResult Charts(string Symbol, int months)
        {
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.

            List<Double> Eod = auxiliar.CarregaDadosYahoo(Symbol);
            //List<DateTime> Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);
            List<DateTime> Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);

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

