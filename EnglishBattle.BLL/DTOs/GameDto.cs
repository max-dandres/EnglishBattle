namespace EnglishBattle.BLL.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string? PlayerName { get; set; }
        public int Score { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }

        private GameDto()
        {

        }

        public GameDto(int id, string playerName, int score, int duration, DateTime createdAt)
        {
            Id = id;
            PlayerName = playerName;
            Score = score;
            Duration = duration;
            CreatedAt = createdAt;
        }
    }
}
