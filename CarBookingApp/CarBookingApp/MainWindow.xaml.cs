using CarBookingAPI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace CarBookingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ApiBaseAddress = "https://localhost:5001/api/";
        private static readonly HttpClient Client = new HttpClient() { BaseAddress = new Uri(ApiBaseAddress) };

        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        public ObservableCollection<Car> CarsAvailable { get; set; } = new ObservableCollection<Car>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _ = LoadCarsAsync();
        }

        private async Task LoadCarsAsync()
        {
            var ResultCars = await GetCars();
            ResultCars.ForEach(c => Cars.Add(c));

        }

        private async Task<List<Car>> GetCars()
        {
            var response = await Client.GetAsync("cars");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Car>>(body);
        }

        private void RefreshButtonClicked (object sender, RoutedEventArgs e) 
        {
            _ = RefreshAvailableCarsOnDateAsync();
        }

        private async Task RefreshAvailableCarsOnDateAsync()
        {
            if (!AvailabilityDatePicker.SelectedDate.HasValue) return;
            var response = await Client.GetAsync("cars/available?date=" + AvailabilityDatePicker.SelectedDate.ToString());
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var ResultCars = JsonSerializer.Deserialize<List<Car>>(body);
            CarsAvailable.Clear();
            ResultCars.ForEach(c => CarsAvailable.Add(c));
        }


        private void BookButtonClicked (object sender, RoutedEventArgs e) 
        {
            _ = BookCarAsync();
        }

        private async Task BookCarAsync()
        {
            if (BookingCarIdTextBox.Text == null) return;
            if (!BookingDateDatePicker.SelectedDate.HasValue) return;

            Booking booking = new Booking {
                Date = BookingDateDatePicker.SelectedDate.Value,
                CarId = Int32.Parse(BookingCarIdTextBox.Text)
            };

            var body = new StringContent(JsonSerializer.Serialize(booking), Encoding.UTF8, "application/json");
            var responseMessage = await Client.PostAsync("bookings", body);
            responseMessage.EnsureSuccessStatusCode();
        }
    }
}
