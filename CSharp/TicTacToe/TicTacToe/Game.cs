using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    /// <summary>
    /// This class represents the tic-tac-toe game
    /// </summary>
    public class Game
    {
        private char[,] map;

        /// <summary>
        /// Winning player's number
        /// </summary>
        /// <remarks>
        /// By default it's 0. It changes when someone wins the game.
        /// </remarks>
        public int Winner { get; private  set; }

        /// <summary>
        /// Current player's number
        /// </summary>
        public int CurrentPlayer { get; private set; }

        /// <summary>
        /// Number of the current turn
        /// </summary>
        public int Turns { get; private set; }

        private int turnCnt;

        public Game()
        {
            map = new char[3,3];
            for(int r = 0; r < 3; r++)
            {
                for(int c = 0; c < 3; c++)
                {
                    map[r, c] = 'o';
                }
            }
            turnCnt = 0;
            Turns = 1;
            Winner = 0;
            CurrentPlayer = 1;
        }

        /// <summary>
        /// Places the player's symbol to the given slot
        /// </summary>
        /// <param name="playerChar">Player's symbol.</param>
        /// <param name="row">Row number to place the symbol.</param>
        /// <param name="column">Column number to place the symbol.</param>
        /// <exception cref="GameException">Thworn when the addressed slot is invalid.</exception>
        /// <list type="bullet">
        /// <item>Row number is less than 0 or greater than 2.</item>
        /// <item>Column number is less that 0 or greater than 2.</item>
        /// <item>Slot is not empty, it was used in a previous move.</item>
        /// </list>
        public void PlayerTurn(char playerChar, int row, int column)
        {

            if (row < 0 || row >= 3) throw new GameException("Invalid row number!");
            if (column < 0 || column >= 3) throw new GameException("Invalid column number!");
            if (map[row, column] != 'o') throw new GameException("Slot already used!");

            map[row, column] = playerChar;
        }
        /// <summary>
        /// Indicates the game's end
        /// </summary>
        /// <returns>
        /// True if the game has ended either by running out of free slots or a player win. False if the game can continue.
        /// </returns>
        /// <remarks>
        /// Must be called after every PlayerTurn call. Also does necessary preparation for the next turn.
        /// </remarks>
        public bool GameEnded()
        {
            if (PlayerWins())
            {
                Winner = CurrentPlayer;
                return true;
            }
            int cnt = 0;
            for(int r = 0; r < 3; r++)
            {
                for(int c = 0;c < 3; c++)
                {
                    if (map[r, c] != 'o') cnt++;
                }
            }
            if (cnt == 9) return true;

            turnCnt++;
            if (turnCnt % 2 == 0)
            {
                CurrentPlayer = 1;
                Turns++;
            }
            else CurrentPlayer = 2;

            return false;
        }
        
        /// <summary>
        /// Indicates player's win
        /// </summary>
        /// <returns>True if the last move won the game for any player. False otherwise.</returns>
        public bool PlayerWins()
        {
            //check rows
            for(int r = 0; r < 3; r++)
            {
                if (map[r, 0] == map[r, 1] && map[r,1]==map[r,2] && map[r,0]!='o') return true;
            }
            //check columns
            for (int c = 0; c < 3; c++)
            {
                if (map[0,c] == map[1,c] && map[1,c] == map[2,c] && map[0,c]!='o') return true;
            }
            //check diagonal
            if (map[0, 0] == map[1, 1] && map[1, 1] == map[2, 2] && map[1, 1] != 'o') return true;
            if (map[0, 2] == map[1, 1] && map[1, 1] == map[2, 0] && map[1, 1] != 'o') return true;
            //every possibility checked
            return false;
        }
        /// <summary>
        /// Get a representation of the game's current state
        /// </summary>
        /// <returns>The game's current state.</returns>
        public string GetMapStateString()
        {
            string result = "";
            for(int r = 0; r < 3; r++)
            {
                for(int c = 0; c < 3; c++)
                {
                    var slot = map[r, c];
                    result += slot;
                    result += " ";
                }
                result += "\n";
            }
            return result;
        }
    }
}
