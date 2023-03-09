using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using TekDeq.Localization.Core.Interfaces;

namespace TeqDeq.Avalonia.Sample.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(ILocalizer localizer) : base(localizer)
    {
    }

    // public ICommand ChangeLanguageCommand = 
    public string Greeting => Localizer["View"];
    public IEnumerable<string> AvailableCultures => Localizer.AvailableCultures.Select(x => x.IetfLanguageTag);

    public string ViewModelDIText => Localizer["ViewModel"];

    public string CurrentCulture
    {
        get => Localizer.CurrentCulture.IetfLanguageTag;
        set
        {
            Localizer.CurrentCulture = Localizer.AvailableCultures.First(c => c.IetfLanguageTag == value);
            this.RaisePropertyChanged();
            this.RaisePropertyChanged(nameof(ViewModelDIText));
        }
    }
}