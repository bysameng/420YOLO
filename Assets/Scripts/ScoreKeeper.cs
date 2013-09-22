using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	
	public int score;
	
	public MLGeffects effects;

	// Use this for initialization
	void Start () {
		effects = GameObject.Find ("MLGeffectsObject").GetComponent<MLGeffects>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	
	public void addScore(int x, float multiplier){
		score += (int)((float)x * multiplier);
		print (score);
		if (score > 1){
			effects.PlayDubstep(10.0f);
			effects.DisplayLightShow(10.0f);
			effects.ShakeScreen(5.0f, 10.0f);
		}
		
	}
		
}
