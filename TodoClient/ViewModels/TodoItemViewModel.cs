using ReactiveUI;
using System.Reactive;

namespace TodoClient.ViewModels;

public class TodoItemViewModel : ReactiveObject
{
    private long _id;
    private bool _isCompleted;
    private string _content = string.Empty;
    private DateTimeOffset _endDate = DateTimeOffset.Now;
    private readonly TodoApiClient _apiClient;

    public TodoItemViewModel(TodoItem item, TodoApiClient apiClient)
    {
        _id = item.Id;
        _isCompleted = item.IsCompleted;
        _content = item.Content;
        _endDate = item.EndDate;
        _apiClient = apiClient;

        CheckCommand = ReactiveCommand.CreateFromTask(OnCompletedChanged);
    }

    public ReactiveCommand<Unit, Unit> CheckCommand { get; }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => this.RaiseAndSetIfChanged(ref _isCompleted, value);
    }

    public string Content
    {
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public DateTimeOffset EndDate
    {
        get => _endDate;
        set => this.RaiseAndSetIfChanged(ref _endDate, value);
    }

    public long Id => _id;

    private async Task OnCompletedChanged()
    {
        var request = new UpdateTodoRequest()
        {
            Id = Id,
            Content = Content,
            EndDate = EndDate,
            IsCompleted = IsCompleted
        };
        await _apiClient.UpdateTodoAsync(Id, request);
    }
}
