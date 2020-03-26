using AlexVitoriano.Clima.Helpers;
using System;
using System.Runtime.Serialization;
using Xamarin.Forms;

namespace AlexVitoriano.Clima.Models
{
    /// <summary>
    /// The weather conditions for a particular day.
    /// </summary>
    [DataContract]
    public class DayDataPoint
    {
        /// <summary>
        /// Unix time at which this data point applies.
        /// </summary>
        [DataMember]
        private int time;

        /// <summary>
        /// Unix time of the last sunrise before the solar noon closest to local noon on
        /// the given day.
        /// </summary>
        [DataMember]
        private int sunriseTime;

        /// <summary>
        /// Unix time of the first sunset after the solar noon closest to local noon on
        /// the given day.
        /// </summary>
        [DataMember]
        private int sunsetTime;

        /// <summary>
        /// Gets or sets the time of this data point.
        /// </summary>
        public DateTimeOffset Time
        {
            get
            {
                return time.ToDateTimeOffset();
            }

            set
            {
                time = value.ToUnixTime();
            }
        }
        public string Atualizado => $"{Time.LocalDateTime.ToString("dd/MM")}";

        /// <summary>
        /// Gets or sets a human-readable summary of this data point.
        /// </summary>
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets machine-readable text that can be used to select an icon to display.
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the time of the first sunset after the solar noon closest to local noon
        /// on the given day.
        /// </summary>
        public DateTimeOffset SunsetTime
        {
            get
            {
                return sunsetTime.ToDateTimeOffset();
            }

            set
            {
                sunsetTime = value.ToUnixTime();
            }
        }
        public string SolPoente => $"{SunsetTime.LocalDateTime.ToShortTimeString()}";

        /// <summary>
        /// Gets or sets the time of the last sunrise before the solar noon closest to local noon
        /// on the given day.
        /// </summary>
        public DateTimeOffset SunriseTime
        {
            get
            {
                return sunriseTime.ToDateTimeOffset();
            }

            set
            {
                sunriseTime = value.ToUnixTime();
            }
        }
        public string SolNascente => $"{SunriseTime.LocalDateTime.ToShortTimeString()}";

        /// <summary>
        /// Gets or sets the average expected precipitation assuming any precipitation occurs.
        /// </summary>
        [DataMember(Name = "precipIntensity")]
        public float PrecipitationIntensity { get; set; }
        public string IntensidadeChuva => PrecipitationIntensity == 0f ? "-" : $"{PrecipitationIntensity * 1000} mm";

        /// <summary>
        /// Gets or sets the maximum expected precipitation intensity.
        /// </summary>
        [DataMember(Name = "precipIntensityMax")]
        public float MaxPrecipitationIntensity { get; set; }

        /// <summary>
        /// Gets or sets the type of precipitation.
        /// </summary>
        [DataMember(Name = "precipType")]
        public string PrecipitationType { get; set; }

        /// <summary>
        /// Gets or sets the probability of precipitation (from 0 to 1).
        /// </summary>
        [DataMember(Name = "precipProbability")]
        public float PrecipitationProbability { get; set; }
        public string PossibilidadeChuva => $"{PrecipitationProbability * 100}%";

        /// <summary>
        /// Gets or sets the amount of snowfall accumulation expected to occur.
        /// </summary>
        [DataMember(Name = "precipAccumulation")]
        public float PrecipitationAccumulation { get; set; }

        /// <summary>
        /// Gets or sets the overnight apparent ("feels like") low temperature.
        /// </summary>
        [DataMember(Name = "apparentTemperatureLow")]
        public float ApparentLowTemperature { get; set; }
        public string TemperaturaFeelMin => $"{ApparentLowTemperature.ToString("0")}°C";

        /// <summary>
        /// Gets or sets the daytime apparent ("feels like") high temperature.
        /// </summary>
        [DataMember(Name = "apparentTemperatureHigh")]
        public float ApparentHighTemperature { get; set; }
        public string TemperaturaFeelMax => $"{ApparentHighTemperature.ToString("0")}°C";

        /// <summary>
        /// Gets or sets the overnight low temperature.
        /// </summary>
        [DataMember(Name = "temperatureLow")]
        public float LowTemperature { get; set; }
        public string TemperaturaMin => $"{LowTemperature.ToString("0")}°C";

        /// <summary>
        /// Gets or sets the daytime high temperature.
        /// </summary>
        [DataMember(Name = "temperatureHigh")]
        public float HighTemperature { get; set; }
        public string TemperaturaMax => $"{HighTemperature.ToString("0")}°C";

        /// <summary>
        /// Gets or sets the air temperature.
        /// </summary>
        [DataMember(Name = "temperature")]
        public float Temperature { get; set; }
        public string Temperatura => $"{Temperature.ToString("0")}°C";

        /// <summary>
        /// Gets or sets the dew point.
        /// </summary>
        [DataMember(Name = "dewPoint")]
        public float DewPoint { get; set; }
        public string PtOrvalho => $"{DewPoint.ToString("0")}°C";

        /// <summary>
        /// Gets or sets the relative humidity (from 0 to 1).
        /// </summary>
        [DataMember(Name = "humidity")]
        public float Humidity { get; set; }
        public string Umidade => $"{(Humidity) * 100}%";

        public ImageSource image => ConverteIcon();
        
        ImageSource ConverteIcon()
        {
            switch (Icon)
            {
                case "sleet":
                    return ImageSource.FromFile("sleet.png");
                case "mist":
                    return ImageSource.FromFile("mist.png");
                case "fog":
                    return ImageSource.FromFile("fog.png");
                case "partly-cloudy-night":
                    return ImageSource.FromFile("night_partial_cloud.png");
                case "partly-cloudy-day":
                    return ImageSource.FromFile("day_partial_cloud.png");
                case "clear-night":
                    return ImageSource.FromFile("night_clear.png");
                case "clear-day":
                    return ImageSource.FromFile("day_clear.png");
                case "rain":
                    return ImageSource.FromFile(Helpers.Helpers.IsNightTime ? "night_rain.png" : $"day_rain.png");
                case "snow":
                    return ImageSource.FromFile(Helpers.Helpers.IsNightTime ? "night_snow.png" : $"day_snow.png");
                case "thunder":
                    return ImageSource.FromFile(Helpers.Helpers.IsNightTime ? "night_rain_thunder.png" : $"day_rain_thunder.png");
                default:
                    return ImageSource.FromFile("overcast.png");
            }
        }
    }
}