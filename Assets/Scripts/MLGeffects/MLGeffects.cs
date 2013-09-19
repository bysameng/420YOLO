using UnityEngine;
using System.Collections;

public class MLGeffects : MonoBehaviour {
	
	public int mlgEffect;
	
	public bool displayingMLG;
	
	public Texture2D MLG1;
	public Texture2D MLG2;
	
	private float effectTimer;

	// Use this for initialization
	void Start () {
		effectTimer = 5.0f;
		mlgEffect = 0;
	}
	
	void OnGUI () {
		if (mlgEffect > 0){
			effectTimer -= Time.deltaTime;
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),MLG1);
			displayingMLG = true;
		}
		
		if (effectTimer < 0){
			mlgEffect = 0;
			effectTimer = 0.1f;
		}
			
	}
}
