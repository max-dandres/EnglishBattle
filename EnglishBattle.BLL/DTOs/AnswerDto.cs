using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.BLL.DTOs
{
    public class AnswerDto
    {
        public int GameId { get; set; }
        public int VerbId { get; set; }
        public string Preterit { get; set; } = null!;
        public string PastPrinciple { get; set; } = null!;
        public DateTime AnsweredAt { get; set; }
    }
}
