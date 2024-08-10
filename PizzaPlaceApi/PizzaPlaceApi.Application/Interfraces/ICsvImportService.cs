using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Interfraces
{
    public interface ICsvImportService
    {
        Task ImportPizzaDataAsync(string csvFilePath);
        Task ImportPizzaTypeDataAsync(string csvFilePath);
        Task ImportOrdersDataAsync(string csvFilePath);
        Task ImportOrderDetailsDataAsync(string csvFilePath);
    }
}
