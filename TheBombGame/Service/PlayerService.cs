using System.Collections.Generic;
using System.Linq;
using TheBombGame.Model;

namespace TheBombGame.Service
{
    public class PlayerService
    {
        public List<Player> CreatePlayerList(int playerCount)
        {
            List<Player> players = new List<Player>(playerCount);
            for (int i = 1; i <= players.Capacity; i++)
            {
                players.Add(new Player()
                {
                    PlayerId = i,
                    PlayerName = $"Player {i}",
                    Turn = false
                });
            }

            return players;
        }

        public Player GetStartPlayer(List<Player> players)
        {
            return players.Where(s => s.PlayerId == 1).FirstOrDefault();
        }

        public Player GetNextPlayer(List<Player> players, Player nextPlayer)
        {
            Player currentPlayer = players.Where(s => s.PlayerId == nextPlayer.PlayerId).FirstOrDefault();

            if (currentPlayer.PlayerId == players.Capacity)
            {
                nextPlayer = players[0];
            }
            else
            {
                nextPlayer = players[currentPlayer.PlayerId];
            }

            return nextPlayer;
        }
    }

}