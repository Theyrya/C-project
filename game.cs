
using ConnectFour;
using System;
using System.Security.Cryptography.X509Certificates;
namespace ConnectFour
{
    public abstract class Player
    {
        public string Name { get; set; }
        public char DiscSymbol { get; set; }

        public abstract int MakeMove(Board board);
    }
    public class HumanPlayer : Player
    {
        public HumanPlayer(char symbol, string name)
        {
            DiscSymbol = symbol;
            Name = name;
        }

        public override int MakeMove(Board board)
        {
            int column;
            bool isValid;

            do
            {
                Console.Write($"{Name}'s turn ({DiscSymbol}). Enter column (1-7): ");
                string input = Console.ReadLine();

                isValid = int.TryParse(input, out column) &&
                         column >= 1 &&
                         column <= 7 &&
                         !board.IsColumnFull(column - 1);

                if (!isValid)
                {
                    Console.WriteLine("Invalid input or column is full. Please try again.");
                }
            } while (!isValid);

            return column - 1; // Convert to 0-based index
        }
    }

    public class Board
    {
        private const int Rows = 6;
        private const int Cols = 7;
        private char[,] grid = new char[Rows, Cols];
        public Board()
        {
            for (int i = 0; i < Rows; i++)

                for (int j = 0; j < Cols; j++)
                    grid[i, j] = ' ';
        }
        public void Display()
        {
            Console.Clear();
            Console.WriteLine(" 1 2 3 4 5 6 7");
            Console.WriteLine("---------------");

            for (int row = 0; row < Rows; row++)
            {
                Console.Write("|");
                for (int col = 0; col < Cols; col++)
                {
                    Console.Write(grid[row, col]);
                    Console.Write("|");
                }
                Console.WriteLine();
                Console.WriteLine("---------------");

            }
        }
        public bool DropDisc(int column, char disc)
        {
            if (column < 0 || column >= Cols || IsColumnFull(column))
                return false;

            for (int row = Rows - 1; row >= 0; row--)
            {
                if (grid[row, column] == ' ')
                {
                    grid[row, column] = disc;
                    return true;
                }
            }

            return false;
        }

        public bool IsColumnFull(int column)
        {
            return grid[0, column] != ' ';

        }
        public bool IsBoardFull()
        {
            for (int col = 0; col < Cols; col++)
            {
                if (!IsColumnFull(col))
                    return false;
            }
            return true;
        }
        public bool CheckForWin(char disc)
        {
            // Check horizontal
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols - 3; col++)
                {
                    if (grid[row, col] == disc &&
                        grid[row, col + 1] == disc &&
                        grid[row, col + 2] == disc &&
                        grid[row, col + 3] == disc)
                    {
                        return true;
                    }
                }
            }

            // Check vertical
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    if (grid[row, col] == disc &&
                        grid[row + 1, col] == disc &&
                        grid[row + 2, col] == disc &&
                        grid[row + 3, col] == disc)
                    {
                        return true;
                    }
                }
            }

            // Check diagonal (top-left to bottom-right)
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 0; col < Cols - 3; col++)
                {
                    if (grid[row, col] == disc &&
                        grid[row + 1, col + 1] == disc &&
                        grid[row + 2, col + 2] == disc &&
                        grid[row + 3, col + 3] == disc)
                    {
                        return true;
                    }
                }
            }

            // Check diagonal (bottom-left to top-right)
            for (int row = 3; row < Rows; row++)
            {
                for (int col = 0; col < Cols - 3; col++)
                {
                    if (grid[row, col] == disc &&
                        grid[row - 1, col + 1] == disc &&
                        grid[row - 2, col + 2] == disc &&
                        grid[row - 3, col + 3] == disc)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
    public class Game
    {
        private Board board;
        private Player[] players;
        private int currentPlayerIndex;

        public Game(string player1Name, string player2Name)
        {
            board = new Board();
            players = new Player[2];
            players[0] = new HumanPlayer('X', player1Name);
            players[1] = new HumanPlayer('O', player2Name);
            currentPlayerIndex = 0;
        }

        public void Start()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                board.Display();
                Player currentPlayer = players[currentPlayerIndex];

                int column = currentPlayer.MakeMove(board);
                board.DropDisc(column, currentPlayer.DiscSymbol);

                if (board.CheckForWin(currentPlayer.DiscSymbol))
                {
                    board.Display();
                    Console.WriteLine($"{currentPlayer.Name} ({currentPlayer.DiscSymbol}) wins!");
                    gameOver = true;
                }
                else if (board.IsBoardFull())
                {
                    board.Display();
                    Console.WriteLine("The game is a draw!");
                    gameOver = true;
                }
                else
                {
                    currentPlayerIndex = (currentPlayerIndex + 1) % 2;
                }
            }

            AskToPlayAgain();
        }

        private void AskToPlayAgain()
        {
            Console.Write("Would you like to play again? (y/n): ");
            string input = Console.ReadLine().ToLower();

            if (input == "y")
            {
                Console.Clear();
                Game newGame = new Game(players[0].Name, players[1].Name);
                newGame.Start();
            }
            else
            {
                Console.WriteLine("Thanks for playing Connect Four!");
            }
        }


        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Welcome to Connect Four!");
                string player1;
                do
                {
                    Console.Write("Enter Player 1's name: ");
                    player1 = Console.ReadLine()?.Trim();
                } while (string.IsNullOrEmpty(player1));

                string player2;
                do
                {
                    Console.Write("Enter Player 2's name: ");
                    player2 = Console.ReadLine()?.Trim();
                } while (string.IsNullOrEmpty(player2));


                Game game = new Game(player1, player2);
                game.Start();
            }
        }
    }
}