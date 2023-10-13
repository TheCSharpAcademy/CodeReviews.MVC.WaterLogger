namespace GameTracker.Forser.Models
{
    public class GameInfo
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Game Title")]
        public string GameTitle { get; set; }
        [DisplayName("Game Description")]
        public string GameDescription { get; set; }
        public int? SessionId { get; set; }
    }
}