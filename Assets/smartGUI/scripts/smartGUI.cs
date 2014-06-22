using UnityEngine;
using System.Collections;

public enum pivotPoint{
	topLeft,topCenter,topRight,middleLeft,middleCenter,middleRight,bottomLeft,bottomCenter,bottomRight
}
[ExecuteInEditMode]
public class smartGui : MonoBehaviour {
	
	Vector2 targetRes;
	
	
	public pivotPoint centerPoint;
	public float scale=100f;
	
	private Rect Box;

	public Rect GetBox(){
		Rect temp=new Rect(0,0,0,0);
		temp.width=Box.width*scale/100f;
		temp.height=Box.height*scale/100f;
		
		switch(centerPoint){
			case pivotPoint.topLeft:
				temp.x=Box.x;
				temp.y=Box.y;
				break;
			case pivotPoint.topCenter:
				temp.x=Box.x-temp.width/2;
				temp.y=Box.y;
				break;
			case pivotPoint.topRight:
				temp.x=Box.x-temp.width;
				temp.y=Box.y;
				break;
			case pivotPoint.middleLeft:
				temp.x=Box.x;
				temp.y=Box.y-temp.height/2;
				break;
			case pivotPoint.middleCenter:
				temp.x=Box.x-temp.width/2;
				temp.y=Box.y-temp.height/2;
				break;
			case pivotPoint.middleRight:
				temp.x=Box.x-temp.width;
				temp.y=Box.y-temp.height/2;
				break;
			case pivotPoint.bottomLeft:
				temp.x=Box.x;
				temp.y=Box.y-temp.height;
				break;
			case pivotPoint.bottomCenter:
				temp.x=Box.x-temp.width/2;
				temp.y=Box.y-temp.height;
				break;
			case pivotPoint.bottomRight:
				temp.x=Box.x-temp.width;
				temp.y=Box.y-temp.height;
				break;
			default:
				break;
		}
		
		return temp;
	}
	
	public void setBox(float x,float y, float width, float heigth){
		Box.x=x;
		Box.y=y;
		Box.width=width;
		Box.height=heigth;
	}
	
	void Awake(){
		targetRes=new Vector2(1080,1920);
	}
	
	public float least(float a, float b){
		if(a<b)
			return a;
		else
			return b;
	}
	
	void GUIScaleToScreen(){
		float scale;
		float tranY;
		float tranX;
		Vector2 ratio= new Vector2(Screen.width/targetRes.x,Screen.height/targetRes.y);
		scale=least(ratio.x,ratio.y);
		tranX=(Screen.width-targetRes.x*scale)/2f;
		tranY=(Screen.height-targetRes.y*scale)/2f;
		Vector3 T=new Vector3(tranX,tranY,0f);
		Quaternion R=Quaternion.Euler(0f,0f,0f);
		Vector3 S=new Vector3(scale,scale,1f);
		Matrix4x4 GUISCALE=Matrix4x4.TRS(T,R,S);
		GUI.matrix=GUISCALE;
	}

	void OnGUI(){
		GUIScaleToScreen();
		inGUI();
	}
	
	public virtual void inGUI(){}
}
