using System;
using System.Text.RegularExpressions;
namespace SoftwareTeamXiangQi
{
  public class display{
    string[,] map = new string[29,26];
    Regex test = new Regex (@"(\d+)\s*,\s*(\d+)");
    string text;     
    public display(){   
        //全部给空格两个
        //初始化没有棋子
        for(int row = 0 ; row < 29; row++){
          for(int col = 0 ; col < 26 ; col++){

            map[row,col] = "  ";
          }
        }           
        //米格
        /* 
          坐标(1,10)(2,11)(4,13)(5,14) (22,10)(23,11)(25,13)(26,14)
          坐标(1,14)(2,13)(4,11)(5,10) (22,14)(23,13)(25,11)(26,10)
        */
        int midNum1 = 0;
        int midNum2 = 0;
        for(int row = 1; row < 27; row++){
            if( (row == 1) || (row == 22)){
               midNum1 = 10;
               midNum2 = 14;
            }
            else{
               midNum1++;
               midNum2--;
            }

            map[row,midNum1] = " ╲";
            map[row,midNum2] = " ╱";

            if(row == 5)
            row = 21;
        } 

        //┼字
        for(int row = 3; row < 25; row = row+3 ){
            for(int col = 3; col < 22; col = col+3){
                if((row < 12) || ( row > 15))
                map[row,col] = " ┼";
            }
        }


        //左右 ╟,╢
        for(int row = 3 ; row < 25 ; row = row+3){
          map[row,0] = " ╟" ;
          map[row,24] = " ╢ ";
        }

        //竖
        for(int row=1; row<28 ;row++){
            for(int col = 0;col<25;col=col+3){
              if(row%3!=0){
                if(col==0){
                  map[row,col] = " ║";
                }
                else if(col==24){
                  map[row,col] = " ║ ";
                }
                else if(row<12||row>15){   
                  map[row,col] = " │";
                }
              } 
            }
          }

        //横
        for(int row = 0; row < 28 ;row = row+3){
          for(int col = 1; col < 25; col++){
              if(col%3!=0){
                if(row==0||row==27){
                  map[row,col] =" ═";
                }
                else{   
                  map[row,col] = " ─";
                }
              } 
          }         
        }

         //4 个角
          map[0,0] = " ╔";
          map[0,24] = " ╗ ";
          map[27,0] = " ╚";
          map[27,24] = " ╝ ";

        //楚河汉界
          map[13,4]="楚";
          map[13,8]="河";
          map[14,17]="汉";
          map[14,20]="界";

        //上下边框╤ ╧ && 楚河汉界上下┴ ┬
        for(int row=0; row<28; row = row+3){//上下╤ ╧
            for(int col=3 ; col<22; col=col+3){
                if(row == 0){
                  map[row,col] = " ╤";  
                }   
                if(row == 27){
                  map[row,col] = " ╧"; 
                }    
                if(row ==12 ){
                  map[row,col] = " ┴";
                } 
                else if( row==15){
                  map[row,col] = " ┬";
                }            
            }
          }

        for(int row=0;row<26;row=row+3){
          int col = row/3;
          map[28,row] = " "+Convert.ToString(col);           
          map[row,25] = " "+Convert.ToString(col);

        }
        map[27,25] = " 9";
        map[28,25] = "   ";    
     }

    public string[,] getMap(){
      return map;
    }

    
    //注意 中文前给空格， 中文后的把空格删掉
    public void OriginChess(Board board){
        for(int row = 0; row < 10; row++){
            for(int col = 0; col < 9 ; col++){
              if(board.chesses[row,col] != null){
                  map[row*3,col*3] = " "+board.chesses[row,col].Print;
              } 
            }
          }

          for(int row = 2; row <= 7; row = row+5){
              map[3*row,4] = "─";
              map[3*row,22] = "─";
          }

          for(int col = 1; col < 25 ; col = col+6 ){
              map[9,col] = "─";
              map[18,col] = "─";
            }
          
          for(int col = 1; col < 25 ; col = col + 3){
              map[0,col] = "═";
              map[27,col] = "═";
            }
    }
    public void displayTheMap(Board board){
      Console.BackgroundColor = ConsoleColor.DarkYellow; 
        for(int row = 0 ; row < 29; row++){
          for(int col = 0 ; col < 26 ; col++){
            if(row%3 ==0 && col%3 ==0 ){
              if(board.chesses[row/3,col/3] == null){
                Console.ForegroundColor = ConsoleColor.Black;
              }
              else{
              if(board.chesses[row/3,col/3].color ==Color.red){//红字
                Console.ForegroundColor = ConsoleColor.DarkRed;
              }
              else {
                Console.ForegroundColor = ConsoleColor.Black;//黑字
              }
              }
            }
            else{
              Console.ForegroundColor = ConsoleColor.Black;
            }
                 Console.Write(map[row,col]);
          }
          Console.WriteLine();
        }
          Console.BackgroundColor = ConsoleColor.Black;//改变棋盘下面的背景->黑色
          Console.ForegroundColor = ConsoleColor.White;//改变棋盘下面的字体->白色
      }

       public Turn ShowtheTurn(Turn turn,Board board){
      turn = board.turn(turn);
      if(turn == Turn.red ){
        Console.WriteLine("It's the turn of Red Player!");
      }
      else
        Console.WriteLine("It's the turn of Black Player!");

      return turn;
    }

    public (int,int) SelectChess(Turn turn,Board board){
      int start_row,start_col;
      bool flow = true;
      bool select = true;

      do{
        Console.WriteLine("Please enter the coordination chess you want to move");
        text = Console.ReadLine();

        Match match = test.Match(text);

        start_row = Convert.ToInt32(match.Groups[1].Value);
        start_col = Convert.ToInt32(match.Groups[2].Value);

        if(start_row > 9 || start_col > 8){  
          flow = true;  //继续do-while
          Console.WriteLine("Please enter x < 10, y < 9.");
        }
        else
          flow = false;

        if(board.chesses[start_row,start_col] == null){
          select = true;
          Console.WriteLine("You don't choose any chess.");
        }
        else{ 
          Color player;     //本回合颜色
          if(turn == Turn.red)
            player = Color.red;
          else
            player = Color.black;


          if(board.chesses[start_row,start_col].color == player)
              select = false;
          else{
              select = true;
              Console.WriteLine($"Please choose {player} chess.");
          }
         
        }
      }while(flow || select); //溢出 => true 或 选择非本回合棋子/空 => true  => (||)do-while循环 有1个true就为true
    
       return (start_row,start_col); //selectChese返回start的坐标x,y
    } 


    public (int,int) AskAndCheck(Turn turn, int start_row, int start_col, Board board){
      bool run = false;
      int destination_row,destination_col;

      do{
        Console.WriteLine("Please enter the destination.");
        text = Console.ReadLine();

        Match match = test.Match(text);

        destination_row = Convert.ToInt32(match.Groups[1].Value);
        destination_col = Convert.ToInt32(match.Groups[2].Value);


        if(destination_row > 9 || destination_col > 8){ 
          Console.WriteLine("Please enter x < 10, y < 9.");
          run = false;
        }
        else{ 
            if(board.chesses[destination_row,destination_col]==null){
              run = board.chesses[start_row,start_col].CheckValidMove(destination_row,destination_col);
            }
            else if (board.chesses[destination_row,destination_col].color == board.chesses[start_row,start_col].color){
              run = false;
              Console.WriteLine($"You can't select {board.chesses[start_row,start_col].color} chess.");
            }
            else{
              run  = board.chesses[start_row,start_col].CheckValidMove(destination_row,destination_col);
            }
        }
          
      }while(!run); //不能运行 => run = false  ==> 要do-while

      return (destination_row,destination_col);

    }

    public (bool,Fail) MoveChess(int start_row, int start_col, int destination_row, int destination_col, Board board, string[,] k_map){
      bool loop = true;
      Fail fail = Fail.no;
      

      if(board.chesses[destination_row,destination_col]==null){
        loop  = true;
      }
      else{
        if(board.chesses[destination_row,destination_col].type == Type.King){ //目的地是不是将
          loop = false;
        if(board.chesses[destination_row,destination_col].color == Color.red)  //哪方将？
          fail = Fail.red;
        else 
          fail = Fail.black;
      }
      else
        loop = true;
      }
          
     board.chesses[start_row,start_col].MoveChess(start_row,start_col,destination_row,destination_col);
     
     map[start_row*3,start_col*3+1] = k_map[start_row*3,start_col*3+1];

     map[destination_row*3,destination_col*3] = map[start_row*3,start_col*3];
     map[start_row*3,start_col*3] = k_map[start_row*3,start_col*3];
     
     if(destination_col == 8){
       map[destination_row*3,destination_col*3+1] = k_map[destination_row*3,destination_col*3+1];
     }
     else{
       if(destination_row == 0 || destination_row == 9){
         map[destination_row*3,destination_col*3+1] = "═";
       }
       else
        map[destination_row*3,destination_col*3+1] = "─";
     }
      return (loop,fail);
    }    
  }   

}
