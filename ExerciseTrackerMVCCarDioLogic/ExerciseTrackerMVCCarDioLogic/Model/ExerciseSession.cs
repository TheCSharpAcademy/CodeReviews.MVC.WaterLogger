using System.ComponentModel.DataAnnotations;

namespace ExerciseTrackerMVCCarDioLogic.Model;

public class ExerciseSession
{
    public int Id { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}", ApplyFormatInEditMode = true)]
    public string Date { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Value for {0} musy be positive.")]
    public int DurationInMinutes { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Value for {0} musy be positive.")]
    public string ExerciseTypeName { get; set; }
    public double Weigth { get; set; }

    public ExerciseType exerciseType { get; set; }
}
