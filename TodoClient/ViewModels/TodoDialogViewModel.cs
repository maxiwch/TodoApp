using ReactiveUI;
using System.Reactive;

namespace TodoClient.ViewModels;

public class TodoDialogViewModel : ReactiveObject
{
    private string _content = string.Empty;
    private DateTime _endDate = DateTime.Now;

    public TodoDialogViewModel()
    {
        ResultCommand = ReactiveCommand.Create<bool>(() =>
        {
            if (string.IsNullOrWhiteSpace(_content))
                return false;
            return true;
        });
    }

    public ReactiveCommand<Unit, bool> ResultCommand { get; }

    public string TodoContent
    {
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public DateTime EndDate
    {
        get => _endDate;
        set => this.RaiseAndSetIfChanged(ref _endDate, value);
    }

}
