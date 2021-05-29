using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Weather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string APP_ID = "723d80cc49b4fe3b626ed923bd63dc48";
        String feels_like;
        String max;
        String min;
        String pressure;
        String humidity;
        String visibility;
        String wind;
        String clouds;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtCityName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                try
                {
                    string query = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", txtCityName.Text, APP_ID);
                    JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));
                    lblCityAndCountry.Content = response.SelectToken("name").ToString() + ", " + response.SelectToken("sys.country").ToString();
                    lblWeather.Content = response.SelectToken("main.temp").ToString() + "c, " + response.SelectToken("weather[0].main").ToString();
                    lblWeatherDescription.Content = response.SelectToken("weather[0].description").ToString();
                    feels_like = response.SelectToken("main.feels_like").ToString();
                    min = response.SelectToken("main.temp_min").ToString();
                    max = response.SelectToken("main.temp_max").ToString();
                    pressure = response.SelectToken("main.pressure").ToString();
                    humidity = response.SelectToken("main.humidity").ToString();
                    visibility = response.SelectToken("visibility").ToString();
                    wind = response.SelectToken("wind.speed").ToString();
                    clouds = response.SelectToken("clouds.all").ToString();
                }
                catch(System.Net.WebException excep)
                {
                    Console.WriteLine("Exception: " + excep.Message);
                    lblCityAndCountry.Content = "City name incorrect";
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\Pufu\\source\\repos\\Weather\\weather_info.txt");
                sw.WriteLine("In " + lblCityAndCountry.Content + ", the temperature is " + lblWeather.Content + ", more exactly " + lblWeatherDescription.Content + ".");
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private void button_Click_details(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\Pufu\\source\\repos\\Weather\\weather_info.txt");
                sw.WriteLine("In " + lblCityAndCountry.Content + ", the temperature is " + lblWeather.Content + ", more exactly " + lblWeatherDescription.Content + "." + "\n" + "It feels like " + feels_like + ", the maximum temperature will be "+ max + " and the minimum will be " + min+".");
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private void button_Click_all_details(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\Pufu\\source\\repos\\Weather\\weather_info.txt");
                sw.WriteLine("In " + lblCityAndCountry.Content + ", the temperature is " + lblWeather.Content + ", more exactly " + lblWeatherDescription.Content + "." + "\n" + "It feels like " + feels_like + ", the maximum temperature will be " + max + " and the minimum will be " + min + "." + "\n" + "The pressure is " + pressure + ", and the humidity is " + humidity + ". \n"  + "The visibility is " + visibility + " and the wind has the speed of " + wind + ". \n" + "There will be " + clouds + " clouds in the sky.");
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
