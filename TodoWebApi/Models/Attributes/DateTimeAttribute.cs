using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Models.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DateTimeAttribute : RequiredAttribute
{
    public DateTimeOffset Min { get; set; } = DateTimeOffset.MinValue;
    public DateTimeOffset Max { get; set; } = DateTimeOffset.MaxValue;

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return base.IsValid(value);
        }
        return (value is DateTimeOffset date) && (date > Min && date < Max);
    }
}
