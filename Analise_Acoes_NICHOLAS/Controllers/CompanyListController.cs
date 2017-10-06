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

namespace Analise_Acoes_NICHOLAS.Controllers
{
    public class CompanyListController : Controller
    {
        private readonly Analise_AcoesContext _context;
        // GET: /CompanyList/
        public CompanyListController(Analise_AcoesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Auxiliares auxiliar = new Auxiliares();

            ViewBag.Price = auxiliar.CarregaDadosYahoo("AAPL");
            ViewBag.Company = auxiliar.CarregaLista("companylist.csv");
            //ViewBag.ShowDropDown = new SelectList(auxiliar.CarregaLista("companylist.csv"), "ID", "Sigla", "Nome");

            return View();
        }

        // GET */CompanyList/IndexSearch/
        public async Task<IActionResult> CompanySearch(string CSymbol, string searchSymbol)
        {
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.

            var CompanyListVM = new CompanyListModel();
            CompanyListVM.CompanyList = auxiliar.CarregaLista("companylist.csv");
            CompanyListVM.SList = new SelectList(CompanyListVM.CompanyList, "ID", "Sigla");
            //CompanyListVM.SymbolList = auxiliar.CarregaSiglas("companylist.csv");
            //teste
            //SelectListItem Selecionado = Company_Listing.SelectedValue();


            //List<Company> Company_Listing = auxiliar.CarregaLista("companylist.csv");
            Company Selecionado = CompanyListVM.CompanyList.Last();
            ViewBag.symbol = Selecionado.Sigla;
            ViewBag.Price = auxiliar.CarregaDadosYahoo(Selecionado.Sigla);
            ViewBag.CompanyList = CompanyListVM.CompanyList;
            //ViewBag.CompanyList = new SelectList(Company_Listing, "ID", "Sigla");           

            return View(CompanyListVM);

        }

        public async Task<IActionResult> IndexSearch()
        {
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.


            var Company_LM = new CompanyListModel();
            Company_LM.CompanyList = auxiliar.CarregaLista("companylist.csv");
            Company_LM.SList = new SelectList(Company_LM.CompanyList, "ID", "Sigla");


            //List <string> SymbolList = auxiliar.CarregaSiglas("companylist.csv");
            // fim do teste
            //SelectListItem Selecionado = Company_Listing.SelectedValue();


            //List<Company> Company_Listing = auxiliar.CarregaLista("companylist.csv");
            //Company Selecionado = Company_Listing.Last();
            //ViewBag.symbol = Selecionado.Sigla;
            //ViewBag.Price = auxiliar.CarregaDadosYahoo(Selecionado.Sigla);
            ViewBag.CompanyList = Company_LM.CompanyList;
            //ViewBag.CompanyList = new SelectList(Company_Listing, "ID", "Sigla");           

            return View(Company_LM);
        }

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

            var Company_LM = new CompanyListModel();
            Company_LM.CompanyList = auxiliar.CarregaLista("companylist.csv");
            List<Double> Eod = auxiliar.CarregaDadosYahoo(Symbol);
            List < DateTime > Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);

            List<Closings> ClosingList = new List<Closings> { };
            for (int i = 1; i < Eod.Count; i++)
            {
                ClosingList.Add(
                    new Closings(Eod[i], Dates[i]));
            }
            //List<Company> Company_Listing = auxiliar.CarregaLista("companylist.csv");
            //Company Selecionado = Company_Listing.Last();
            ViewBag.symbol = Symbol;

            ViewBag.Closings = ClosingList;
            
            return View();

        }

        // GET: /Charts/
        public IActionResult Charts(string Symbol, int months)
        {
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.

            List<Double> Eod = auxiliar.CarregaDadosYahoo(Symbol);
            List<DateTime> Dates = auxiliar.CarregaDatasYahooPeriod(Symbol, months);

            List<Closings> ClosingList = new List<Closings> { };
            for (int i = 1; i < Eod.Count; i++)
            {
                ClosingList.Add(
                    new Closings(Eod[i], Dates[i]));
            }
            List<DataPoint> dataPoints = new List<DataPoint> { };
            for (int i = 1; i < Eod.Count; i++)
            {
                dataPoints.Add(
                    new DataPoint((Double)Dates[i].DayOfYear, Eod[i]));
            }
            ViewBag.symbol = Symbol;
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View("~/Views/CompanyList/Charts2.cshtml");
        }
        
    }
}

