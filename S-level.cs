using System;
using System.Collections.Generic;

public class Test
{
    private class Point
    {
        public int Y;
        public int X;
        public Point(int y, int x) { Y = y; X = x; }

        public Point Move(Point p)
        {
            return new Point(this.Y + p.Y, this.X + p.X);
        }
    }

    private static bool canMove(char[,] map, int row, int col, Point address)
    {
        if(address.Y < 0 || address.Y >= row || address.X < 0 || address.X >= col)
        {
            return false;
        }

        if(map[address.Y, address.X] == '#')
        {
            return false;
        }
        return true;
    }

    private static int Search(int row, int col, char[,] map)
    {
        writeMap(map, row, col);
        List<Point> direction = new List<Point>{ new Point(1,0), new Point(0,1), new Point(-1,0), new Point(0,-1)};
        Queue<Point> target = new Queue<Point>();
        int[,] mapCount = new int[row, col];
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                mapCount[y, x] = 9999;  // とりあえずMAX
            }
        }

        // スタート地点設定(一律処理のため-1スタート)
        mapCount[0,0] = -1;
        target.Enqueue(new Point(0,0));

        while(target.Count > 0)
        {
            Point current = target.Dequeue();

            if(current.Y == (row - 1) && current.X == (col - 1))
            {
                // ゴールだった
                // スタートとゴールが同じだった場合-1が返ってしまう
                return (mapCount[current.Y, current.X] == -1 ? 0 : mapCount[current.Y, current.X]);
            }

            // 上下左右にturnを設定
            int curTurn = mapCount[current.Y, current.X];
            foreach (Point dir in direction)
            {
                Point tp = current.Move(dir);
                while(canMove(map, row, col, tp))
                {
                    if(mapCount[tp.Y, tp.X] > curTurn + 1)
                    {
                        // 最小ターンでだどり着いたのでキューに設定
                        mapCount[tp.Y, tp.X] = curTurn + 1;
                        target.Enqueue(tp);
                    }
                    tp = tp.Move(dir);
                }
            }
            writeMap(mapCount, row, col);
        }

        return -1;
    }

    private static void writeMap(char[,] map, int row, int col)
    {
        //Console.WriteLine("----------");
        //for (int y = 0; y < row; y++)
        //{
        //    for (int x = 0; x < col; x++)
        //    {
        //        Console.Write(map[y, x]);
        //    }
        //    Console.WriteLine("");
        //}
    }
    private static void writeMap(int[,] map, int row, int col)
    {
        //Console.WriteLine("***********");
        //for (int y = 0; y < row; y++)
        //{
        //    for (int x = 0; x < col; x++)
        //    {
        //        Console.Write(map[y, x] + ",");
        //    }
        //    Console.WriteLine("");
        //}
    }
    static void Main()
    {
        char[,] map = null;
        String line;
        int row = 0;
        int col = 0;

        if ((line = Console.ReadLine()) != null)
        {
            String[] arg = line.Split(' ');
            row = int.Parse(arg[0]);
            col = int.Parse(arg[1]);
            map = new char[row, col];
 
            for (int y = 0; y < row; y++)
            {
                line = Console.ReadLine();
                for (int x = 0; x < col; x++)
                {
                    map[y, x] = line[x];
                }
            }
        }
        
        Console.WriteLine(Search(row, col, map));
    }
}
