using System;
using System.Collections.Generic;

public class Test
{
    enum direction { down, right, up, left };
    private class Point
    {
        public int Y; public int X;
        public Point(int y, int x) { Y = y; X = x; }
    }

    private static List<Point> canMove(Point current, char[,] map)
    {
        List<Point> list = new List<Point>();

        if (map[current.Y + 1, current.X] != '#')
        {
            list.Add(new Point(1, 0));
        }
        if (map[current.Y, current.X + 1] != '#')
        {
            list.Add(new Point(0, 1));
        }
        if (map[current.Y - 1, current.X] != '#')
        {
            list.Add(new Point(-1, 0));
        }
        if (map[current.Y, current.X - 1] != '#')
        {
            list.Add(new Point(0, -1));
        }
        return list;
    }

    private static int Search(Point current, Point vec, char[,] map, int turnCnt)
    {
        writeMap(map, 5, 5);

        // カレントがゴールなら終了
        if (map[current.Y, current.X] == 'g')
        {
            return turnCnt;
        }

        // カレントを移動不可とし次を検索
        char[,] nextMap = new char[66, 66];
        for (int y = 0; y < 66; y++)
        {
            for (int x = 0; x < 66; x++)
            {
                nextMap[y, x] = map[y, x];
            }
        }
        nextMap[current.Y, current.X] = '#';
        List<Point> list = canMove(current, nextMap);
        if (list.Count == 0)
        {
            return 9999;    // 行き詰った
        }
        else
        {
            int minTurn = 9999;
            foreach (Point route in list)
            {
                int nextTurn = turnCnt;
                if (vec != null && vec.Y != route.Y && vec.X != route.X)
                {
                    nextTurn++;     // 方向転換したら+1
                }
                int routeTurn = Search(new Point(current.Y + route.Y, current.X + route.X), route, nextMap, nextTurn);
                if (routeTurn < minTurn)
                {
                    minTurn = routeTurn;
                }
            }
            return minTurn;
        }
    }

    private static void writeMap(char[,] map, int row, int col)
    {
        /*
        Console.WriteLine("----------");
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                Console.Write(map[y, x]);
            }
            Console.WriteLine("");
        }
        */
    }

    static void Main()
    {
        char[,] map = new char[66, 66];
        for (int y = 0; y < 66; y++)
        {
            for (int x = 0; x < 66; x++)
            {
                map[y, x] = '#';
            }
        }

        String line;
        if ((line = Console.ReadLine()) != null)
        {
            String[] arg = line.Split(' ');
            for (int y = 0; y < int.Parse(arg[0]); y++)
            {
                line = Console.ReadLine();
                for (int x = 0; x < int.Parse(arg[1]); x++)
                {
                    map[y + 1, x + 1] = line[x];
                }
            }
            map[1, 1] = 's';
            map[int.Parse(arg[0]), int.Parse(arg[1])] = 'g';
        }
        
        Console.WriteLine(Search(new Point(1, 1), null, map, 0));
    }
}