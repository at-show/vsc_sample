using System;
public class Test
{
　private static int[] coin = {500, 100, 50, 10, 5, 1};

  private static int patternCount(int price, int num){
    if(coin[num] == 1){
        return 1;
    }

    int pattern = 0;
    for(int i=0; price >= coin[num] * i; i++){
        pattern += patternCount(price - (coin[num] * i), num + 1);
    }
    return pattern;
  }

  static void Main()
  {
    String line;
    for(;(line=Console.ReadLine())!=null;)
    {
      String[] arg = line.Split(' ');
      Console.WriteLine(patternCount(int.Parse(arg[0]), 0));
    }
  }
}