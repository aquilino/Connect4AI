using UnityEngine;
using System.Collections;

public class chipsound : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		audio.Play();
		this.enabled=false;
	}
}
