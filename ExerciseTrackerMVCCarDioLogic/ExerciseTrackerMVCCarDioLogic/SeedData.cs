using ExerciseTrackerMVCCarDioLogic.Model;

namespace ExerciseTrackerMVCCarDioLogic
{
    public class SeedData
    {
        //probably wont need this fille
        private readonly Context _context;
        public SeedData(Context context)
        {
            _context = context;
        }

        public void SeedDataToDatabase()
        {
            _context.Database.EnsureCreated();

            if(!_context.Sessions.Any())
            {
                var session1 = new ExerciseSession
                {
                    Date = "2023/09/20",
                    DurationInMinutes = 20 
                };
                var session2 = new ExerciseSession
                {
                    Date = "2023/09/21",
                    DurationInMinutes = 30
                };

                _context.Sessions.AddRange(session1, session2);
                _context.SaveChanges();
            }
        }
    }
}
