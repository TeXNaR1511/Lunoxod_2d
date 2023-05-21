using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunoxod_2d.Converter
{
    public class Converter : IValueConverter
    {
        

        private double GetDoubleValue(object? str, double defaultValue)
        {
            string str2 = str.ToString();
            double a;
            if (str2 != null)
                try
                {
                    System.Diagnostics.Debug.WriteLine("parameter inside");
                    //System.Diagnostics.Debug.WriteLine(str2);
                    //System.Diagnostics.Debug.WriteLine(str2.GetType());
                    //System.Diagnostics.Debug.WriteLine(System.Convert.ToDouble(str2));
                    System.Diagnostics.Debug.WriteLine(System.Convert.ToDouble("0.75"));
                    System.Diagnostics.Debug.WriteLine(double.Parse("0.75"));
                    a = System.Convert.ToDouble(str2);
                    System.Diagnostics.Debug.WriteLine(a);
                }
                catch
                {
                    a = defaultValue;
                }
            else
                a = defaultValue;
            return a;
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //parameter="0|0|0|0|0|0|0"
            string param = parameter as string;
            //System.Diagnostics.Debug.WriteLine(param);
            string[] par = param.Split('|');
            //double a = GetDoubleValue(par[0], 0);
            double a = System.Convert.ToDouble(par[0], CultureInfo.InvariantCulture);
            double b = System.Convert.ToDouble(par[1], CultureInfo.InvariantCulture);
            double c = System.Convert.ToDouble(par[2], CultureInfo.InvariantCulture);
            double d = System.Convert.ToDouble(par[3], CultureInfo.InvariantCulture);
            double e = System.Convert.ToDouble(par[4], CultureInfo.InvariantCulture);
            double f = System.Convert.ToDouble(par[5], CultureInfo.InvariantCulture);
            double g = System.Convert.ToDouble(par[6], CultureInfo.InvariantCulture);
            double x = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
            //double x = GetDoubleValue(value, 0);
            //double x = 1.0;
            //double b = 1.0;
            //double b = GetDoubleValue(par[1], 0);
            //System.Diagnostics.Debug.WriteLine(par[0]);
            //System.Diagnostics.Debug.WriteLine(par[0].GetType());
            //System.Diagnostics.Debug.WriteLine(par[1]);
            //System.Diagnostics.Debug.WriteLine(a);
            //System.Diagnostics.Debug.WriteLine(b);
            //System.Diagnostics.Debug.WriteLine(par[0].GetType());
            //System.Diagnostics.Debug.WriteLine(value);
            //System.Diagnostics.Debug.WriteLine(value.GetType());
            //System.Diagnostics.Debug.WriteLine(a);
            //System.Diagnostics.Debug.WriteLine(a.GetType());
            //System.Diagnostics.Debug.WriteLine(b);
            //System.Diagnostics.Debug.WriteLine(b.GetType());
            // For add this is simple. just return the sum of the value and the parameter.
            // You may want to validate value and parameter in a real world App
            //System.Diagnostics.Debug.WriteLine((value as double? ?? 0) + (parameter as double? ?? 0));
            return (a * Math.Pow(x, 2) + b * x + c + d * Math.Cos(e * x) + f * Math.Sin(g * x));
            //#double a = GetDoubleValue(parameter, A);

            //double x = GetDoubleValue(value, 0.0);

            //return (a * x) + B;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // If we want to convert back, we need to subtract instead of add.
            return (double?)value - (double?)parameter;
            //double a = GetDoubleValue(parameter, A);

            //double y = GetDoubleValue(value, 0.0);

            //return (y - B) / a;
        }
    }
}
