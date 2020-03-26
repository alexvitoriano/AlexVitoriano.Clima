using AlexVitoriano.Clima.Helpers;
using System;
using System.Runtime.Serialization;
using Xamarin.Forms;

namespace AlexVitoriano.Clima.Models
{
    /// <summary>
    /// The weather conditions for a particular hour.
    /// </summary>
    [DataContract]
    public class HourDataPoint
    {
        /// <summary>
        /// Unix time at which this data point applies.
        /// </summary>
        [DataMember]
        private int time;

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
        public string Atualizado => Time.LocalDateTime.ToLocalTime().ToString("dd/MM HH:mm");

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
        /// Gets or sets the average expected precipitation assuming any precipitation occurs.
        /// </summary>
        [DataMember(Name = "precipIntensity")]
        public float PrecipitationIntensity { get; set; }
        public string IntensidadeChuva => PrecipitationIntensity == 0f ? "-" : $"{PrecipitationIntensity * 1000} mm";

        /// <summary>
        /// Gets or sets the probability of precipitation (from 0 to 1).
        /// </summary>
        [DataMember(Name = "precipProbability")]
        public float PrecipitationProbability { get; set; }
        public string PossibilidadeChuva => $"{PrecipitationProbability * 100}%";

        /// <summary>
        /// Gets or sets the type of precipitation.
        /// </summary>
        [DataMember(Name = "precipType")]
        public string PrecipitationType { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        [DataMember(Name = "temperature")]
        public float Temperature { get; set; }
        public string Temperatura => $"{Temperature.ToString("0")}°C";

        /// <summary>
        /// Gets or sets the apparent ("feels like") temperature.
        /// </summary>
        [DataMember(Name = "apparentTemperature")]
        public float ApparentTemperature { get; set; }
        public string SensaTermica => $"{ApparentTemperature.ToString("0")}°C";
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
                    return "sleet.png";
                case "mist":
                    return "mist.png";
                case "fog":
                    return "fog.png";
                case "partly-cloudy-night":
                    return "night_partial_cloud.png";
                case "partly-cloudy-day":
                    return "day_partial_cloud.png";
                case "clear-night":
                    return "night_clear.png";
                case "clear-day":
                    return "day_clear.png";
                case "rain":
                    return Helpers.Helpers.IsNightTime ? "night_rain.png" : $"day_rain.png";
                case "snow":
                    return Helpers.Helpers.IsNightTime ? "night_snow.png" : $"day_snow.png";
                case "thunder":
                    return Helpers.Helpers.IsNightTime ? "night_rain_thunder.png" : $"day_rain_thunder.png";
                default:
                    return "overcast.png";
            }
        }

    }
}