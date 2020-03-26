using AlexVitoriano.Clima.Helpers;
using AlexVitoriano.Clima.Models;
using AlexVitoriano.Clima.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlexVitoriano.Clima.ViewModels
{
    public class ForecastViewModel : BaseViewModel
    {
        public ForecastViewModel()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            Altura = Convert.ToInt16(mainDisplayInfo.Height / mainDisplayInfo.Density);
            Largura = Convert.ToInt16(mainDisplayInfo.Width / mainDisplayInfo.Density);

            serveClima = ServicesContainer.ClimaService;
        }

        IServiceClima serveClima;

        public Command LoadItemsCommand { get; set; }

        #region Props
        private double _lat;
        public double Lat
        {
            get { return _lat; }
            set { SetProperty(ref _lat, value); }
        }

        private double _lon;
        public double Lon
        {
            get { return _lon; }
            set { SetProperty(ref _lon, value); }
        }

        private int largura;
        public int Largura
        {
            get { return largura; }
            set { SetProperty(ref largura, value); }
        }

        private int altura;
        public int Altura
        {
            get { return altura; }
            set { SetProperty(ref altura, value); }
        }

        private ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        private Forecast previsao;
        public Forecast Previsao
        {
            get { return previsao; }
            set { SetProperty(ref previsao, value); }
        }

        private string tempAtual;
        public string TempAtual
        {
            get { return tempAtual; }
            set { SetProperty(ref tempAtual, value); }
        }

        private string placeName;
        public string PlaceName
        {
            get { return placeName; }
            set { SetProperty(ref placeName, value); }
        }

        private string sumary;
        public string Sumary
        {
            get { return sumary; }
            set { SetProperty(ref sumary, value); }
        }

        private string atualizado;
        public string Atualizado
        {
            get { return atualizado; }
            set { SetProperty(ref atualizado, value); }
        }

        private string sencTerm;
        public string SencTerm
        {
            get { return sencTerm; }
            set { SetProperty(ref sencTerm, value); }
        }

        private string tempMin;
        public string TempMin
        {
            get { return tempMin; }
            set { SetProperty(ref tempMin, value); }
        }

        private string tempMax;
        public string TempMax
        {
            get { return tempMax; }
            set { SetProperty(ref tempMax, value); }
        }

        private string solNascente;
        public string SolNascente
        {
            get { return solNascente; }
            set { SetProperty(ref solNascente, value); }
        }

        private string solPoente;
        public string SolPoente
        {
            get { return solPoente; }
            set { SetProperty(ref solPoente, value); }
        }

        private string posChuva;
        public string PosChuva
        {
            get { return posChuva; }
            set { SetProperty(ref posChuva, value); }
        }

        private string intChuva;
        public string IntChuva
        {
            get { return intChuva; }
            set { SetProperty(ref intChuva, value); }
        }

        private string umid;
        public string Umid
        {
            get { return umid; }
            set { SetProperty(ref umid, value); }
        }

        private string orvalho;
        public string Orvalho
        {
            get { return orvalho; }
            set { SetProperty(ref orvalho, value); }
        }

        private string tempMinPrev;
        public string TempMinPrev
        {
            get { return tempMinPrev; }
            set { SetProperty(ref tempMinPrev, value); }
        }

        private string tempMaxPrev;
        public string TempMaxPrev
        {
            get { return tempMaxPrev; }
            set { SetProperty(ref tempMaxPrev, value); }
        }

        private string tempMinFeel;
        public string TempMinFeel
        {
            get { return tempMinFeel; }
            set { SetProperty(ref tempMinFeel, value); }
        }

        private string tempMaxFeel;
        public string TempMaxFeel
        {
            get { return tempMaxFeel; }
            set { SetProperty(ref tempMaxFeel, value); }
        }

        private ObservableCollection<HourDataPoint> hourDataPoints;
        public ObservableCollection<HourDataPoint> HourDataPoints
        {
            get { return hourDataPoints; }
            set { SetProperty(ref hourDataPoints, value); }
        }

        private ObservableCollection<DayDataPoint> dayDataPoints;
        public ObservableCollection<DayDataPoint> DayDataPoints
        {
            get { return dayDataPoints; }
            set { SetProperty(ref dayDataPoints, value); }
        }
        #endregion

        public async Task ExecuteLoadItems()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                
                if (Lat != 0.0d && Lon != 0.0d)
                {
                    //Compilação no iOS perdia a referencia ao serviço.
                    if (serveClima == null)
                    {
                        serveClima = ServicesContainer.ClimaService;
                    }


                    Previsao = await serveClima.GetForecast(Lat, Lon);

                    if (Previsao != null)
                    {
                        TempAtual = Previsao.Currently.Temperatura;
                        Sumary = Previsao.Currently.Summary;
                        Atualizado = Previsao.Currently.Atualizado;
                        SencTerm = Previsao.Currently.SensaTermica;
                        Orvalho = Previsao.Currently.PtOrvalho;
                        Umid = Previsao.Currently.Umidade;
                        PosChuva = Previsao.Currently.PossibilidadeChuva;
                        IntChuva = Previsao.Currently.IntensidadeChuva;
                        Image = Previsao.Currently.image;

                        if (Previsao.Daily.Days != null && Previsao.Daily.Days.Count > 0)
                        {
                            SolNascente = Previsao.Daily.Days[0].SolNascente;
                            SolPoente = Previsao.Daily.Days[0].SolPoente;
                            TempMinPrev = Previsao.Daily.Days[0].TemperaturaFeelMin;
                            TempMaxPrev = Previsao.Daily.Days[0].TemperaturaFeelMax;
                            TempMin = Previsao.Daily.Days[0].TemperaturaMin;
                            TempMax = Previsao.Daily.Days[0].TemperaturaMax;
                        }

                        ConverteBackground(Previsao.Currently.Icon);

                        Previsao.Hourly.Hours.RemoveAt(0);
                        HourDataPoints = new ObservableCollection<HourDataPoint>(Previsao.Hourly.Hours);

                        Previsao.Daily.Days.RemoveAt(0);
                        DayDataPoints = new ObservableCollection<DayDataPoint>(Previsao.Daily.Days);
                    }
                    else
                    {
                        IsError = true;
                    }
                }
                else
                {
                    //Exibe animação de Location com problema.
                    IsLocationNotEnabled = true;

                }

            }
            catch (Exception ex)
            {
                IsError = true;
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }

        }

        public async Task RecuperaLocation()
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;

                var location = await Geolocation.GetLastKnownLocationAsync();


                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Lat = location.Latitude;
                    Lon = location.Longitude;
                }
                else
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Lowest, new TimeSpan(0, 1, 0));
                    location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                        Lat = location.Latitude;
                        Lon = location.Longitude;
                    }
                    else
                        Lat = Lon = 0.0d;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                IsError = true;

            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                IsError = true;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                IsError = true;
            }
            catch (Exception ex)
            {
                // Unable to get location
                IsError = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        void ConverteBackground(string Icon)
        {
            switch (Icon)
            {
                default:
                case "mist":
                case "fog":
                case "sleet":
                    Application.Current.Resources["Gradiente"] = Application.Current.Resources["Sleet"];
                    break;
                case "partly-cloudy-night":
                    if (Helpers.Helpers.IsMadrugada)
                        Application.Current.Resources["Gradiente"] = Application.Current.Resources["CloudNight03"];
                    else
                        Application.Current.Resources["Gradiente"] = Application.Current.Resources["CloudNight20"];
                    break;
                case "partly-cloudy-day":
                    if (Helpers.Helpers.IsTarde)
                        Application.Current.Resources["Gradiente"] = Application.Current.Resources["CloudPreSunset"];
                    else
                        Application.Current.Resources["Gradiente"] = Application.Current.Resources["CloudDay12"];
                    break;
                case "clear-night":
                    if (Helpers.Helpers.IsMadrugada)
                        Application.Current.Resources["Gradiente"] = Application.Current.Resources["ClearNight03"];
                    else
                        Application.Current.Resources["Gradiente"] = Application.Current.Resources["ClearNight20"];
                    break;
                case "clear-day":
                    if (Helpers.Helpers.IsTarde)
                        Application.Current.Resources["Gradiente"] = Application.Current.Resources["ClearPreSunset"];
                    else
                        Application.Current.Resources["Gradiente"] = Application.Current.Resources["ClearDay12"];
                    break;
                case "rain":
                    {
                        if (Helpers.Helpers.IsNightTime)
                            Application.Current.Resources["Gradiente"] = Application.Current.Resources["RainNight"];
                        else
                            Application.Current.Resources["Gradiente"] = Application.Current.Resources["RainDay"];
                    }
                    break;
                case "snow":
                    {
                        if (Helpers.Helpers.IsNightTime)
                            Application.Current.Resources["Gradiente"] = Application.Current.Resources["SnowNight"];
                        else
                            Application.Current.Resources["Gradiente"] = Application.Current.Resources["SnowDay"];
                    }
                    break;
                case "thunder":
                    {
                        if (Helpers.Helpers.IsNightTime)
                            Application.Current.Resources["Gradiente"] = Application.Current.Resources["RainNight"];
                        else
                            Application.Current.Resources["Gradiente"] = Application.Current.Resources["RainDay"];
                    }
                    break;
            }


        }

    }
}
