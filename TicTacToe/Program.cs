using System;

class Program
{
    char[,] board;
    private char winChar;

    public char winPerson
    {
        get { return winChar; }
        set { winChar = value; }
    }

    private bool hasWon;

    public bool isWin
    {
        get { return hasWon; }
        set { hasWon = value; }
    }

    private bool isX;

    public bool isY
    {
        get { return isX; }
        set { isX = value; }
    }

    public void WriteBoard()
    {
        int lineAmount = (int)Math.Sqrt(board.Length) * 4 - 1;
        string line = new string('-', lineAmount);
        for (int x = 0; x < Math.Sqrt(board.Length); x++)
        {
            for (int y = 0; y < Math.Sqrt(board.Length); y++)
            {
                Console.Write(y != Math.Sqrt(board.Length) - 1 ? $" {board[x, y]} |" : $" {board[x, y]} \n");
            }
            Console.WriteLine(x != Math.Sqrt(board.Length) - 1 ? line : null);
        }
    }

    public void CheckWin()
    {
        char[] winSequenceRow = new char[board.Length];
        char[] winSequenceColumn = new char[board.Length];
        char[] winSequenceDiagonal1 = new char[board.Length];
        char[] winSequenceDiagonal2 = new char[board.Length];

        string sequenceX = "";
        string sequenceY = "";

        for (int i = 0; i < Math.Sqrt(board.Length); i++)
        {
            sequenceX += "X";
            sequenceY += "Y";
        }


        for (int x = 0; x < Math.Sqrt(board.Length); x++)
        {
            for (int y = 0; y < Math.Sqrt(board.Length); y++)
            {
                if (x + y == Math.Sqrt(board.Length) - 1)
                {
                    winSequenceDiagonal2[y] = board[x, y];
                }
                winSequenceRow[y] = board[x, y];
                winSequenceColumn[y] = board[y, x];
                winSequenceDiagonal1[y] = board[y, y];
            }
            string checkRow = new string(winSequenceRow);
            string checkColumn = new string(winSequenceColumn);
            string checkDiagonal1 = new string(winSequenceDiagonal1);
            string checkDiagonal2 = new string(winSequenceDiagonal2);


            int winXRow = string.Compare(checkRow, sequenceX);
            int winYRow = string.Compare(checkRow, sequenceY);
            int winXColumn = string.Compare(checkColumn, sequenceX);
            int winYColumn = string.Compare(checkColumn, sequenceY);
            int winXDiagonal1 = string.Compare(checkDiagonal1, sequenceY);
            int winYDiagonal1 = string.Compare(checkDiagonal1, sequenceX);
            int winXDiagonal2 = string.Compare(checkDiagonal2, sequenceY);
            int winYDiagonal2 = string.Compare(checkDiagonal2, sequenceX);
            if (winXRow == 0 || winYRow == 0)
            {
                isWin = true;
                winPerson = winSequenceRow[0];
                break;
            }
            else if (winXColumn == 0 || winYColumn == 0)
            {
                isWin = true;
                winPerson = winSequenceColumn[0];
                break;
            }
            else if (winXDiagonal1 == 0 || winYDiagonal1 == 0)
            {
                isWin = true;
                winPerson = winSequenceDiagonal1[0];
                break;
            }
            else if (winXDiagonal2 == 0 || winYDiagonal2 == 0)
            {
                isWin = true;
                winPerson = winSequenceDiagonal2[0];
                break;
            }
        }
    }

    public void BoardSizeNotNumberError()
    {
        Console.WriteLine();
        Console.WriteLine("Error: input not number (N)!");
        Console.WriteLine("Press any key to try again..");
        Console.ReadKey();
        Console.Clear();
        return;
    }

    public void NotVacantError()
    {
        _error = true;
        Console.WriteLine();
        Console.WriteLine("Error: box not vacant!");
        Console.WriteLine("Press any key to try again..");
        Console.ReadKey();
        return;
    }

    public void InputNotNumberError()
    {
        _error = true;
        Console.WriteLine();
        Console.WriteLine("Error: input not number (1-9)!");
        Console.WriteLine("Press any key to try again..");
        Console.ReadKey();
        return;
    }

    public void DisplayLoss()
    {
        Console.WriteLine();
        Console.WriteLine("No one won.");
        Console.ReadKey();
        Environment.Exit(1);
    }

    private bool hasError;

    public bool _error
    {
        get { return hasError; }
        set { hasError = value; }
    }

    static void Main()
    {
        Program prog = new Program();
        int number;
        string size = "";
        while (!int.TryParse(size, out number))
        {
            Console.Write("Provide desire board size (N): ");
            size = Console.ReadLine();
            if (!int.TryParse(size, out number))
            {
                prog.BoardSizeNotNumberError();
            }
        }
        int moveCount = 0; // check loss
        char askMove; // display X or Y in question
        string userInput;
        int selTemp;
        bool userChoice;
        prog._error = false;
        prog.board = new char[Int64.Parse(size), Int64.Parse(size)];

        for (int x = 0; x < Math.Sqrt(prog.board.Length); x++)
        {
            for (int y = 0; y < Math.Sqrt(prog.board.Length); y++)
            {
                prog.board[x, y] = ' ';
            }
        }

        prog.isY = true;
        Console.WriteLine(" -- Tic Tac Toe -- ");
        Console.Clear();
        while (!prog.isWin)
        {
            if (moveCount == prog.board.Length)
            {
                prog.DisplayLoss();
            }
            if (prog.isY == true) // if is X
            {
                askMove = 'X';
            }
            else
            {
                askMove = 'Y';
            }
            Console.Clear();
            prog.WriteBoard();
            Console.WriteLine();
            Console.WriteLine("What box do you want to place {0} in? (1-{1})", askMove, prog.board.Length);
            Console.Write("> ");
            userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out selTemp))
            {
                prog.InputNotNumberError();
                userChoice = false;
            }
            else
            {
                selTemp = int.Parse(userInput);
                userChoice = true;
            }

            while (userChoice)
            {
                int positionCounter = 1;
                int x = 0;
                int y = 0;
                bool breakes = false;
                for (x = 0; x < Math.Sqrt(prog.board.Length); x++)
                {
                    for (y = 0; y < Math.Sqrt(prog.board.Length); y++)
                    {
                        if (positionCounter == selTemp)
                        {
                            if (prog.board[x, y] == ' ')
                            {
                                prog.board[x, y] = askMove;
                                moveCount++;
                                breakes = true;
                                break;
                            }
                            else
                            {
                                prog.NotVacantError();
                                breakes = true;
                                break;
                            }
                        }
                        positionCounter++;
                    }
                    if (breakes)
                    {
                        break;
                    }
                }
                if (x == Math.Sqrt(prog.board.Length) && y == Math.Sqrt(prog.board.Length))
                {
                    Console.WriteLine();
                    Console.WriteLine("Wrong selection entered!");
                    Console.WriteLine("Press any key to try again..");
                    Console.ReadKey();
                    prog._error = true;
                }
                break;
            }

            if (prog._error)
            {
                prog.CheckWin(); // if error, just check win
                prog._error = !prog._error;
            }
            else
            {
                prog.isY = !prog.isY; // flip boolean
                prog.CheckWin();
            }
        }
        Console.Clear();
        prog.WriteBoard();
        Console.WriteLine();
        Console.WriteLine("The winner is {0}!", prog.winPerson);
        Console.ReadKey();
    }
}