using UnityEngine;
using System.Collections;

public class C4AI {
	public static int getBestMove(bool redMove, int[,] board, int depth){
		int move=0;
		if(redMove){
			float max=Mathf.NegativeInfinity;
			for(int i=0;i<7;i++){
				int col = FindTop(board,i);
				if(col!=-1){
					float result= getMoveValue(!redMove,getNewBoard(board,i,col,1),depth-1,i,col);
					if (result>max){
						max = result;
						move=i;
					}
				}
			}
		}
		else{
			float min=Mathf.Infinity;
			for(int i=0;i<7;i++){
				int col = FindTop(board,i);
				if(col!=-1){
					float result= getMoveValue(!redMove,getNewBoard(board,i,col,-1),depth-1,i,col);
					if (result<min){
						min = result;
						move=i;
					}
				}
			}
		}
		return move;
	}
	private static float getMoveValue(bool redMove, int[,] board, int depth, int lastmoveX, int lastMoveY){
		if(checkWin(board,lastmoveX,lastMoveY)){
			if(redMove){
				return Mathf.NegativeInfinity;
			}
			else{
				return Mathf.Infinity;
			}
		}
		else if(isDraw(board)){
			return 0;
		}
		else if(depth<0){
			return evalboard(board);
		}
		else if(redMove){
			//return max
			float max=Mathf.NegativeInfinity;
			for(int i=0;i<7;i++){
				int col = FindTop(board,i);
				if(col!=-1){
					float result= getMoveValue(!redMove,getNewBoard(board,i,col,1),depth-1,i,col);
					if (result>max){
						max = result;
					}
				}
			}
			return max;
		}
		else{
			//return min
			float min=Mathf.Infinity;
			for(int i=0;i<7;i++){
				int col = FindTop(board,i);
				if(col!=-1){
					float result= getMoveValue(!redMove,getNewBoard(board,i,col,-1),depth-1,i,col);
					if (result<min){
						min = result;
					}
				}
			}
			return min;
		}

	}
	private static int[,] getNewBoard(int[,] board,int x,int y,int value){
		int[,] newBoard = new int[7,6];
		for(int i=0;i<7;i++){
			for(int j=0;j<6;j++){
				newBoard[i,j]=board[i,j];
			}
		}
		newBoard[x,y]=value;
		return newBoard;
	}
	public static float evalboard( int[,] board ){
		float ret=0;
		for(int i=0;i<7;i++){
			int col = FindTop(board,i);
			if(col==-1){
				continue;
			}
			else{
				ret+=computeValueForLocation(board,i,col);
			}
		}
		return ret;
	}
	private static int FindTop(int[,] board, int row){
		int ret=-1;
		for(int j=0;j<6;j++){
			if(board[row,j]==0){
				ret=j;
				break;
			}
		}
		return ret;
	}
	private static int computeValueForLocation(int[,] board, int row, int col){
		int ret=0;
		ret+= lookInDirection(board,row,col,-1,-1);
		ret+= lookInDirection(board,row,col,-1,0);
		ret+= lookInDirection(board,row,col,-1,1);
		ret+= lookInDirection(board,row,col,1,-1);
		ret+= lookInDirection(board,row,col,1,0);
		ret+= lookInDirection(board,row,col,1,1);
		ret+= lookInDirection(board,row,col,0,1);
		ret+= lookInDirection(board,row,col,0,-1);
		return ret;
	}
	private static int lookInDirection(int[,] board, int x, int y, int directionVert,int directionHor){
		int ret=0;
		if(x+directionHor < 0 || x+directionHor > 6 || y+directionVert <0 || y+directionVert >5){
			return 0;
		}
		int first=board[x+directionHor,y+directionVert];
		int vertOffset= directionVert;
		int horOffset = directionHor;
		//Debug.Log("looking at board["+(x+vertOffset)+","+""+"]");
		while(board[x+horOffset,y+vertOffset]!=0 && board[x+horOffset,y+vertOffset]==first){
			ret+=board[x+horOffset,y+vertOffset];
			vertOffset+=directionVert;
			horOffset+=directionHor;
			if(x+horOffset < 0 || x+horOffset > 6 || y+vertOffset <0 || y+vertOffset >5 ){
				break;
			}
		}
		return ret;
	}
	static bool checkWin(int [,] board,int x, int y){
		if(board[x,y]==0){
			return false;
		}
		else if(board[x,y]==-1){
			if(lookForFour(board,x,y,1,-1,0,-1,false)){
				return true;
			}
			else if(lookForFour(board,x,y,1,0,0,-1,false)){
				return true;
			}
			else if(lookForFour(board,x,y,1,1,0,-1,false)){
				return true;
			}
			else if(lookForFour(board,x,y,0,1,0,-1,false)){
				return true;
			}
			else{
				return false;
			}
		}
		else{
			if(lookForFour(board,x,y,1,-1,0,1,false)){
				return true;
			}
			else if(lookForFour(board,x,y,1,0,0,1,false)){
				return true;
			}
			else if(lookForFour(board,x,y,1,1,0,1,false)){
				return true;
			}
			else if(lookForFour(board,x,y,0,1,0,1,false)){
				return true;
			}
			else{
				return false;
			}
		}
	}
	static bool lookForFour(int [,] board, int x, int y, int directionVert,int directionHor, int count, int lookFor, bool reversed){
		if(x>6 || x<0 || y<0 || y>5 || board[x,y]!=lookFor){
			return false;
		}
		count++;
		if(count==4){
			return true;
		}
		bool next = lookForFour(board,x+directionHor,y+directionVert,directionVert,directionHor,count,lookFor,reversed);
		if(next==false && !reversed){
			return lookForFour(board,x,y,-1*directionVert,-1*directionHor,0,lookFor,!reversed);
		}
		else if(next==false && reversed){
			return false;
		}
		else{
			return true;
		}
	}
	static bool isDraw(int[,] board){
		bool ret=true;
		for(int i =0;i<7;i++){
			if(board[i,5]==0){
				ret=false;
				break;
			}
		}
		return ret;
	}
}
