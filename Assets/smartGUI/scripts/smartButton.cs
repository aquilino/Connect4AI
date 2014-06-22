using UnityEngine;
using System.Collections;

public class smartButton : smartGui {
	public string text;
	public int FontSize;
	public Vector2 position;
	public Vector2 dimensions;
	
	public delegate void buttonCallBack();
	public buttonCallBack callBack;
	
	public override void inGUI(){
		GUI.skin.button.fontSize=(int)(FontSize*scale/100);
		setBox(position.x,position.y,dimensions.x,dimensions.y);
		if(GUI.Button(GetBox(),text)){
			callBack();
		}
	}
}
