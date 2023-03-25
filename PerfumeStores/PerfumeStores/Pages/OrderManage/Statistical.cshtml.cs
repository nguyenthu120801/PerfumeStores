using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;
using System.Linq;

namespace PerfumeStores.Pages.OrderManage
{
    public class StatisticalModel : PageModel
    {
        private readonly Prn221Context _context;

        public StatisticalModel(Prn221Context context)
        {
            _context = context;
        }

        public JsonResult OnGet()
        {
            var vm = new ChartVM();
            vm.Labels = new List<string>();
            vm.Datasets = new List<DataSetRow>();
            var temp = _context.OrderDetails.GroupBy(x => x.ProductId).Select(x => new { ProductID = x.Key, TotalRevenue = x.Sum(i => i.TotalPrice) }).ToList();
            var listStatic = new List<Statistical>();
            foreach (var item in temp)
            {
                listStatic.Add(new Statistical { Product = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductID), TotalRevenue = item.TotalRevenue });
            }
            vm.Labels = listStatic.OrderByDescending(x => x.TotalRevenue).Select(x => x.Product.Name).Skip(0).Take(6).ToList();
            var ds1 = new DataSetRow
            {
                BackgroundColor = "#f2f2f2",
                Color = "#24248f",
                Data = listStatic.OrderByDescending(x => x.TotalRevenue).Select(x => x.TotalRevenue).Skip(0).Take(6).ToList()
            };
            vm.Datasets.Add(ds1);
            return new JsonResult(vm);
        }

        public class ChartVM
        {
            public List<string> Labels { get; set; }
            public List<DataSetRow> Datasets { get; set; }

        }
        public class DataSetRow
        {
            public List<double> Data { get; set; }
            public string BackgroundColor { get; set; }
            public string Color { get; set; }
        }
    }
}
