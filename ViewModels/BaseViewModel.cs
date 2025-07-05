using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModels
{
    //public class BaseViewModel : INotifyPropertyChanged
    //{
    //    bool _isBusy;
    //    public bool IsBusy 
    //    { 
    //        get => _isBusy;
    //        set 
    //        { 
    //            if (_isBusy == value)
    //                return;
    //            _isBusy = value;
    //            OnPropertyChanged();
    //        } 
    //    }
    //    // Allow propperties from ViewModel to be observed by Application for every change and fire event
    //    //Property = Public accessible, Fields = Private accessible
    //    public event PropertyChangedEventHandler? PropertyChanged;

    //    public void OnPropertyChanged([CallerMemberName] string name = null)
    //    {
    //        // Get the name of the propery that will trigger this

    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    //    }
    //}

    
    public partial class BaseViewModel : ObservableObject
    {
        //Using CommunityToolkit.Mvvm

        [ObservableProperty]
        //Generate a property to match the field(Getter and Setter). Public presentation of the field
        //Can be found in Dependencies.Analyzers.SourceGenerators
        //[NotifyCanExecuteChangedFor(nameof(GetCarListCommand))]
        bool isLoading;

        [ObservableProperty]
        string title;

    }
}
