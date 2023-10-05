namespace ExerciseTrackerMVCCarDioLogic.Model
{
    public class ExerciseType
    {
        public int ExerciseTypeId { get; set; }
        public string ExerciseTypeName { get; set; }

        public ICollection<ExerciseSession> Sessions { get; set; }
    }
}
