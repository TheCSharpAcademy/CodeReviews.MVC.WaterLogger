using System.ComponentModel.DataAnnotations;

namespace HabitGains.Web.ViewModels.Entries;

public class EntryItem(Guid id, Guid habitId, DateTime date, decimal quantity)
{
    public Guid Id { get; set; } = id;
    public Guid HabitId { get; set; } = habitId;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
    public DateTime Date { get; set; } = date;

    [DisplayFormat(DataFormatString = "{0:0.##}")]
    public decimal Quantity { get; set; } = quantity;
}
