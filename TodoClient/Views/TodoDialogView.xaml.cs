using ReactiveUI;
using System.Reactive.Disposables;
using TodoClient.ViewModels;

namespace TodoClient.Views;

public partial class TodoDialogView : ReactiveWindow<TodoDialogViewModel>
{
    public TodoDialogView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            this.Bind(ViewModel, viewModel => viewModel.TodoContent, view => view.TodoContent.Text).DisposeWith(d);
            this.Bind(ViewModel, viewModel => viewModel.EndDate, view => view.EndDate.SelectedDate).DisposeWith(d);
            this.BindCommand(ViewModel, viewModel => viewModel.ResultCommand, view => view.OkButton).DisposeWith(d);
        });

        this.WhenActivated(a => a(ViewModel!.ResultCommand.Subscribe(valid =>
        {
            if (valid)
            {
                DialogResult = valid;
                Close();
            }
        })));
    }
}
