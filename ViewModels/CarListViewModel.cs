using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        //private readonly CarService carService;
        //Dynemic way of filling a list and databinding. Update UI for everty change in this data bind.
        //In.NET, property names are case-sensitive. Use Capital letter

        //Bonus: Why ObservableCollection is important ...the UI automatically updates in real-time when you:
        //Add a new car
        //Remove a car
        //Clear() the list
        //This is what makes the CollectionView dynamic and reactive.

        //ObservableCollection<Car>
        //A collection of Car objects
        //Observable — meaning the UI will automatically update when you add, remove, or clear items
        //Typically used as the ItemsSource of a list control like CollectionView
        public ObservableCollection<Car> Cars { get; private set; } = new();
        public readonly CarApiService carApiService;

        //DI here for Inversion of Control(Its controller is built in .NET application)
        //Will create a copy of CarService and Lifespan is whthin this obj.
        //public CarListViewModel(CarService carService)
        //{
        //    Title = "Car List";
        //    this.carService = carService;

        //}
        public CarListViewModel(CarApiService carApiService)
        {
            Title = "Car List";
            GetCarListAsync().Wait();
            this.carApiService = carApiService;
        }

        //The source generator in the MVVM Toolkit automatically generates a
        //public property like this behind the scenes:
        //public bool IsRefreshing
        //{
        //    get => isRefreshing;
        //    set => SetProperty(ref isRefreshing, value);
        //}
        //Meaning its lower case here but upper case when called in binding

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;

        // Relay command binded from View
        [RelayCommand]
        async Task GetCarListAsync()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any()) Cars.Clear();

                var cars = App.CarService.GetCars();
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", "Failed to retreive list of Cars.", "OK");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task AddCar()
        {
            if(string.IsNullOrEmpty(make) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(vin))
            {
                await Shell.Current.DisplayAlert("Invalid data", "Please insert valid data", "Ok");
                return;
            }

            var car = new Car
            {
                Vin = vin,
                Make = make,
                Model = model
            };

            App.CarService.AddCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
            await GetCarListAsync();
        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = App.CarService.DeleteCar(id);
                    if (result == 0)
                        await Shell.Current.DisplayAlert("Invalid data", "Failed deleting data", "Ok");
                    else
                    {
                        await Shell.Current.DisplayAlert("Successfully deleted", "Record Successfully deleted", "Ok");
                        await GetCarListAsync();
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Invalid data", "Please select valid data", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed deleting data", "Ok");
            }

        }

        //[RelayCommand]
        //async Task GetCarDetailsAsync(Car car)
        //{
        //    if (car == null) return;

        //    // Shell navigation to another XAML Page. Parameters will be passed and will automatically bind in ViewModel
        //    await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
        //    {
        //        { nameof(Car), car } 
        //    });

        //}

        [RelayCommand]
        async Task GetCarDetailsAsync(int id)
        {
            if (id == 0) return;

            // Shell navigation to another XAML Page. Parameters will be passed and will automatically bind in ViewModel
            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);

        }

        [RelayCommand]
        async Task UpdateCar(int id)
        {
            return;
        }
    }
}
