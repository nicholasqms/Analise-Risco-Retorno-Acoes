using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Analise_Acoes_NICHOLAS.Models;
using Analise_Acoes_NICHOLAS.Auxiliar;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Analise_Acoes_NICHOLAS.Controllers
{
    public class CompanyListController : Controller
    {
        // GET: /CompanyList/
        public IActionResult Index()
        {
            Auxiliares auxiliar = new Auxiliares();
            //List<string> lista_siglas = new List<string>();
            //List<string> lista_Nomes = new List<string>();


            //lista_siglas = auxiliar.ObtainFirstColumn("companylist.csv", 0);
            //lista_Nomes = auxiliar.ObtainFirstColumn("companylist.csv", 1);

            //List<Company> CompanyList = new List<Company> { };
            //   for (int i = 0; i < lista_siglas.Count;i++)
            //{
            //    CompanyList.Add(new Company(i, lista_siglas[i], lista_Nomes[i]));

            //}
            ViewBag.Price = auxiliar.CarregaDadosYahoo("AAPL");
            ViewBag.Company = auxiliar.CarregaLista("companylist.csv");
            //ViewBag.ShowDropDown = new SelectList(auxiliar.CarregaLista("companylist.csv"), "ID", "Sigla", "Nome");

            return View();
        }

        // GET */CompanyList/IndexSearch/
        public IActionResult IndexSearch(SelectList Company_Listing)
        {
            if (Company_Listing == null)
            {
                Company_Listing = new SelectList(Company_Listing, "ID", "Sigla", "Nome");
            }
            Auxiliares auxiliar = new Auxiliares(); //Create instance to use auxiliary methods.

            SelectListItem Selecionado = Company_Listing.SelectedValue();
            //Company Selecionado = Company_Listing.Last();
            ViewBag.symbol = Selecionado.Sigla;
            ViewBag.Price = auxiliar.CarregaDadosYahoo(Selecionado.Sigla);
            ViewBag.Company = auxiliar.CarregaLista("companylist.csv");
            ViewBag.CompanyList = Company_Listing;
            
            return View();
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

    }
}
