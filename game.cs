
using System;
using System.Security.Cryptography.X509Certificates;
namespace ConnectFour
{
    public abstract class player
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
            for(int i = 0; i <6; i++)
            
                for (int j = 0; j < 7; j++)
                    grid[i, j] = '.'; 
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
        public bool DropDisc(int column, char symbol)
        {
            if (column < 0 || column >= Cols || IsColumnFull(column))
                return false;
            for (int row=Rows -1; row=>0; row--)
            {
                if (grid[row, column] == '.')
                {
                    grid[row, column] = symbol;
                    return true; 
                }
            }
            return false; 

        }
        public bool IsColumnFull(int column))
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

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Connect Four\n");
            Console.WriteLine("Enter Player 1 name: ");

            string name1 = Console.ReadLine(); 
            player player1 = new player(name1, 'X');

            Console.WriteLine("Enter Player 2name: ");
            string name2 = Console.ReadLine();
            player player2 = new player(name2, 'O');

            player current = player1;
            Board board = new Board(); 
            while(true)
            {
                board.Display();
                Console.WriteLine($"{current.Name} ({current.Symbol}) choose a column (1-7): ");

                bool valid = int.TryParse(Console.ReadLine(), out int col);

                col -= 1; 
                if(!valid || col < 0 || col >= 7)
                {
                    Console.WriteLine("X Invalid input.Try a number from 1 - 7:  ");
                    continue; 

                }
                if(!board.DropDisc(col, current.Symbol))
                {
                    Console.WriteLine(" That column is full. Try a different one: ");
                    continue; 

                    
                }
                if (board.CheckWin(current.Symbol))
                {
                    board.Display();
                    Console.WriteLine($"{current.Name}");
                    break; 

                }
                if (board.IsFull())
                {
                    board.Display();
                    Console.WriteLine(" !!");
                    break; 
                }
                current = (current == player1) ? player2 : player1; 
            }
            Console.WriteLine("\nGame over.Press any key to exit. ");
            Console.ReadLine();

            // kjwjhegefhgwfwgfgwefhwegfhwegefyisfgyi


            //hgeruygfiuegruewu


            // this is iissss for you
            Console.WriteLine("Thank you  ");

        }
    }
 
    }

