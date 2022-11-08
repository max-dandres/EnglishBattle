using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.DAL.Entities
{
    public class Player
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public List<Game> Games { get; private set; } = new();

        public Player(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
