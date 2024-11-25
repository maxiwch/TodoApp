using System.ComponentModel.DataAnnotations;
using TodoWebApi.Models.Attributes;
using TodoWebApi.Resources;

namespace TodoWebApi.Models.DTOs;

public class UpdateTodoRequest
{
    [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "IdRequired")]
    public long? Id { get; set; }

    [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "ContentRequired", AllowEmptyStrings = false)]
    public string? Content { get; set; }

    [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "CompleteRequired")]
    public bool? IsCompleted { get; set; }

    [DateTime(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "EndDateInvalid")]
    public DateTimeOffset? EndDate { get; set; }
}
