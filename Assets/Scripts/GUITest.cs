using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {
	
	private bool menu;
	
	void OnGUI(){
		float rotationPower = (GameObject.Find ("Player").GetComponent<RotationCounter>()).powerToGUI;
		GUI.Box (new Rect(0,0,100,50), rotationPower.ToString ());
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{Application.Quit ();}
	}
	
	void Start(){
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}
}
	
