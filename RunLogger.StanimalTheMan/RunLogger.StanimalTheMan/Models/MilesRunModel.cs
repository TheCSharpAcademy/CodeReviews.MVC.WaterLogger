using System;
using System.ComponentModel.DataAnnotations;

namespace RunLogger.StanimalTheMan.Models
{
	public class MilesRunModel
	{
		public int Id { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd-MMM-yy}", ApplyFormatInEditMode = true)]
		public DateTime Date { get; set; }

		[Range(0, Double.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public decimal Distance { get; set; }
	}
}

