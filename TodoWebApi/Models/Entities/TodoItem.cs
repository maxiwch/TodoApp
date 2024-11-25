using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Models.Entities;

public class TodoItem
{
    public TodoItem(string content, DateTimeOffset endDate)
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
        EndDate = endDate;
    }

    public long Id { get; set; }

    public string Content { get; set; }

    public bool IsCompleted { get; set; }

    public DateTimeOffset EndDate { get; set; }
}
