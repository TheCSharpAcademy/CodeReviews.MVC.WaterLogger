namespace GameTracker.Forser.Models
{
    public class GameSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Start of Session")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm", ApplyFormatInEditMode = true)]
        public DateTime SessionStart { get; set; }
        [DisplayName("End of Session")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm", ApplyFormatInEditMode = true)]
        public DateTime SessionEnd { get; set; }
        [DisplayName("Total time of Session")]
        public TimeSpan Duration => SessionEnd - SessionStart;
        public int? GameId { get; set; }
        public GameInfo? Game { get; set; }
    }
}