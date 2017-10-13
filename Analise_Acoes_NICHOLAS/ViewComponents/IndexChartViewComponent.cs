using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analise_Acoes_NICHOLAS.Models;
using Analise_Acoes_NICHOLAS.AuxiliaryF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Analise_Acoes_NICHOLAS.ViewComponents
{
    public class IndexChartViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string symbol, DateTime start, DateTime end)
        {
            Auxiliary auxiliary = new Auxiliary(); //Create instance to use auxiliary methods.            
            var DataPoints = await auxiliary.GetDatapointsAsync(symbol, start, end);
            return View(DataPoints);

        }
    }
}
  