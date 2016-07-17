using System;

public class Test
{
  private static int count(int valueMax, int targetCnt)
  {
    int hitCnt = 0;

    for(int i = 0; i <= valueMax; i++){
      String str = Convert.ToString(i, 2);
      int cnt = 0;
      foreach (char c in str)
      {
          if(c == '1'){cnt++;}
      }

      if(cnt == targetCnt){
        hitCnt++;
      }
    }
    return hitCnt;
  }

  static void Main(){
    String line;
    for(;(line=Console.ReadLine())!=null;){
      String[] arg = line.Split(' ');
      Console.WriteLine(count(int.Parse(arg[0]), int.Parse(arg[1])));
    }
  }
}