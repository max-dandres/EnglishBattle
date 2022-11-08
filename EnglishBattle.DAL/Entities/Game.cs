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
        public List<GameAnswer> Answers { get; private set; } = new();

        public Game(int id, int playerId)
        {
            Id = id;
            PlayerId = playerId;
        }
    }
}
