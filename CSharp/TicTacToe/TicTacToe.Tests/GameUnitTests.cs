using System;
using Xunit;

namespace TicTacToe.Tests
{
    public class GameUnitTests
    {
        [Fact]
        public void StartNewGame()
        {
            Game game = new Game();

            Assert.Equal(1, game.CurrentPlayer);

            Assert.Equal(1, game.Turns);
        }

        [Fact]
        public void IncorrectRow1()
        {
            Game game = new Game();

            Assert.Throws<GameException>(() => game.PlayerTurn('+', -1, 1));
        }

        [Fact]
        public void IncorrectRow2()
        {
            Game game = new Game();

            Assert.Throws<GameException>(() => game.PlayerTurn('+', 5, 1));
        }

        [Fact]
        public void IncorrectColumn1()
        {
            Game game = new Game();

            Assert.Throws<GameException>(() => game.PlayerTurn('+', 1, -1));
        }

        [Fact]
        public void IncorrectColumn2()
        {
            Game game = new Game();

            Assert.Throws<GameException>(() => game.PlayerTurn('+', 1, 7));
        }

        [Fact]
        public void UsedSlot()
        {
            Game game = new Game();

            game.PlayerTurn('+', 0, 0);
            Assert.Throws<GameException>(() => game.PlayerTurn('-', 0, 0));
            Assert.False(game.GameEnded());
        }


        [Fact]
        public void TurnNumber()
        {
            Game game = new Game();

            game.PlayerTurn('+', 0, 0);
            Assert.False(game.GameEnded());
            game.PlayerTurn('-', 0, 1);
            Assert.False(game.GameEnded());

            Assert.Equal(2, game.Turns);
            Assert.False(game.GameEnded());
        }

        [Fact]
        public void WinByRow()
        {
            Game game = new Game();
            /*
             * o - -
             * + + +
             * o o o
             */

            game.PlayerTurn('+', 1, 0);
            game.GameEnded();
            game.PlayerTurn('-', 0, 2);
            game.GameEnded();

            Assert.Equal(2, game.Turns);
            game.PlayerTurn('+', 1, 1);
            game.GameEnded();
            game.PlayerTurn('-', 0, 1);
            game.GameEnded();

            Assert.Equal(3, game.Turns);
            game.PlayerTurn('+', 1, 2);
            Assert.True(game.GameEnded());
            Assert.Equal(1, game.Winner);
        }

        [Fact]
        public void WinByColumn()
        {
            Game game = new Game();
            /*
             * o + -
             * + + -
             * o o -
             */

            game.PlayerTurn('+', 1, 0);
            game.GameEnded();
            game.PlayerTurn('-', 0, 2);
            game.GameEnded();

            Assert.Equal(2, game.Turns);
            game.PlayerTurn('+', 1, 1);
            game.GameEnded();
            game.PlayerTurn('-', 1, 2);
            game.GameEnded();

            Assert.Equal(3, game.Turns);
            game.PlayerTurn('+', 0, 1);
            game.GameEnded();
            game.PlayerTurn('-', 2, 2);
            Assert.True(game.GameEnded());
            Assert.Equal(2, game.Winner);
        }

        [Fact]
        public void WinByDiagonal()
        {
            Game game = new Game();
            /*
             * + o o
             * - + -
             * o o +
             */

            game.PlayerTurn('+', 0, 0);
            game.GameEnded();
            game.PlayerTurn('-', 1, 0);
            game.GameEnded();

            Assert.Equal(2, game.Turns);
            game.PlayerTurn('+', 1, 1);
            game.GameEnded();
            game.PlayerTurn('-', 1, 2);
            game.GameEnded();

            Assert.Equal(3, game.Turns);
            game.PlayerTurn('+', 2, 2);
            Assert.True(game.GameEnded());
            Assert.Equal(1, game.Winner);
        }
    }
}
