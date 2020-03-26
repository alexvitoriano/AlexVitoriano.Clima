using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AlexVitoriano.Clima.Helpers
{
    public static class ConnectionUtil
    {
        /// <summary>
        /// Verifica conexao com a internet
        /// 
        /// Plugin que verifica a conexao : https://github.com/jamesmontemagno/ConnectivityPlugin
        /// 
        /// **IMPORTANT**
        ///   Android:
        ///   The following persmissions are automatically added for you:
        ///   ACCESS_NETWORK_STATE & ACCESS_WIFI_STATE
        ///
        ///   iOS:
        ///   Bandwidths are not supported and will always return an empty list.
        ///
        ///   Windows 8.1 & Windows Phone 8.1 RT:
        ///   RT apps can not perform loopback, so you can not use IsReachable to query the states of a local IP.
        ///
        ///   Permissions to think about:
        ///   The Private Networks (Client & Server) capability is represented by the Capability name = "privateNetworkClientServer" tag in the app manifest. 
        ///   The Internet (Client & Server) capability is represented by the Capability name = "internetClientServer" tag in the app manifest.        
        /// </summary>
        /// <returns>Conexao ativa</returns>
        ///

        public static bool ConexaoInternetAtiva()
        {
            
            return Connectivity.NetworkAccess.IsIn(NetworkAccess.Internet);
        }
    }
}
