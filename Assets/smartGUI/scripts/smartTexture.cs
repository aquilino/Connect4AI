using UnityEngine;
using System.Collections;

public class smartTexture : smartGui {

	public Vector2 position;
	public Texture image;
	
	public override void inGUI(){
		setBox(position.x,position.y,image.width,image.height);
		GUI.DrawTexture(GetBox(),image);
	}
}
