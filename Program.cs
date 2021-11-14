using System;
using System.Text.RegularExpressions;

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
            display a = new display();
            display K_display = new display();
            string[,] k_map = K_display.getMap();

            a.OriginChess(board);
            

             do{
                a.displayTheMap(board);
                turn = a.ShowtheTurn(turn,board); // 展示回合
                start_coordination = a.SelectChess(turn,board);//起始坐标获得
                destination = a.AskAndCheck(turn,start_coordination.Item1,start_coordination.Item2,board); // 目标坐标
                outcome = a.MoveChess(start_coordination.Item1,start_coordination.Item2,destination.Item1,destination.Item2,board,k_map); 
                // 是否还循环 ， 谁赢  (loop,fail)
                //Console.Clear();
             }while(outcome.Item1);

             a.displayTheMap(board);
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
