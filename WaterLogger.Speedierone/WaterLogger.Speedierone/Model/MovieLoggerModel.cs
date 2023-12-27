using System.ComponentModel.DataAnnotations;

namespace WaterLogger.Speedierone.Model
{
    public class MovieLoggerModel
    {

        public int Id { get; set; }
        public string MovieName { get; set; }
        public string MovieGenre { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MovieWatchDate { get; set; }
    }
}
