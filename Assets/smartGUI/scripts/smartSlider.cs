using UnityEngine;
using System.Collections;

public class smartSlider : smartGui {
	public Vector2 position;
	public Vector2 dimensions;
	public float left;
	public float right;
	private float sliderValue;
	public float thumbWidth;
	
	private GUIStyle sliderStyle;
	private GUIStyle thumbStyle;
	
	public float getValue(){
		return sliderValue;
	}
	
	public void setValue(float a){
		if(a<Mathf.Min (left,right))
			sliderValue=Mathf.Min (left,right);
		else if(a>Mathf.Max(left,right))
			sliderValue=Mathf.Max(left,right);
		else
			sliderValue=a;
					
	}
	
	public override void inGUI(){
		setBox(position.x,position.y,dimensions.x,dimensions.y);
		initStyle();
		thumbStyle.fixedWidth=thumbWidth*scale/100;
		sliderValue=GUI.HorizontalSlider(GetBox(),sliderValue,left,right);
	}
	
	void initStyle(){
		sliderStyle="horizontalslider";
		sliderStyle.fixedHeight=0;
		thumbStyle="horizontalsliderthumb";
		thumbStyle.fixedHeight=0;
	}
}
