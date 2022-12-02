namespace EnglishBattle.BLL.DTOs
{
    public class GameDetailsDto
    {
        public int Id { get; set; }
        public string PlayerName { get; set; } = null!;
        public int Score { get; set; }
        public double Duration { get; set; }
        public List<GameDetailsAnswerDto> Answers { get; set; } = null!;
    }

    public class GameDetailsAnswerDto
    {
        public string BaseForm { get; set; } = null!;
        public string Preterit { get; set; } = null!;
        public string PastParticiple { get; set; } = null!;
        public string PreteritAnswer { get; set; } = null!;
        public string PastParticipleAnswer { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public bool TimePenalty { get; set; }
    }
}
