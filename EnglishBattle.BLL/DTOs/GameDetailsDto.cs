namespace EnglishBattle.BLL.DTOs
{
    public class GameDetailsDto
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public double Duration { get; set; }
        public List<GameDetailsAnswerDto> Answers { get; set; }
    }

    public class GameDetailsAnswerDto
    {
        public string BaseForm { get; set; }
        public string Preterit { get; set; }
        public string PastParticiple { get; set; }
        public string PreteritAnswer { get; set; }
        public string PastParticipleAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public bool TimePenalty { get; set; }
    }
}
