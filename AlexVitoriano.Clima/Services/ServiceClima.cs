using AlexVitoriano.Clima.Models;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using AlexVitoriano.Clima.Helpers;
using System;

namespace AlexVitoriano.Clima.Services
{
    public class ServiceClima : IServiceClima
    {
        public async Task<Forecast> GetForecast(double Lat, double Long)
        {
            try
            {
                var response =
                    await ServiceHelper.BaseApiRequest
                                       .AppendPathSegment($"{Lat.ToString().Replace(",",".")},{Long.ToString().Replace(",", ".")}")
                                       .GetJsonAsync<Forecast>();

                return response;
            }
            catch(Exception ex)
            {
                var mensagem = ex.Message;
                return null;
            }
        }
    }
}
