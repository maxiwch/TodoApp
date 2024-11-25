using System.ComponentModel.DataAnnotations;
using TodoWebApi.Models.Attributes;
using TodoWebApi.Resources;

namespace TodoWebApi.Models.DTOs;

public class NewTodoRequest
{
    [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "ContentRequired", AllowEmptyStrings = false)]
    public string? Content { get; set; }

    [DateTime(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "EndDateInvalid")]
    public DateTimeOffset? EndDate { get; set; }
}
