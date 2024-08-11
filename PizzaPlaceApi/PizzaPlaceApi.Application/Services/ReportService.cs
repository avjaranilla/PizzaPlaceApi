using PizzaPlaceApi.Application.Interfaces;
using PizzaPlaceApi.Domain.DTOs.ReportDTOs;
using PizzaPlaceApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace PizzaPlaceApi.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<List<DailySalesDTO>> GetSalesReportByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Fetch the daily sales data
            var dailySales = await _reportRepository.GetSalesReportByDateRangeAsync(startDate, endDate);

            if (!dailySales.Any())
            {
                throw new InvalidOperationException("No sales data available for the specified period.");
            }

            return dailySales;
        }

        public async Task<List<TopSellingPizzaDTO>> GetTopSellingPizzasAsync(int topN)
        {
            return await _reportRepository.GetTopSellingPizzasAsync(topN);
        }
    }
}
