using System;
namespace SoftwareTeamXiangQi{
  
    public class Board{
        public Chess[,] chesses;

        public Chess white;
        
        
        public Board(){
            chesses = new Chess[10,9];

            //初始化
            for(int row = 0; row < 10; row++){
                for(int col = 0; col < 9 ; col++){
                    chesses[row,col] = new No_chess(row,col,this);
                }
            }


            //红方
            chesses[0,0] = new Rook(0,0,Color.red,Type.Rook,this);
            chesses[0,1] = new Horse(0,1,Color.red,Type.Horse,this);
            chesses[0,2] = new Elephant(0,2,Color.red,Type.Elephant,this);
            chesses[0,3] = new Guard(0,3,Color.red,Type.Guard,this);
            chesses[0,4] = new King(0,4,Color.red,Type.King,this);
            chesses[0,5] = new Guard(0,5,Color.red,Type.Guard,this);
            chesses[0,6] = new Elephant(0,6,Color.red,Type.Elephant,this);
            chesses[0,7] = new Horse(0,7,Color.red,Type.Horse,this);
            chesses[0,8] = new Rook(0,8,Color.red,Type.Rook,this);
            chesses[2,1] = new Cannon(2,1,Color.red,Type.Cannon,this);
            chesses[2,7] = new Cannon(2,7,Color.red,Type.Cannon,this);
            chesses[3,0] = new Soldier(3,0,Color.red,Type.Soilder,this);
            chesses[3,2] = new Soldier(3,2,Color.red,Type.Soilder,this);
            chesses[3,4] = new Soldier(3,4,Color.red,Type.Soilder,this);
            chesses[3,6] = new Soldier(3,6,Color.red,Type.Soilder,this);
            chesses[3,8] = new Soldier(3,8,Color.red,Type.Soilder,this);

            //黑方
            chesses[9,0] = new Rook(9,0,Color.black,Type.Rook,this);
            chesses[9,1] = new Horse(9,1,Color.black,Type.Horse,this);
            chesses[9,2] = new Elephant(9,2,Color.black,Type.Elephant,this);
            chesses[9,3] = new Guard(9,3,Color.black,Type.Guard,this);
            chesses[9,4] = new King(9,4,Color.black,Type.King,this);
            chesses[9,5] = new Guard(9,5,Color.black,Type.Guard,this);
            chesses[9,6] = new Elephant(9,6,Color.black,Type.Elephant,this);
            chesses[9,7] = new Horse(9,7,Color.black,Type.Horse,this);
            chesses[9,8] = new Rook(9,8,Color.black,Type.Rook,this);
            chesses[7,1] = new Cannon(7,1,Color.black,Type.Cannon,this);
            chesses[7,7] = new Cannon(7,7,Color.black,Type.Cannon,this);
            chesses[6,0] = new Soldier(6,0,Color.black,Type.Soilder,this);
            chesses[6,2] = new Soldier(6,2,Color.black,Type.Soilder,this);
            chesses[6,4] = new Soldier(6,4,Color.black,Type.Soilder,this);
            chesses[6,6] = new Soldier(6,6,Color.black,Type.Soilder,this);
            chesses[6,8] = new Soldier(6,8,Color.black,Type.Soilder,this);
        }


        public Turn turn (Turn turn){
            if(turn == Turn.red){
                turn = Turn.black;
            }
            else
                turn = Turn.red;

            return turn;       
        }

    }

    public abstract class Chess{
        public bool valid = false;
        protected Board board;
        public int row;
        public int col;
        public Color color;
        public Type type;
        public string Print;
    

        public (int col, int row) position;

        public Chess(int row, int col, Color color, Type type,string Print,Board board){
            this.row = row;
            this.col = col;
            this.color = color;
            this.type = type;
            this.Print = Print;
            this.board = board;
        }

        public Color GetColor(){
            return this.color;
        }

        public void SetColor(Color color){
            this.color = color;
        }

        public  Type GetType(){
            return this.type;
        }

        public void SetType(Type type){
            this.type = type;
        }

        public void SetRow(int row){
            this.row = row;
        }

        public void SetCol(int col){
            this.col = col;
        }

        
        public string GetPrint(){
            return this.Print;
        }
        public void MoveChess(int start_row, int start_col ,int destination_row, int destination_col){

           // Chess load = new No_chess(start_row,start_col,this.board);
            Chess load =  this.board.chesses[destination_row,destination_col];
            
            load.SetColor(Color.no_color);
            load.SetType(Type.no_chess);

            this.SetRow(destination_row);
            this.SetCol(destination_col);          



            this.board.chesses[destination_row,destination_col] = this;
            this.board.chesses[start_row,start_col] = load;
        }


        public abstract bool CheckValidMove(int destinationColumn, int destinationRow);
    }

    public class No_chess : Chess{
        public No_chess(int row,int col,Board board) : base(row,col,Color.no_color,Type.no_chess," ",board){}

        public override bool CheckValidMove(int destinationRow, int destinationColumn){           
            
            return true;
                            
        }
    }       

   public class Soldier : Chess {

        public Soldier(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"兵",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){   

            if( board.chesses[this.row,this.col].GetColor() == Color.red){ //红兵
                if(this.row < 5){ // 在红方地盘 【自己】
                    if((this.row == destinationRow-1) && (this.col == destinationColumn))
                        valid = true;
                    else{
                        valid = false; 
                        Console.WriteLine("You just can move forward.");
                    }
                }
                else{       // 在黑方地盘 【敌方】
                    if((this.row == destinationRow-1) && (this.col == destinationColumn)) //向前
                        valid = true;
                    else{
                        if((this.row == destinationRow)){    // 左右
                            if(this.col == destinationColumn+1) 
                                valid = true;
                            else if(this.col == destinationColumn-1)
                                valid = true;
                            else
                                valid = false;
                        }
                        else
                            valid = false;
                    }
                }             
            }//红兵的end}

            if( board.chesses[this.row,this.col].GetColor() == Color.black){//黑兵
                //Console.WriteLine(player);
                if(this.row >= 5){// 在黑方地盘 【自己】
                    if((this.row == destinationRow+1) && (this.col == destinationColumn) )
                        valid = true;
                    else
                        valid = false;
                }
                
                else{// 在红方地盘 【敌方】
                    if((this.row == destinationRow+1) && (this.col == destinationColumn)) //向前
                    valid = true;
                    else{
                        if((this.row == destinationRow)){    // 左右
                            if(this.col == destinationColumn+1) 
                            valid = true;
                            else if(this.col == destinationColumn-1)
                            valid = true;
                            else
                            valid = false;
                        }
                        else
                        valid = false;
                    }
                }   
             }//黑兵的end}

            return valid;
            //return true;                           
        }
    }
    public class Cannon : Chess {

        public Cannon(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"炮",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){ 
            if(this.row== destinationRow ||this.col == destinationColumn){//同一直线
                int countChess = 0;
                if(this.row != destinationRow && this.col == destinationColumn){//同一直线下竖走
                    for(int Row = Math.Min(this.row,destinationRow)+1; Row < Math.Max(this.row,destinationRow); Row++){
                        if(board.chesses[Row,this.col].GetType()==Type.no_chess){countChess = countChess+0;}//无棋加0
                        else{countChess++;}//有棋加1
                    }
                    
                }

                if(this.row == destinationRow && this.col != destinationColumn){//同一直线下竖走
                    for(int Col = Math.Min(this.col,destinationColumn)+1; Col < Math.Max(this.row,destinationRow); Col++){
                        if(board.chesses[this.row,Col].GetType()==Type.no_chess){countChess = countChess+0;}//无棋加0
                        else{countChess++;}//有棋加1
                    }
                }

                if(countChess == 0){   //两者中间无棋
                        if(board.chesses[destinationRow,destinationColumn].GetType()==Type.no_chess) //终点无棋 => 直走
                            valid = true;
                        else{         // 终点有棋 => 但是路上无棋 => 不可以跳吃
                            valid = false;
                            Console.WriteLine("There is a chess in the destination.");
                        }
                    }
                else if(countChess == 1){ // 路上有一棋
                        if((board.chesses[destinationRow,destinationColumn].GetColor() != board.chesses[this.row,this.col].GetColor())
                            && board.chesses[destinationRow,destinationColumn].GetType()!=Type.no_chess){
                            valid = true; 
                        }
                        else{
                            valid = false;
                            Console.WriteLine("There more than one chesses between this two chesses.");
                        }
                }
            }
            else{
                valid = false;
                Console.WriteLine("The cannon only can move straight or sideways");
            }


            return valid;                            
        }
    }

    public class Rook : Chess {

        public Rook(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"车",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){     
            if(this.row != destinationRow && this.col != destinationColumn){//位置不在同一条线
                Console.WriteLine("Your Rook is unable to move.");
                valid = false;
            }
            else{//同一条线 
                int control = 0;
                int countChess = 0;//计数；
                do{
                    if(this.row == destinationRow){//横走                   
                    int ymin =  Math.Min(destinationColumn,this.col);//最小量加变量，测试路上有无棋子阻碍
                        for(control = 1; control < Math.Abs(this.col - destinationColumn); control++){
                            if(board.chesses[this.row,ymin+control].GetType() != Type.no_chess ){
                                countChess++;
                            }
                        }     
                    }
                    else if(this.col == destinationColumn){//竖走
                        int xmin =  Math.Min(destinationRow,this.row);
                        // Console.WriteLine(xmin);
                        for(control = 1; control < Math.Abs(this.row - destinationRow); control++){
                            if(board.chesses[xmin+control,this.col].GetType() != Type.no_chess ){
                                countChess++; 
                            }
                        }     
                    }
                }while((this.row == destinationRow && control == Math.Abs(destinationColumn-this.col)-1)||(this.col == destinationColumn && control == Math.Abs(destinationRow-this.row)-1));

                if( countChess == 0 ){
                    //一路上没有棋
                    valid = true; 
                }
                else{//其他情况，不能走
                    Console.WriteLine("Your Rook is stuck.");
                    valid = false;
                }
            }
            return valid;                            
        }
    }

    public class Horse : Chess {

        public Horse(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"马",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){  
            if(Math.Abs(destinationRow-this.row)==1 && Math.Abs(destinationColumn-this.col)==2){ //横着走
                if( board.chesses[this.row,(this.col+destinationColumn)/2].GetType() != Type.no_chess){ //有棋
                    valid = false;
                    Console.WriteLine("Your Horse is stuck.");
                }   
                else{
                    valid = true;                            
                }
            }
            else if(Math.Abs(destinationColumn-this.col)==1 && Math.Abs(destinationRow-this.row)==2){ //竖着走
                if( board.chesses[(this.row+destinationRow)/2,this.col].GetType() != Type.no_chess){ //有棋
                    valid = false;
                    Console.WriteLine("Your Horse is stuck.");      
                }
                else{
                    valid = true;
                }
            } 
            else{
                valid = false;
                Console.WriteLine("You should follow the rule.");                   
            }//}//红黑马end
            return valid;                            
        }
    }

    public class Elephant : Chess {

        public Elephant(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"象",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){   
            if(System.Math.Abs(this.row-destinationRow)==2 && System.Math.Abs(this.col-destinationColumn)==2){//符合两格
                if((destinationRow>4 && board.chesses[row,col].GetColor() == Color.red) //红象过河
                || (destinationRow<5 && board.chesses[row,col].GetColor() == Color.black)){//黑象过河
                    valid = false;
                    Console.WriteLine("Your elephant is stuck.");
                }
                else{
                    if(System.Math.Abs(this.row-destinationRow)==2 && System.Math.Abs(this.col-destinationColumn)==2){//符合两格
                                
                        if(board.chesses[(this.row+destinationRow)/2,(this.col+destinationColumn)/2].GetType() != Type.no_chess){//绊脚
                                valid = false;
                                Console.WriteLine("Your elephant is stuck.");
                        }
                        else { valid = true; }
                    }                
                }
            }
            else{
                valid = false;
                Console.WriteLine("The elepant only can move two squares.");
            }
            return valid;                            
        }
    }

    public class Guard : Chess {

        public Guard(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"士",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){ 
            if((destinationColumn > 2) && (destinationColumn < 6)){
                if(( (destinationRow >= 0) && (destinationRow < 3) && board.chesses[this.row,this.col].GetColor() == Color.red )    //红方米格里
                   || ( (destinationRow > 6) && (destinationRow <= 9) && board.chesses[this.row,this.col].GetColor() == Color.black)){ //黑方米格里
                    
                    if((this.row == destinationRow) || (this.col == destinationColumn)){ //不是斜着
                        valid = false;
                        Console.WriteLine("Sorry, you have to walk diagonally.");
                    }
                    else if((System.Math.Abs(this.row - destinationRow) == 2) || (System.Math.Abs(this.col - destinationColumn) == 2)){ //士走两格
                        valid = false;
                        Console.WriteLine("Sorry, you only can walk diagonally one space.");
                    }
                    else
                        valid = true;
                }
                else{
                    valid = false;
                    Console.WriteLine("Sorry, you only can move in the 米 space.");
                }
            }
            else{
                    valid = false;
                    Console.WriteLine("Sorry, you only can move in the 米 space.");
            }

            return valid;                            
        }
    }

    public class King : Chess {

        public King(int row,int col,Color color,Type type,Board board) : base(row,col,color,type,"将",board){}
        public override bool CheckValidMove(int destinationRow, int destinationColumn){              
                if(board.chesses[this.row,this.col].GetColor() == Color.red){   //红将        
                    if(board.chesses[destinationRow,destinationColumn].GetColor() == Color.black 
                      && board.chesses[destinationRow,destinationColumn].GetType() == Type.King){
                        if(this.col == destinationColumn){//两将同一列
                            for(int theRow = this.row+1; theRow < destinationRow ; theRow++){
                                if(board.chesses[theRow,this.col].GetType() == Type.no_chess) //路上无棋
                                    valid = true;
                                else
                                    valid = false;
                                if( valid == false){// 有棋退出
                                    Console.WriteLine("Sorry, you only can not eat the black_King.There is at least one chess between two Kings");
                                    break;
                                }                                     
                            }
                        }
                        else{//两将不同列
                            valid = false;
                            Console.WriteLine("Sorry, you can not eat the black_King. You are not in same coloumn.");
                        }
                    }
                    else{
                        if((destinationRow >= 0) && (destinationRow < 3) && (destinationColumn > 2) && (destinationColumn < 6)){ //米格里
                            if((this.row == destinationRow) || (this.col == destinationColumn)){
                                if( (System.Math.Abs(this.row - destinationRow) + System.Math.Abs(this.col - destinationColumn) ) == 1 )
                                    valid = true;
                                else{
                                    valid = false;
                                    Console.WriteLine("Sorry, you only can walk one space.");
                                }
                            }
                            else{
                                valid = false;
                                Console.WriteLine("Sorry, you cannot go diagonal.");
                            }
                        }
                        else{
                            valid = false;
                            Console.WriteLine("Sorry, you only can move in the 米 space.");
                        }
                    }
                }//红将end

                 if((board.chesses[this.row,this.col].GetColor() == Color.black)){   //黑将
                    if(board.chesses[destinationRow,destinationColumn].GetColor() == Color.red 
                      && board.chesses[destinationRow,destinationColumn].GetType() == Type.King){
                        if(this.col == destinationColumn){
                            for(int theRow = this.row-1; theRow > destinationRow ; theRow--){
                                if(board.chesses[theRow,this.col].GetType() == Type.no_chess) //路上无棋
                                    valid = true;
                                else
                                    valid = false;
                                if( valid == false){// 有棋退出
                                    Console.WriteLine("Sorry, you only can not eat the red_King.There is at least one chess between two Kings");
                                    break;
                                }                                     
                            }
                        }
                        else{
                            valid = false;
                            Console.WriteLine("Sorry, you can not eat the black_King. You are not in same coloumn.");
                        }
                    }
                    else{
                        if((destinationRow > 6) && (destinationRow <= 9) && (destinationColumn > 2) && (destinationColumn < 6)){ //米格里
                            if((this.row == destinationRow) || (this.col == destinationColumn)){
                                if( (System.Math.Abs(this.row - destinationRow) + System.Math.Abs(this.col - destinationColumn) ) == 1 )
                                    valid = true;
                                else{
                                    valid = false;
                                    Console.WriteLine("Sorry, you only can walk one space.");
                                }
                            }
                            else{
                                valid = false;
                                Console.WriteLine("Sorry, you cannot go diagonal.");
                            }
                        }
                        else{
                            valid = false;
                            Console.WriteLine("Sorry, you only can move in the 米 space.");
                        }
                    }
                }//黑将end

            return valid;                            
        }
    }

    public enum Color{ red, black, no_color}

    public enum Type{ King , Guard , Elephant , Horse , Rook , Cannon , Soilder , no_chess}

    public enum Turn{red,black,start};
    public enum Fail{red,black,no};

}
