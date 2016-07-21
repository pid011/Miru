using System.Collections.Generic;

namespace Miru.Widget
{
    internal class WeatherIcon
    {
        private Dictionary<string, char> iconTable = new Dictionary<string, char>
        {
            ["s-01"] = '1',
            ["n-01"] = '6',
            ["s-02"] = 'A',
            ["n-02"] = 'a',
            ["s-03"] = '3',
            ["n-03"] = '3',
            ["s-04"] = 'K',
            ["n-04"] = 'k',
            ["s-05"] = 'H',
            ["n-05"] = 'h',
            ["s-06"] = 'T',
            ["n-06"] = 't',
            ["s-07"] = '…',
            ["n-07"] = '!',
            ["s-08"] = 'M',
            ["n-08"] = 'm',
            ["s-09"] = 'I',
            ["n-09"] = 'i',
            ["s-10"] = 'U',
            ["n-10"] = 'u',
            ["s-11"] = 'Y',
            ["n-11"] = 'y',
            ["s-12"] = 'Q',
            ["n-12"] = 'q',
            ["s-13"] = 'S',
            ["n-13"] = 's',
            ["s-14"] = 'R',
            ["n-14"] = 'r',
        };

        private bool isNight() => new Clock().TimeStatus == Clock.Status.Night ? true : false;

        public char GetWeatherIcon(WeatherWidget.SkyCode sky)
        {
            char icon = '1';

            switch(sky)
            {
                case WeatherWidget.SkyCode.Sunny:
                    icon = !isNight() ? iconTable["s-01"] : iconTable["n-01"];
                    break;

                case WeatherWidget.SkyCode.PartlyCloudy:
                    icon = !isNight() ? iconTable["s-02"] : iconTable["n-02"];
                    break;

                case WeatherWidget.SkyCode.MostlyCloudy:
                    icon = !isNight() ? iconTable["s-03"] : iconTable["n-03"];
                    break;

                case WeatherWidget.SkyCode.MostlyCloudyAndRain:
                    icon = !isNight() ? iconTable["s-04"] : iconTable["n-04"];
                    break;

                case WeatherWidget.SkyCode.MostlyCloudyAndSnow:
                    icon = !isNight() ? iconTable["s-05"] : iconTable["n-05"];
                    break;

                case WeatherWidget.SkyCode.MostlyCloudyAndRainAndSnow:
                    icon = !isNight() ? iconTable["s-06"] : iconTable["n-06"];
                    break;

                case WeatherWidget.SkyCode.Fog:
                    icon = !isNight() ? iconTable["s-07"] : iconTable["n-07"];
                    break;

                case WeatherWidget.SkyCode.FogAndRain:
                    icon = !isNight() ? iconTable["s-08"] : iconTable["n-08"];
                    break;

                case WeatherWidget.SkyCode.FogAndSnow:
                    icon = !isNight() ? iconTable["s-09"] : iconTable["n-09"];
                    break;

                case WeatherWidget.SkyCode.FogAndRainAndSnow:
                    icon = !isNight() ? iconTable["s-10"] : iconTable["n-10"];
                    break;

                case WeatherWidget.SkyCode.FogAndThunderstroke:
                    icon = !isNight() ? iconTable["s-11"] : iconTable["n-11"];
                    break;

                case WeatherWidget.SkyCode.ThunderstormAndRain:
                    icon = !isNight() ? iconTable["s-12"] : iconTable["n-12"];
                    break;

                case WeatherWidget.SkyCode.ThunderstormAndSnow:
                    icon = !isNight() ? iconTable["s-13"] : iconTable["n-13"];
                    break;

                case WeatherWidget.SkyCode.ThunderstormAndRainAndSnow:
                    icon = !isNight() ? iconTable["s-14"] : iconTable["n-14"];
                    break;
            }

            return icon;
        }
    }
}