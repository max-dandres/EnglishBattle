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
        public string PastPrincipleInput { get; private set; }
        public string PreteritInput { get; private set; }
        public DateTime AnsweredAt { get; private set; }

        public GameAnswer(int gameId, int verbId, string pastPrincipleInput, string preteritInput, DateTime answeredAt)
        {
            GameId = gameId;
            VerbId = verbId;
            PastPrincipleInput = pastPrincipleInput;
            PreteritInput = preteritInput;
            AnsweredAt = answeredAt;
        }
    }
}
