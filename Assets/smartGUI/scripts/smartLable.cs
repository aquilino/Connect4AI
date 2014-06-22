using UnityEngine;
using System.Collections;

public class smartLable : smartGui {
	public Vector2 position;
	public string text;
	public int FontSize=100;
	
	public override void inGUI(){
		GUI.skin.label.fontSize=FontSize;
		setBox(position.x,position.y,getTextWidth(),getTextHeight());
		scale=100;
		GUI.Label(GetBox(),text);
	}
	
	float getTextWidth(){
		float width=0;
		CharacterInfo temp;
		for(int i=0;i<text.Length;i++){
			GUI.skin.font.GetCharacterInfo(text[i],out temp,FontSize);
			width+=temp.width;
		}
		return width;
	}
	
	float getTextHeight(){
		int lines=1;
		for(int i=0;i<text.Length;i++){
			if(text[i]=='\n')
				lines++;
		}
		return lines*GUI.skin.label.lineHeight;
	}
}
