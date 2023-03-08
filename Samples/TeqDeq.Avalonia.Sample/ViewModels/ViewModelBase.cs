using ReactiveUI;
using TekDeq.Localization.Interfaces;

namespace TeqDeq.Avalonia.Sample.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
    protected ILocalizer Localizer { get; }

    protected ViewModelBase(ILocalizer localizer)
    {
        Localizer = localizer;
    }
}