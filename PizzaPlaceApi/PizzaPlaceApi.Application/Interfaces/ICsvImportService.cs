using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Interfaces
{
    public interface ICsvImportService
    {
        Task<int> ImportPizzaDataAsync(string csvFilePath);
        Task<int> ImportPizzaTypeDataAsync(string csvFilePath);
        Task<int> ImportOrdersDataAsync(string csvFilePath);
        Task<int> ImportOrderDetailsDataAsync(string csvFilePath);
    }
}
