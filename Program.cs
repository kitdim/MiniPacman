using System.Net;

class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        char[,] map = ReadMap("map.txt");
        var pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

        Task.Run(() =>
        {
            while (true)
            {
                pressedKey = Console.ReadKey();
            }
        });

        var pacmanX = 1;
        var pacmanY = 1;
        var score = 0;
        while (true)
        {
            Console.Clear();
            HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);

            Console.ForegroundColor = ConsoleColor.Blue;
            DrawMap(map);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pacmanX, pacmanY);
            Console.Write("@");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 13);
            Console.Write($"Score: {score}");

            Thread.Sleep(1000);
        }
    }


    private static char[,] ReadMap(string path)
    {
        string[] file = File.ReadAllLines("map.txt");
        char[,] map = new char[GetMaxLenghtOfLine(file), file.Length];

        for (var x = 0; x < map.GetLength(0); x++)
            for (var j = 0; j < map.GetLength(1); j++)
                map[x, j] = file[j][x];

        return map;
    }

    private static void DrawMap(char[,] map)
    {
        for (var j = 0; j < map.GetLength(1); j++)
        {
            for (var x = 0; x < map.GetLength(0); x++)
                Console.Write(map[x, j]);
            Console.Write("\n");
        }
    }

    private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
    {
        int[] direction = GetDirection(pressedKey);

        var nextPacmanPositionX = pacmanX + direction[0];
        var nextPacmatPositionY = pacmanY + direction[1];

        char nextCell = map[nextPacmanPositionX, nextPacmatPositionY];

        if (nextCell == ' ' || nextCell == '.')
        {
            pacmanX = nextPacmanPositionX;
            pacmanY = nextPacmatPositionY;

            if (nextCell == '.')
            {
                score++;
                map[nextPacmanPositionX, nextPacmatPositionY] = ' ';
            }
        }
    }
    private static int[] GetDirection(ConsoleKeyInfo pressedKey)
    {
        int[] direction = { 0, 0 };
        if (pressedKey.Key == ConsoleKey.UpArrow)
            direction[1] = -1;
        else if (pressedKey.Key == ConsoleKey.DownArrow)
            direction[1] = 1;
        else if (pressedKey.Key == ConsoleKey.LeftArrow)
            direction[0] = -1;
        else if (pressedKey.Key == ConsoleKey.RightArrow)
            direction[0] = 1;
        return direction;
    }

    private static int GetMaxLenghtOfLine(string[] lines)
    {
        int maxLenght = lines[0].Length;

        foreach (var el in lines)
        {
            if (el.Length > maxLenght)
                maxLenght = el.Length;
        }
        return maxLenght;
    }
}