using PizzaPlaceApi.Domain.DTOs.ReportDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Interfaces
{
    public interface IReportService
    {
        Task<List<TopSellingPizzaDTO>> GetTopSellingPizzasAsync(int topN);

        Task<List<DailySalesDTO>> GetSalesReportByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
