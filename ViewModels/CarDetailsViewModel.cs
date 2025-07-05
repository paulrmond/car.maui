using CarListApp.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarListApp.Maui.ViewModels
{
    //Steps:
    //1. Navigation occurs - You call GoToAsync("carDetails?id=5")
    //2. Shell locates the target View - Shell resolves the route carDetails → your CarDetailsPage.xaml
    //3. Shell creates the ViewModel - The corresponding CarDetailsViewModel is instantiated (e.g., via constructor or DI).
    //4. [QueryProperty] sets the property - MAUI will automatically do: viewModel.Id = 5;
    //5. ApplyQueryAttributes() is called - viewModel.ApplyQueryAttributes(queryDict);
    //6. Page is rendered - 

    //[QueryProperty(nameof(Car), "Car")]
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        //MAUI will automatically call set Car on the ViewModel, because [QueryProperty] is declared.
        [ObservableProperty]
        Car car;

        [ObservableProperty]
        int id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This will automatically execute after QueryProperty sets its value
            if (query != null)
            {
                Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
                Car = App.CarService.GetCars(Id);
            }
        }
    }
}
