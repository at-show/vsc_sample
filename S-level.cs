using System;
using System.Collections.Generic;

public class Test
{
  enum direction {down, right, up, left};
  private class Point
  { public int Y; public int X;
    public Point(int y, int x){Y = y; X = x;}

    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Point c = (Point)obj;
        return ((this.Y == c.Y) && (this.X == c.X));
    }

    public override int GetHashCode()
    {
        return this.Y ^ this.X;
    }
  }

  private static List<Point> canMove(Point current, char[,] map){
      List<Point> list = new List<Point>();

      if(map[current.Y + 1, current.X] != '#'){
          list.Add(new Point(1, 0));
      }
      if(map[current.Y, current.X + 1] != '#'){
          list.Add(new Point(0, 1));
      }
      if(map[current.Y - 1, current.X] != '#'){
          list.Add(new Point(-1, 0));
      }
      if(map[current.Y, current.X - 1] != '#'){
          list.Add(new Point(0, -1));
      }
      return list;
  }

  private static int Search(Point current, Point vec, char[,] map, int turnCnt){
    Console.WriteLine(String.Format("y:{0}, x:{1}", current.Y, current.X));
    // カレントがゴールなら終了
    if(map[current.Y, current.X] == 'g'){
        return turnCnt;
    }

    // カレントを移動不可とし次を検索
    char[,] nextMap = new char[66,66];
    for(int y = 0; y < 66; y++){
        for(int x = 0; x < 66; x++){
            nextMap[y,x] = map[y,x];
        }
    }
    nextMap[current.Y, current.X] = '#';
    List<Point> list = canMove(current, nextMap);
    if(list.Count == 0){
        return 9999;    // 行き詰った
    }
    else
    {
      int minTurn = 9999;
      int nextTurn = turnCnt;
      foreach (Point route in list)
      {
        if(vec != null && vec != route){
            nextTurn++;     // 方向転換したら+1
        }
        int routeTurn = Search(new Point(current.Y + route.Y, current.X + route.X), route, nextMap, nextTurn);
        if(routeTurn < minTurn){
            minTurn = routeTurn;
        }
      }
      Console.WriteLine(String.Format("  y:{0}, x:{1} ->min:{2}", current.Y, current.X, minTurn));
      return minTurn;
    }
  }

  static void Main()
  {
    char[,] map = new char[66,66];
    for(int y = 0; y < 66; y++){
        for(int x = 0; x < 66; x++){
            map[y,x] = '#';
        }
    }

    String line;
    if((line = Console.ReadLine()) != null)
    {
      String[] arg = line.Split(' ');
      for(int y = 0; y < int.Parse(arg[0]); y++){
        line = Console.ReadLine();
        for (int x = 0; x < int.Parse(arg[1]); x++)
        {
            map[y+1,x+1] = line[x];
        }
      }
      map[1,1] = 's';
      map[int.Parse(arg[0]), int.Parse(arg[1])] = 'g';
    }

    for(int y = 1; y < 10; y++){
        for(int x = 1; x < 10; x++){
            map[y,x] = '.';
        }
    }
      map[1,1] = 's';
      map[5,1] = '#';
      map[1,9] = '#';
      map[9,9] = 'g';

    for(int y = 0; y < 66; y++){
        for(int x = 0; x < 66; x++){
            Console.Write(map[y,x]);
        }
        Console.WriteLine("");
    }

      Console.WriteLine(Search(new Point(1,1), null, map, 0));
  }
}