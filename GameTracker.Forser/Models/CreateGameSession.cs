namespace GameTracker.Forser.Models
{
    public class CreateGameSession
    {
        public GameSession GameSession { get; set; }
        public List<SelectListItem>? GameInfos { get; set; }
    }
}
