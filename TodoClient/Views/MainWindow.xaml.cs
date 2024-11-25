using ReactiveUI;
using Splat;
using TodoClient.ViewModels;

namespace TodoClient.Views;

public partial class MainWindow : ReactiveWindow<MainWindowModel>
{
    public MainWindow()
    {
        InitializeComponent();

        ViewModel = new MainWindowModel();
        DataContext = ViewModel;

        this.WhenActivated(a => a(ViewModel!.ShowTodoItemDialog.RegisterHandler(ShowDialogAsync)));
    }

    private void ShowDialogAsync(IInteractionContext<TodoDialogViewModel, bool?> interaction)
    {
        var w = Locator.Current.GetService<IViewLocator>();
        var window = w.ResolveView(interaction.Input) as ReactiveWindow<TodoDialogViewModel>;
        window.Owner = this;
        window.ViewModel = interaction.Input;
        var result = window.ShowDialog();
        interaction.SetOutput(result);
    }
}