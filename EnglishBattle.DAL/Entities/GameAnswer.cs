using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.DAL.Entities
{
    public class GameAnswer
    {
        public int GameId { get; private set; }
        public Game Game { get; private set; } = null!;
        public int VerbId { get; private set; }
        public IrregularVerb Verb { get; private set; } = null!;
        public string PastParticipleInput { get; private set; }
        public string PastSimpleInput { get; private set; }
        public bool IsCorrect { get; private set; }
        public int TimePenalty { get; private set; }
        public DateTime AnsweredAt { get; private set; }

        public GameAnswer(int gameId, int verbId, string pastParticipleInput, string pastSimpleInput, bool isCorrect, DateTime answeredAt)
        {
            GameId = gameId;
            VerbId = verbId;
            PastParticipleInput = pastParticipleInput;
            PastSimpleInput = pastSimpleInput;
            AnsweredAt = answeredAt;
            TimePenalty = isCorrect ? 3 : -3;
            IsCorrect = isCorrect;
        }
    }
}
