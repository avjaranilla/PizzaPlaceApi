using PizzaPlaceApi.Domain.DTOs.ReportDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.Repositories
{
    public interface IReportRepository
    {
        Task<List<TopSellingPizzaDTO>> GetTopSellingPizzasAsync(int topN);

        Task<List<DailySalesDTO>> GetSalesReportByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
