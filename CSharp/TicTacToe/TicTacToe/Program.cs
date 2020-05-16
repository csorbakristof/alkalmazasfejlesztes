using System;

namespace TicTacToe
{
    class Program
    {
        private static Game game;
        private static bool gameEnded;
        private static void PrintGameRules()
        {
            Console.Clear();
            Console.WriteLine("Tic-tac-toe game");
            Console.WriteLine("Row&column index can be 0, 1 or 2\nPlayer1 places + marks, Player2 places - marks");
            Console.WriteLine($"Turn: {game.Turns}");
            Console.WriteLine();
            Console.WriteLine(game.GetMapStateString());
        }
        private static void PlayerTurn()
        {
            int player = game.CurrentPlayer;
            Console.WriteLine($"Player{player}'s turn");
            Console.Write("R");
            int row = -1;
            var key = Console.ReadKey();
            row = Int32.Parse(key.KeyChar.ToString());
            Console.Write("C");
            int column = -1;
            key = Console.ReadKey();
            column = Int32.Parse(key.KeyChar.ToString());

            try
            {
                if (player == 1) game.PlayerTurn('+', row, column);
                else game.PlayerTurn('-', row, column);
            }
            catch(Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                gameEnded = true;
            }
            

        }
        private static void EndPlayerTurn()
        {
            if (game.GameEnded())
            {
                Console.WriteLine();
                Console.WriteLine("Game ended");
                gameEnded = true;
                Console.WriteLine();
                Console.WriteLine(game.GetMapStateString());
                if (game.Winner != 0) Console.WriteLine($"Player{game.Winner} is the winner");
                else Console.WriteLine("Draw");
            }
        }
        
        static void Main(string[] args)
        {
            game = new Game();
            gameEnded = false;

            while (!gameEnded)
            {
                PrintGameRules();
                PlayerTurn();
                EndPlayerTurn();
            }
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

        }
    }
}
