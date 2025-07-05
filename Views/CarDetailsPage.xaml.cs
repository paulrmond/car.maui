using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class CarDetailsPage : ContentPage
{
	public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
	{
		InitializeComponent();
        //Data will automatically bind in ViewModel base on ObservableProperty declared.
        BindingContext = carDetailsViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        //After this execution will only the binding starts with the ViewModel
        base.OnNavigatedTo(args);
    }
}