using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.DAL.Entities
{
    public class Game : BaseEntity
    {
        public int PlayerId { get; private set; }
        public Player Player { get; private set; } = null!;
        public int Seconds { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? OverAt { get; set; }
        public List<GameAnswer> Answers { get; private set; } = new();

        public Game(int playerId)
        {
            Seconds = 60;
            PlayerId = playerId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
