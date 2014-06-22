using UnityEngine;
using System.Collections;

public class smartToggle : smartGui {
	private bool on;
	public Vector2 position;
	private GUIStyle style;
	
	
	public bool isOn(){
		return on;
	}
	
	public void setOn(bool a){
		on=a;
	}
	
	public override void inGUI(){
		setBox(position.x,position.y,100f,100f);
		style="toggle";
		style.border.top=0;
		style.border.left=0;
		style.fixedHeight=GetBox().height;
		style.fixedWidth=GetBox().width;
		on=GUI.Toggle(GetBox(),on,"",style);
	}
}
