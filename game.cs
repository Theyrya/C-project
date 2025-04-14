
using System;
using System.Security.Cryptography.X509Certificates;
namespace ConnectFour
{
    class player
    {
        public string Name { get; set; }
        public char Symbol { get; set; }

        public player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol; 
        }
    }

    class Board
    {
        private char[,] grid = new char[6, 7]; 
        public Board()
        {
            for(int i = 0; i <6; i++)
            
                for (int j = 0; j < 7; j++)
                    grid[i, j] = '.'; 
            }
           public void Display()
        {
            Console.WriteLine("Connect Four Game\n"); 
            for(int i=0; i<6; i++)
            {
                for (int j = 0; j < 7; j++)
                    Console.WriteLine(grid[i, j]);
                Console.WriteLine(); 
            }
            Console.WriteLine("1 2 3 4 5 6 7\n"); 
        }
        public bool DropDisc(int column, char symbol)
        {
            for(int row=5; row>0; row--)
            {
                if (grid[row, column] == '.')
                {
                    grid[row, column] = symbol;
                    return true; 
                }
            }
            return false; 

        }
        public bool IsFull()
        {
            for (int i = 0; i < 7; i++)
                if (grid[0, i] == '.')
                    return false;
            return true; 

        }
        public bool CheckWin(char symbol)
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 4; j++)
                    if (grid[i, j] == symbol && grid[i, j + 1] == symbol
                        && grid[i, j + 2] == symbol && grid[i, j + 3] == symbol)
                        return true;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 7; j++)
                    if (grid[i, j] == symbol && grid[i + 1, j] == symbol &&
                        grid[i + 2, j] == symbol && grid[i + 3, j] == symbol)
                        return true;
            // Diagonal(bottom - left to top - right)
            for (int i = 3; i < 6; i++)
                for (int j = 0; j < 4; j++)
                    if (grid[i, j] == symbol && grid[i - 1, j + 1] == symbol &&
                        grid[i - 2, j + 2] == symbol && grid[i - 3, j + 3] == symbol)
                        return true;

            // Diagonal (top-left to bottom-right)
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 4; j++)
                    if (grid[i, j] == symbol && grid[i + 1, j + 1] == symbol &&
                        grid[i + 2, j + 2] == symbol && grid[i + 3, j + 3] == symbol)
                        return true;

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


            //hgeruygfiuegruewut


            





            console.WritLine("update"); 
        }
    }
 
    }

