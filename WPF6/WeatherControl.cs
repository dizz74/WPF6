using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF6
{
    public class WeatherControl : DependencyObject
    {

        public enum RainType { 
        SUN,
        CLOUD,
        RAIN,
        SNOW
        }

        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }

        public int WindSpeed { get; set; }

        public string WindArrow { get; set; }

        public RainType Rain;

        public WeatherControl(int temp, RainType rainy = RainType.SUN,int wSpeed=0,string wArrow="North") {
            this.Temperature = temp;
            this.WindSpeed = wSpeed;
            this.Rain = rainy;
            this.WindArrow = wArrow;
        }

        private static readonly DependencyProperty TemperatureProperty;


        static WeatherControl()
        {

            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,FrameworkPropertyMetadataOptions.None,
                    new PropertyChangedCallback(OnTempChanged),
                    new CoerceValueCallback(CoerceTemp)),
                new ValidateValueCallback(Validater));
        }

        private static object CoerceTemp(DependencyObject d, object baseValue)
        {
            int temp = (int)baseValue;
            if (temp > 50 || temp < -50) temp = 0;

            return temp;
        }

        private static void OnTempChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WeatherControl w = (WeatherControl)d;
            Console.WriteLine($"Температура:{w.Temperature}");

        }
        private static bool Validater(object value)
        {
            int temp = (int)value;
            if (temp > 50 || temp < -50) return false;
            else
            return true;
        }

        
    }
}
