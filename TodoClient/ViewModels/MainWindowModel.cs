using DynamicData;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;

namespace TodoClient.ViewModels;

public class MainWindowModel
{
    private readonly TodoApiClient _apiClient;

    public MainWindowModel()
    {
        var _baseUrl = "http://localhost:5000/";
        var client_ = new System.Net.Http.HttpClient();
        _apiClient = new TodoApiClient(_baseUrl, client_);
        var items = _apiClient.GetAllTodosAsync()
            .GetAwaiter()
            .GetResult()
            .Select(x => new TodoItemViewModel(x, _apiClient))
            .ToList();
        Items = new ObservableCollection<TodoItemViewModel>(items);

        ShowTodoItemDialog = new Interaction<TodoDialogViewModel, bool?>();

        RefreshCommand = ReactiveCommand.CreateFromTask(Refresh);
        AddCommand = ReactiveCommand.CreateFromTask(Add);
        DeleteCommand = ReactiveCommand.CreateFromTask<TodoItemViewModel>(Delete);
        EditCommand = ReactiveCommand.CreateFromTask<TodoItemViewModel>(Edit);
    }

    public readonly Interaction<TodoDialogViewModel, bool?> ShowTodoItemDialog;

    public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<TodoItemViewModel, Unit> DeleteCommand { get; }
    public ReactiveCommand<TodoItemViewModel, Unit> EditCommand { get; }

    public ObservableCollection<TodoItemViewModel> Items { get; set; }

    private async Task Refresh()
    {
        var items = await _apiClient.GetAllTodosAsync();
        var models = items.Select(x => new TodoItemViewModel(x, _apiClient)).ToList();
        Items.Clear();
        Items.Add(models);
    }

    private async Task Add()
    {
        var model = new TodoDialogViewModel();
        var result = await ShowTodoItemDialog.Handle(model);
        if (result != null && result != false)
        {
            var request = new NewTodoRequest()
            {
                Content = model.TodoContent,
                EndDate = model.EndDate
            };

            var newItem = await _apiClient.CreateTodoAsync(request);
            Items.Add(new TodoItemViewModel(newItem, _apiClient));
        }    
    }

    private async Task Delete(TodoItemViewModel item)
    {
        await _apiClient.DeleteTodoAsync(item.Id);
        Items.Remove(item);
    }

    private async Task Edit(TodoItemViewModel item)
    {
        var model = new TodoDialogViewModel()
        {
            TodoContent = item.Content,
            EndDate = item.EndDate.DateTime
        };
        var result = await ShowTodoItemDialog.Handle(model);
        if (result != null && result != false)
        {
            var request = new UpdateTodoRequest()
            {
                Id = item.Id,
                Content = model.TodoContent,
                EndDate = model.EndDate,
                IsCompleted = item.IsCompleted
            };
            await _apiClient.UpdateTodoAsync(item.Id, request);
            item.Content = model.TodoContent;
            item.EndDate = model.EndDate;
        }
    }
}
