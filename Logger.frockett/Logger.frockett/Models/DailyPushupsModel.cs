using System.ComponentModel.DataAnnotations;

namespace Logger.frockett.Models;

public class DailyPushupsModel
{
	public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }
	public int Quantity { get; set; }
}
