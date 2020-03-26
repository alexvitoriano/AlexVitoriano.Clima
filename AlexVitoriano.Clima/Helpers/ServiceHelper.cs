using AlexVitoriano.Clima.Services;
using Caeno.Net.Http.Abstractions;
using Caeno.Net.Http.Services;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexVitoriano.Clima.Helpers
{
    public static class HttpUtils
    {
        public static string CONTENT_TYPE = "application/json";

        public static string URL_DEFAULT = "https://api.darksky.net/";

        public static string XAPIKEY_HEADER = "292ff701ee6f90dc51e7e82ecbfafe35";

        // Timeout Conexao Http
        public const int TIMEOUT_SEGUNDOS_CONEXAO_HTTP = 25;

    }

    public static class ServiceHelper
    {
        public static IHttpHelper DefaultApiClient { get; private set; }

        static ServiceHelper() { DefaultApiClient = new HttpHelper(HttpUtils.URL_DEFAULT); }

        public static IFlurlRequest BaseApiRequest =>
          new FlurlClient(HttpUtils.URL_DEFAULT).WithTimeout(120)
            .Request($"forecast/{HttpUtils.XAPIKEY_HEADER}")
            .SetQueryParam("lang", "pt")
            .SetQueryParam("units", "si");
    }

    public static class ServicesContainer
    {
        public static ServiceClima ClimaService { get; private set; }

        public static void Register()
        {
            ClimaService = new ServiceClima();
        }
    }
}
