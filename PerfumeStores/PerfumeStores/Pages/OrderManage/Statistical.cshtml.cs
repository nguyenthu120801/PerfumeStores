using Microsoft.AspNetCore.Mvc.RazorPages;
using PerfumeStores.Core.Models;
using System.Data;
using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using PerfumeStores.Core.DTOs;

namespace PerfumeStores.Pages.OrderManage
{
    public class StatisticalModel : PageModel
    {
        private readonly Prn221Context _context;

        public StatisticalModel(Prn221Context context)
        {
            _context = context;
        }

        public string ChartJson { get; internal set; }
        public string ChartJson2 { get; internal set; }
        public void OnGet()
        {
            if (LoginDTO.CustomerID != null && LoginDTO.IsAdmin)
            {
                DataTable ChartData = new DataTable();
                ChartData.Columns.Add("Product", typeof(System.String));
                ChartData.Columns.Add("Revenue", typeof(System.Double));
                var temp = _context.OrderDetails.GroupBy(x => x.ProductId).Select(x => new { ProductID = x.Key, TotalRevenue = x.Sum(i => i.TotalPrice) }).ToList();
                var listStatic = new List<Statistical>();
                foreach (var item in temp)
                {
                    listStatic.Add(new Statistical { Product = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductID), TotalRevenue = item.TotalRevenue });
                }
                var list = listStatic.OrderByDescending(x => x.TotalRevenue).Skip(0).Take(6).ToList();
                foreach (var item in list)
                {
                    ChartData.Rows.Add(item.Product.Name, item.TotalRevenue);
                }
                StaticSource source = new StaticSource(ChartData);
                DataModel model = new DataModel();
                model.DataSources.Add(source);
                Charts.ColumnChart column = new Charts.ColumnChart("first_chart");
                column.Width.Pixel(1000);
                column.Height.Pixel(400);
                column.Data.Source = model;
                column.Caption.Text = "Most popular revenue";
                column.Legend.Show = false;
                column.XAxis.Text = "Product";
                column.YAxis.Text = "Revenue";
                column.ThemeName = FusionChartsTheme.ThemeName.FUSION;
                ChartJson = column.Render();

                DataTable ChartData2 = new DataTable();
                ChartData2.Columns.Add("Product", typeof(System.String));
                ChartData2.Columns.Add("Total sell", typeof(System.Int32));
                var temp2 = _context.OrderDetails.GroupBy(x => x.ProductId).Select(x => new { ProductID = x.Key, TotalSell = x.Sum(i => i.Quantity) }).ToList();
                var listStatic2 = new List<StatisticalSell>();
                foreach (var item in temp2)
                {
                    listStatic2.Add(new StatisticalSell { Product = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductID), TotalSell = item.TotalSell });
                }
                var list2 = listStatic2.OrderByDescending(x => x.TotalSell).Skip(0).Take(6).ToList();
                foreach (var item in list2)
                {
                    ChartData2.Rows.Add(item.Product.Name, item.TotalSell);
                }
                StaticSource source2 = new StaticSource(ChartData2);
                DataModel model2 = new DataModel();
                model2.DataSources.Add(source2);
                Charts.ColumnChart column2 = new Charts.ColumnChart("second_chart");
                column2.Width.Pixel(1000);
                column2.Height.Pixel(400);
                column2.Data.Source = model2;
                column2.Caption.Text = "Most popular sell product";
                column2.Legend.Show = false;
                column2.XAxis.Text = "Product";
                column2.YAxis.Text = "Total Sell";
                column2.ThemeName = FusionChartsTheme.ThemeName.FUSION;
                ChartJson2 = column2.Render();
            }
        }
    }
}
