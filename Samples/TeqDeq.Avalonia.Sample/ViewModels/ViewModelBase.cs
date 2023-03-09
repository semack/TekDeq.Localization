using ReactiveUI;
using TekDeq.Localization.Core.Interfaces;

namespace TeqDeq.Avalonia.Sample.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
    protected ViewModelBase(ILocalizer localizer)
    {
        Localizer = localizer;
    }

    protected ILocalizer Localizer { get; }
}