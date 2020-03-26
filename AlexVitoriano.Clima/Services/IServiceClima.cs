using AlexVitoriano.Clima.Models;
using System.Threading.Tasks;

namespace AlexVitoriano.Clima.Services
{
    interface IServiceClima
    {
        Task<Forecast> GetForecast(double lat, double lng);
    }
}
