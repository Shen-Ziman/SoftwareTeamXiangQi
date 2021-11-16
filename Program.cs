using System;
//using System.Text.RegularExpressions;

namespace SoftwareTeamXiangQi
{
    class Program
    {
        static void Main(string[] args)
        {
            (int,int) start_coordination;
            (int,int) destination;
            (bool,Fail) outcome;
            Turn turn = Turn.start;                                  
            Board board = new Board();
            display Screen = new display();
            display K_display = new display();
            string[,] k_map = K_display.getMap();

            Screen.OriginChess(board);
            

             do{
                Screen.displayTheMap(board);
                turn = Screen.ShowtheTurn(turn,board); // 展示回合
                start_coordination = Screen.SelectChess(turn,board);//起始坐标获得
                destination = Screen.AskAndCheck(turn,start_coordination.Item1,start_coordination.Item2,board); // 目标坐标
                outcome = Screen.MoveChess(start_coordination.Item1,start_coordination.Item2,destination.Item1,destination.Item2,board,k_map); 
                // 是否还循环 ， 谁赢  (loop,fail)
                Console.Clear();
             }while(outcome.Item1);//item1返回loop —> 判断游戏结束->loop=false 

             Screen.displayTheMap(board);
             if(outcome.Item2 == Fail.red){
                 Console.WriteLine("Black Win!");
             }
             else{
                  Console.WriteLine("Red Win!");
             }             
        
            Console.ReadKey(); 
            

            
                       
        }
    }
}
