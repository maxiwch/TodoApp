using ReactiveUI;
using Splat;
using System.Reflection;

namespace TodoClient;

public partial class App : System.Windows.Application
{
    public App()
    {
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
    }
}
