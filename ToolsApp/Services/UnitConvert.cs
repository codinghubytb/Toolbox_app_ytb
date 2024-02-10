
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsApp.ViewModels
{
    public enum ConvertUnit
    {
        Temperature,
        Frequency,
        Speed,
        Time,
        Mass,
        Pressure,
        Energy,
        Length
    }

    public enum Unit
    {
        Celsius,
        Fahrenheit,
        Kelvin,
        Hertz,
        KiloHertz,
        MegaHertz,
        GigaHertz,
        MileHour,
        MeterSeconde,
        KilometerHour,
        Knot,
        FootSecond,
        Second,
        Minute,
        Hour,
        Day,
        Week,
        Month,
        Year,
        Century,
        Gram,
        Kilogram,
        Tonne,
        Stone,
        Pound,
        Ounce,
        Pascal,
        Atmosphere,
        Bar, 
        Joule,
        KiloCalorie,
        WattHour,
        ElectronVolt,
        Meter,
        Kilometer,
        Mile,
        Yard,
        Foot,
        Inch
    }

    public class UnitConvert : IUnitConvert
    {
        Dictionary<ConvertUnit, Dictionary<Unit, string>> _units = new Dictionary<ConvertUnit, Dictionary<Unit, string>>()
        {
            {
                ConvertUnit.Temperature, new Dictionary<Unit, string>()
                {
                    {Unit.Celsius, "°C" },
                    {Unit.Fahrenheit, "F" },
                    {Unit.Kelvin, "K" }
                }
            },
            {
                ConvertUnit.Frequency, new Dictionary<Unit, string>()
                {
                    {Unit.Hertz, "Hz" },
                    {Unit.KiloHertz, "kHz" },
                    {Unit.MegaHertz, "MHz" },
                    {Unit.GigaHertz, "GHz" }
                }
            },
            {
                ConvertUnit.Speed, new Dictionary<Unit, string>()
                {
                    {Unit.MileHour, "mph" },
                    {Unit.MeterSeconde, "m/s" },
                    {Unit.KilometerHour, "km/h" },
                    {Unit.Knot, "n" },
                    {Unit.FootSecond, "p" }
                }
            },
            {
                ConvertUnit.Time, new Dictionary<Unit, string>()
                {
                    {Unit.Second, "s" },
                    {Unit.Minute, "min" },
                    {Unit.Hour, "h" },
                    {Unit.Day, "day" },
                    {Unit.Week, "week" },
                    {Unit.Month, "month" },
                    {Unit.Year, "year" },
                    {Unit.Century, "century" }
                }
            },
            {
                ConvertUnit.Mass, new Dictionary<Unit, string>()
                {
                    {Unit.Gram, "g" },
                    {Unit.Kilogram, "kg" },
                    {Unit.Tonne, "t" },
                    {Unit.Stone, "st" },
                    {Unit.Pound, "lb" },
                    {Unit.Ounce, "oz" }
                }
            },
            {
                ConvertUnit.Pressure, new Dictionary<Unit, string>()
                {
                    {Unit.Pascal, "Pa" },
                    {Unit.Atmosphere, "atm" },
                    {Unit.Bar, "bar" }
                }
            },
            {
                ConvertUnit.Energy, new Dictionary<Unit, string>()
                {
                    {Unit.Joule, "J" },
                    {Unit.KiloCalorie, "kcal" },
                    {Unit.WattHour, "Wh" },
                    {Unit.ElectronVolt, "eV" }
                }
            },
            {
                ConvertUnit.Length, new Dictionary<Unit, string>()
                {
                    {Unit.Meter, "m" },
                    {Unit.Kilometer, "km" },
                    {Unit.Mile, "mi" },
                    {Unit.Yard, "yd" },
                    {Unit.Foot, "ft" },
                    {Unit.Inch, "in" }
                }
            }
        };

        public Dictionary<Unit, string> GetUnit(ConvertUnit value)
        {
            return _units[value];
        }

        public double GetResultConvert(double value, Unit fromUnit, Unit toUnit)
        {
            Dictionary<(Unit, Unit), Func<double, double>> conversionMap = new Dictionary<(Unit, Unit), Func<double, double>>
            {
                {(Unit.Celsius, Unit.Fahrenheit), CelsiusToFahrenheit},
                {(Unit.Celsius, Unit.Kelvin), CelsiusToKelvin},
                {(Unit.Fahrenheit, Unit.Celsius), FahrenheitToCelsius},
                {(Unit.Fahrenheit, Unit.Kelvin), FahrenheitToKelvin},
                {(Unit.Kelvin, Unit.Celsius), KelvinToCelsius},
                {(Unit.Kelvin, Unit.Fahrenheit), KelvinToFahrenheit},


                {(Unit.Hertz, Unit.KiloHertz), HertzToKiloHertz},
                {(Unit.Hertz, Unit.MegaHertz), HertzToMegaHertz},
                {(Unit.Hertz, Unit.GigaHertz), HertzToGigaHertz},
                {(Unit.KiloHertz, Unit.Hertz), KiloHertzToHertz},
                {(Unit.KiloHertz, Unit.MegaHertz), KiloHertzToMegaHertz},
                {(Unit.KiloHertz, Unit.GigaHertz), KiloHertzToGigaHertz},
                {(Unit.MegaHertz, Unit.Hertz), MegaHertzToHertz},
                {(Unit.MegaHertz, Unit.KiloHertz), MegaHertzToKiloHertz},
                {(Unit.MegaHertz, Unit.GigaHertz), MegaHertzToGigaHertz},
                {(Unit.GigaHertz, Unit.Hertz), GigaHertzToHertz},
                {(Unit.GigaHertz, Unit.KiloHertz), GigaHertzToKiloHertz},
                {(Unit.GigaHertz, Unit.MegaHertz), GigaHertzToMegaHertz},

                {(Unit.MileHour, Unit.KilometerHour), MilesPerHourToKilometersPerHour},
                {(Unit.MileHour, Unit.MeterSeconde), MilesPerHourToMetersPerSecond},
                {(Unit.MileHour, Unit.Knot), MilesPerHourToKnots},
                {(Unit.MileHour, Unit.FootSecond), MilesPerHourToFeetPerSecond},
                {(Unit.KilometerHour, Unit.MileHour), KilometersPerHourToMilesPerHour},
                {(Unit.KilometerHour, Unit.MeterSeconde), KilometersPerHourToMetersPerSecond},
                {(Unit.KilometerHour, Unit.Knot), KilometersPerHourToKnots},
                {(Unit.KilometerHour, Unit.FootSecond), KilometersPerHourToFeetPerSecond},
                {(Unit.MeterSeconde, Unit.MileHour), MetersPerSecondToMilesPerHour},
                {(Unit.MeterSeconde, Unit.KilometerHour), MetersPerSecondToKilometersPerHour},
                {(Unit.MeterSeconde, Unit.Knot), MetersPerSecondToKnots},
                {(Unit.MeterSeconde, Unit.FootSecond), MetersPerSecondToFeetPerSecond},
                {(Unit.Knot, Unit.MileHour), KnotsToMilesPerHour},
                {(Unit.Knot, Unit.KilometerHour), KnotsToKilometersPerHour},
                {(Unit.Knot, Unit.MeterSeconde), KnotsToMetersPerSecond},
                {(Unit.Knot, Unit.FootSecond), KnotsToFeetPerSecond},
                {(Unit.FootSecond, Unit.MileHour), FeetPerSecondToMilesPerHour},
                {(Unit.FootSecond, Unit.KilometerHour), FeetPerSecondToKilometersPerHour},
                {(Unit.FootSecond, Unit.MeterSeconde), FeetPerSecondToMetersPerSecond},
                {(Unit.FootSecond, Unit.Knot), FeetPerSecondToKnots},



                {(Unit.Second, Unit.Minute), SecondsToMinutes},
                {(Unit.Second, Unit.Hour), SecondsToHours},
                {(Unit.Second, Unit.Day), SecondsToDays},
                {(Unit.Second, Unit.Week), SecondsToWeeks},
                {(Unit.Second, Unit.Month), SecondsToMonths},
                {(Unit.Second, Unit.Year), SecondsToYears},
                {(Unit.Second, Unit.Century), SecondsToCenturies},
                {(Unit.Minute, Unit.Second), MinutesToSeconds},
                {(Unit.Minute, Unit.Hour), MinutesToHours},
                {(Unit.Minute, Unit.Day), MinutesToDays},
                {(Unit.Minute, Unit.Week), MinutesToWeeks},
                {(Unit.Minute, Unit.Month), MinutesToMonths},
                {(Unit.Minute, Unit.Year), MinutesToYears},
                {(Unit.Minute, Unit.Century), MinutesToCenturies},
                {(Unit.Hour, Unit.Second), HoursToSeconds},
                {(Unit.Hour, Unit.Minute), HoursToMinutes},
                {(Unit.Hour, Unit.Day), HoursToDays},
                {(Unit.Hour, Unit.Week), HoursToWeeks},
                {(Unit.Hour, Unit.Month), HoursToMonths},
                {(Unit.Hour, Unit.Year), HoursToYears},
                {(Unit.Hour, Unit.Century), HoursToCenturies},
                {(Unit.Day, Unit.Second), DaysToSeconds},
                {(Unit.Day, Unit.Minute), DaysToMinutes},
                {(Unit.Day, Unit.Hour), DaysToHours},
                {(Unit.Day, Unit.Week), DaysToWeeks},
                {(Unit.Day, Unit.Month), DaysToMonths},
                {(Unit.Day, Unit.Year), DaysToYears},
                {(Unit.Day, Unit.Century), DaysToCenturies},
                {(Unit.Week, Unit.Second), WeeksToSeconds},
                {(Unit.Week, Unit.Minute), WeeksToMinutes},
                {(Unit.Week, Unit.Hour), WeeksToHours},
                {(Unit.Week, Unit.Day), WeeksToDays},
                {(Unit.Week, Unit.Month), WeeksToMonths},
                {(Unit.Week, Unit.Year), WeeksToYears},
                {(Unit.Week, Unit.Century), WeeksToCenturies},
                {(Unit.Month, Unit.Second), MonthsToSeconds},
                {(Unit.Month, Unit.Minute), MonthsToMinutes},
                {(Unit.Month, Unit.Hour), MonthsToHours},
                {(Unit.Month, Unit.Day), MonthsToDays},
                {(Unit.Month, Unit.Week), MonthsToWeeks},
                {(Unit.Month, Unit.Year), MonthsToYears},
                {(Unit.Month, Unit.Century), MonthsToCenturies},
                {(Unit.Year, Unit.Second), YearsToSeconds},
                {(Unit.Year, Unit.Minute), YearsToMinutes},
                {(Unit.Year, Unit.Hour), YearsToHours},
                {(Unit.Year, Unit.Day), YearsToDays},
                {(Unit.Year, Unit.Week), YearsToWeeks},
                {(Unit.Year, Unit.Month), YearsToMonths},
                {(Unit.Year, Unit.Century), YearsToCenturies},
                {(Unit.Century, Unit.Second), CenturiesToSeconds},
                {(Unit.Century, Unit.Minute), CenturiesToMinutes},
                {(Unit.Century, Unit.Hour), CenturiesToHours},
                {(Unit.Century, Unit.Day), CenturiesToDays},
                {(Unit.Century, Unit.Week), CenturiesToWeeks},
                {(Unit.Century, Unit.Month), CenturiesToMonths},
                {(Unit.Century, Unit.Year), CenturiesToYears},

                {(Unit.Gram, Unit.Kilogram), GramToKilogram},
                {(Unit.Gram, Unit.Tonne), GramToTonne},
                {(Unit.Gram, Unit.Stone), GramToStone},
                {(Unit.Gram, Unit.Pound), GramToPound},
                {(Unit.Gram, Unit.Ounce), GramToOunce},
                {(Unit.Kilogram, Unit.Gram), KilogramToGram},
                {(Unit.Kilogram, Unit.Tonne), KilogramToTonne},
                {(Unit.Kilogram, Unit.Stone), KilogramToStone},
                {(Unit.Kilogram, Unit.Pound), KilogramToPound},
                {(Unit.Kilogram, Unit.Ounce), KilogramToOunce},
                {(Unit.Tonne, Unit.Gram), TonneToGram},
                {(Unit.Tonne, Unit.Kilogram), TonneToKilogram},
                {(Unit.Tonne, Unit.Stone), TonneToStone},
                {(Unit.Tonne, Unit.Pound), TonneToPound},
                {(Unit.Tonne, Unit.Ounce), TonneToOunce},
                {(Unit.Stone, Unit.Gram), StoneToGram},
                {(Unit.Stone, Unit.Kilogram), StoneToKilogram},
                {(Unit.Stone, Unit.Tonne), StoneToTonne},
                {(Unit.Stone, Unit.Pound), StoneToPound},
                {(Unit.Stone, Unit.Ounce), StoneToOunce},
                {(Unit.Pound, Unit.Gram), PoundToGram},
                {(Unit.Pound, Unit.Kilogram), PoundToKilogram},
                {(Unit.Pound, Unit.Tonne), PoundToTonne},
                {(Unit.Pound, Unit.Stone), PoundToStone},
                {(Unit.Pound, Unit.Ounce), PoundToOunce},
                {(Unit.Ounce, Unit.Gram), OunceToGram},
                {(Unit.Ounce, Unit.Kilogram), OunceToKilogram},
                {(Unit.Ounce, Unit.Tonne), OunceToTonne},
                {(Unit.Ounce, Unit.Stone), OunceToStone},
                {(Unit.Ounce, Unit.Pound), OunceToPound},


                {(Unit.Pascal, Unit.Atmosphere), PascalToAtmosphere},
                {(Unit.Pascal, Unit.Bar), PascalToBar},
                {(Unit.Atmosphere, Unit.Pascal), AtmosphereToPascal},
                {(Unit.Atmosphere, Unit.Bar), AtmosphereToBar},
                {(Unit.Bar, Unit.Pascal), BarToPascal},
                {(Unit.Bar, Unit.Atmosphere), BarToAtmosphere},


                {(Unit.Joule, Unit.KiloCalorie), JouleToKiloCalorie},
                {(Unit.Joule, Unit.ElectronVolt), JouleToElectronVolt},
                {(Unit.Joule, Unit.WattHour), JouleToWattHour},
                {(Unit.KiloCalorie, Unit.Joule), KiloCalorieToJoule},
                {(Unit.KiloCalorie, Unit.WattHour) , KiloCalorieToWattHour},
                {(Unit.KiloCalorie, Unit.ElectronVolt), KiloCalorieToElectronVolt},
                {(Unit.WattHour, Unit.Joule), WattHourToJoule},
                {(Unit.WattHour, Unit.KiloCalorie), WattHourToKiloCalorie},
                {(Unit.WattHour, Unit.ElectronVolt), WattHourToElectronVolt},
                {(Unit.ElectronVolt, Unit.Joule), ElectronVoltToJoule},
                {(Unit.ElectronVolt, Unit.KiloCalorie), ElectronVoltToKiloCalorie},
                {(Unit.ElectronVolt, Unit.WattHour), ElectronVoltToWattHour},

                {(Unit.Meter, Unit.Kilometer), MeterToKilometer},
                {(Unit.Meter, Unit.Mile), MeterToMile},
                {(Unit.Meter, Unit.Yard), MeterToYard},
                {(Unit.Meter, Unit.Foot), MeterToFoot},
                {(Unit.Meter, Unit.Inch), MeterToInch},
                {(Unit.Kilometer, Unit.Meter), KilometerToMeter},
                {(Unit.Kilometer, Unit.Mile), KilometerToMile},
                {(Unit.Kilometer, Unit.Yard), KilometerToYard},
                {(Unit.Kilometer, Unit.Foot), KilometerToFoot},
                {(Unit.Kilometer, Unit.Inch), KilometerToInch},
                {(Unit.Mile, Unit.Meter), MileToMeter},
                {(Unit.Mile, Unit.Kilometer), MileToKilometer},
                {(Unit.Mile, Unit.Yard), MileToYard},
                {(Unit.Mile, Unit.Foot), MileToFoot},
                {(Unit.Mile, Unit.Inch), MileToInch},
                {(Unit.Yard, Unit.Meter), YardToMeter},
                {(Unit.Yard, Unit.Kilometer), YardToKilometer},
                {(Unit.Yard, Unit.Mile), YardToMile},
                {(Unit.Yard, Unit.Foot), YardToFoot},
                {(Unit.Yard, Unit.Inch), YardToInch},
                {(Unit.Foot, Unit.Meter), FootToMeter},
                {(Unit.Foot, Unit.Kilometer), FootToKilometer},
                {(Unit.Foot, Unit.Mile), FootToMile},
                {(Unit.Foot, Unit.Yard), FootToYard},
                {(Unit.Foot, Unit.Inch), FootToInch},
                {(Unit.Inch, Unit.Meter), InchToMeter},
                {(Unit.Inch, Unit.Kilometer), InchToKilometer},
                {(Unit.Inch, Unit.Mile), InchToMile},
                {(Unit.Inch, Unit.Yard), InchToYard},
                {(Unit.Inch, Unit.Foot), InchToFoot}};

            if (conversionMap.TryGetValue((fromUnit, toUnit), out var conversionFunc))
            {
                return conversionFunc(value);
            }

            return 0.0;
        }

        #region Température

        public double CelsiusToFahrenheit(double value)
        {
            return (value * 9 / 5) + 32;
        }
        public double CelsiusToKelvin(double value)
        {
            return value + 273.15;
        }
        public double FahrenheitToKelvin(double value)
        {
            return (value - 32) * 5 / 9 + 273.15;
        }
        public double FahrenheitToCelsius(double value)
        {
            return (value - 32) * 5 / 9;
        }
        public double KelvinToCelsius(double value)
        {
            return value - 273.15;
        }
        public double KelvinToFahrenheit(double value)
        {
            return (value - 273.15) * 9 / 5 + 32;
        }

        #endregion

        #region Frequence

        public double HertzToKiloHertz(double value)
        {
            return value / Math.Pow(10, 3);
        }
        public double HertzToMegaHertz(double value)
        {
            return value / Math.Pow(10, 6);
        }
        public double HertzToGigaHertz(double value)
        {
            return value / Math.Pow(10, 9);
        }
        public double KiloHertzToHertz(double value)
        {
            return value * Math.Pow(10, 3);
        }
        public double KiloHertzToMegaHertz(double value)
        {
            return value / Math.Pow(10, 3);
        }
        public double KiloHertzToGigaHertz(double value)
        {
            return value / Math.Pow(10, 6);
        }
        public double MegaHertzToHertz(double value)
        {
            return value * Math.Pow(10, 6);
        }
        public double MegaHertzToKiloHertz(double value)
        {
            return value * Math.Pow(10, 3);
        }
        public double MegaHertzToGigaHertz(double value)
        {
            return value / Math.Pow(10, 3);
        }
        public double GigaHertzToHertz(double value)
        {
            return value * Math.Pow(10, 9);
        }
        public double GigaHertzToKiloHertz(double value)
        {
            return value * Math.Pow(10, 6);
        }
        public double GigaHertzToMegaHertz(double value)
        {
            return value * Math.Pow(10, 3);
        }

        #endregion

        #region Speed

        public double MilesPerHourToKilometersPerHour(double value) => value * 1.609;
        public double MilesPerHourToMetersPerSecond(double value) => value / 2.237;
        public double MilesPerHourToKnots(double value) => value / 1.151;
        public double MilesPerHourToFeetPerSecond(double value) => value * 1.467;

        public double KilometersPerHourToMilesPerHour(double value) => value / 1.609;
        public double KilometersPerHourToMetersPerSecond(double value) => value / 3.6;
        public double KilometersPerHourToKnots(double value) => value / 1.852;
        public double KilometersPerHourToFeetPerSecond(double value) => value * 0.911344;

        public double MetersPerSecondToMilesPerHour(double value) => value * 2.237;
        public double MetersPerSecondToKilometersPerHour(double value) => value * 3.6;
        public double MetersPerSecondToKnots(double value) => value * 1.94384;
        public double MetersPerSecondToFeetPerSecond(double value) => value * 3.28084;

        public double KnotsToMilesPerHour(double value) => value * 1.15078;
        public double KnotsToKilometersPerHour(double value) => value * 1.852;
        public double KnotsToMetersPerSecond(double value) => value * 0.514444;
        public double KnotsToFeetPerSecond(double value) => value * 1.68781;

        public double FeetPerSecondToMilesPerHour(double value) => value * 0.681818;
        public double FeetPerSecondToKilometersPerHour(double value) => value * 1.09728;
        public double FeetPerSecondToMetersPerSecond(double value) => value * 0.3048;
        public double FeetPerSecondToKnots(double value) => value * 0.592484;

        #endregion

        #region Time

        public double SecondsToMinutes(double seconds) => seconds / 60.0;
        public double SecondsToHours(double seconds) => seconds / 3600.0;
        public double SecondsToDays(double seconds) => seconds / 86400.0;
        public double SecondsToWeeks(double seconds) => seconds / 604800.0;
        public double SecondsToMonths(double seconds) => seconds / 2628000.0;
        public double SecondsToYears(double seconds) => seconds / 31536000.0;
        public double SecondsToCenturies(double seconds) => seconds / 3153600000.0;

        public double MinutesToSeconds(double minutes) => minutes * 60.0;
        public double MinutesToHours(double minutes) => minutes / 60.0;
        public double MinutesToDays(double minutes) => minutes / 1440.0;
        public double MinutesToWeeks(double minutes) => minutes / 10080.0;
        public double MinutesToMonths(double minutes) => minutes / 43830.0;
        public double MinutesToYears(double minutes) => minutes / 525600.0;
        public double MinutesToCenturies(double minutes) => minutes / 52560000.0;

        public double HoursToSeconds(double hours) => hours * 3600.0;
        public double HoursToMinutes(double hours) => hours * 60.0;
        public double HoursToDays(double hours) => hours / 24.0;
        public double HoursToWeeks(double hours) => hours / 168.0;
        public double HoursToMonths(double hours) => hours / 730.5;
        public double HoursToYears(double hours) => hours / 8766.0;
        public double HoursToCenturies(double hours) => hours / 876600.0;

        public double DaysToSeconds(double days) => days * 86400.0;
        public double DaysToMinutes(double days) => days * 1440.0;
        public double DaysToHours(double days) => days * 24.0;
        public double DaysToWeeks(double days) => days / 7.0;
        public double DaysToMonths(double days) => days / 30.44;
        public double DaysToYears(double days) => days / 365.25;
        public double DaysToCenturies(double days) => days / 36525.0;

        public double WeeksToSeconds(double weeks) => weeks * 604800.0;
        public double WeeksToMinutes(double weeks) => weeks * 10080.0;
        public double WeeksToHours(double weeks) => weeks * 168.0;
        public double WeeksToDays(double weeks) => weeks * 7.0;
        public double WeeksToMonths(double weeks) => weeks / 4.35;
        public double WeeksToYears(double weeks) => weeks / 52.18;
        public double WeeksToCenturies(double weeks) => weeks / 5218.0;

        public double MonthsToSeconds(double months) => months * 2628000.0;
        public double MonthsToMinutes(double months) => months * 43830.0;
        public double MonthsToHours(double months) => months * 730.5;
        public double MonthsToDays(double months) => months * 30.44;
        public double MonthsToWeeks(double months) => months * 4.35;
        public double MonthsToYears(double months) => months / 12.0;
        public double MonthsToCenturies(double months) => months / 1200.0;

        public double YearsToSeconds(double years) => years * 31536000.0;
        public double YearsToMinutes(double years) => years * 525600.0;
        public double YearsToHours(double years) => years * 8766.0;
        public double YearsToDays(double years) => years * 365.25;
        public double YearsToWeeks(double years) => years * 52.18;
        public double YearsToMonths(double years) => years * 12.0;
        public double YearsToCenturies(double years) => years / 100.0;

        public double CenturiesToSeconds(double centuries) => centuries * 3153600000.0;
        public double CenturiesToMinutes(double centuries) => centuries * 52560000.0;
        public double CenturiesToHours(double centuries) => centuries * 876600.0;
        public double CenturiesToDays(double centuries) => centuries * 36525.0;
        public double CenturiesToWeeks(double centuries) => centuries * 5218.0;
        public double CenturiesToMonths(double centuries) => centuries * 1200.0;
        public double CenturiesToYears(double centuries) => centuries * 100.0;

        #endregion

        #region Masse

        public double KilogramToGram(double value)
        {
            return value * 1000;
        }

        public double KilogramToTonne(double value)
        {
            return value / 1000;
        }

        public double KilogramToStone(double value)
        {
            return value * 0.157473;
        }

        public double KilogramToPound(double value)
        {
            return value * 2.20462;
        }

        public double KilogramToOunce(double value)
        {
            return value * 35.274;
        }
        public double GramToKilogram(double value)
        {
            return value / 1000;
        }

        public double GramToTonne(double value)
        {
            return value / 1e+6;
        }

        public double GramToStone(double value)
        {
            return value * 0.000157473;
        }

        public double GramToPound(double value)
        {
            return value * 0.00220462;
        }

        public double GramToOunce(double value)
        {
            return value * 0.035274;
        }

        public double TonneToKilogram(double value)
        {
            return value * 1000;
        }

        public double TonneToGram(double value)
        {
            return value * 1e+6;
        }

        public double TonneToStone(double value)
        {
            return value * 157.473;
        }

        public double TonneToPound(double value)
        {
            return value * 2204.62;
        }

        public double TonneToOunce(double value)
        {
            return value * 35274;
        }

        public double StoneToKilogram(double value)
        {
            return value * 6.35029;
        }

        public double StoneToGram(double value)
        {
            return value * 6350.29;
        }

        public double StoneToTonne(double value)
        {
            return value * 0.00635029;
        }

        public double StoneToPound(double value)
        {
            return value * 14;
        }

        public double StoneToOunce(double value)
        {
            return value * 224;
        }

        public double PoundToKilogram(double value)
        {
            return value * 0.453592;
        }

        public double PoundToGram(double value)
        {
            return value * 453.592;
        }

        public double PoundToTonne(double value)
        {
            return value * 0.000453592;
        }

        public double PoundToStone(double value)
        {
            return value * 0.0714286;
        }

        public double PoundToOunce(double value)
        {
            return value * 16;
        }

        public double OunceToKilogram(double value)
        {
            return value * 0.0283495;
        }

        public double OunceToGram(double value)
        {
            return value * 28.3495;
        }

        public double OunceToTonne(double value)
        {
            return value * 2.835e-5;
        }

        public double OunceToStone(double value)
        {
            return value * 0.00446429;
        }

        public double OunceToPound(double value)
        {
            return value * 0.0625;
        }

        #endregion

        #region Pressure

        public double PascalToAtmosphere(double value) => value * 9.86923e-6;
        public double PascalToBar(double value) => value * 1e-5;
        public double AtmosphereToPascal(double value) => value * 101325.0;
        public double AtmosphereToBar(double value) => value * 1.01325;
        public double BarToPascal(double value) => value * 1e5;
        public double BarToAtmosphere(double value) => value * 0.986923;


        #endregion

        #region Energy

        public double JouleToKiloCalorie(double value) => value * 0.000239006;
        public double JouleToWattHour(double value) => value * 0.000277778;
        public double JouleToElectronVolt(double value) => value * 6.242e+18;

        public double KiloCalorieToJoule(double value) => value * 4184;
        public double KiloCalorieToWattHour(double value) => value * 1.16222;
        public double KiloCalorieToElectronVolt(double value) => value * 2.611e+22;

        public double WattHourToJoule(double value) => value * 3600;
        public double WattHourToKiloCalorie(double value) => value * 0.860421;
        public double WattHourToElectronVolt(double value) => value * 2.247e+22;

        public double ElectronVoltToJoule(double value) => value * 1.60218e-19;
        public double ElectronVoltToKiloCalorie(double value) => value * 3.82929e-23;
        public double ElectronVoltToWattHour(double value) => value * 4.4505e-23;


        #endregion

        #region Length

        public double MeterToKilometer(double value) => value * 0.001;
        public double MeterToMile(double value) => value * 0.000621371;
        public double MeterToYard(double value) => value * 1.09361;
        public double MeterToFoot(double value) => value * 3.28084;
        public double MeterToInch(double value) => value * 39.3701;

        public double KilometerToMeter(double value) => value * 1000;
        public double KilometerToMile(double value) => value * 0.621371;
        public double KilometerToYard(double value) => value * 1093.61;
        public double KilometerToFoot(double value) => value * 3280.84;
        public double KilometerToInch(double value) => value * 39370.1;

        public double MileToMeter(double value) => value * 1609.34;
        public double MileToKilometer(double value) => value * 1.60934;
        public double MileToYard(double value) => value * 1760;
        public double MileToFoot(double value) => value * 5280;
        public double MileToInch(double value) => value * 63360;

        public double YardToMeter(double value) => value * 0.9144;
        public double YardToKilometer(double value) => value * 0.0009144;
        public double YardToMile(double value) => value * 0.000568182;
        public double YardToFoot(double value) => value * 3;
        public double YardToInch(double value) => value * 36;

        public double FootToMeter(double value) => value * 0.3048;
        public double FootToKilometer(double value) => value * 0.0003048;
        public double FootToMile(double value) => value * 0.000189394;
        public double FootToYard(double value) => value * 0.333333;
        public double FootToInch(double value) => value * 12;

        public double InchToMeter(double value) => value * 0.0254;
        public double InchToKilometer(double value) => value * 2.54e-5;
        public double InchToMile(double value) => value * 1.5783e-5;
        public double InchToYard(double value) => value * 0.0277778;
        public double InchToFoot(double value) => value * 0.0833333;

        #endregion

    }
}
