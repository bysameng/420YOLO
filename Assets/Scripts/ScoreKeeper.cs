using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	
	public int score;
	public MLGeffects effects;

	// Use this for initialization
	void Start () {
		effects = GameObject.Find ("MLGeffectsObject").GetComponent<MLGeffects> ();
	}
	
	
	
	// Update is called once per frame
	void Update () {
	}
	
	
	
	public int addScore (int x, float multiplier, float rotation) {
		
		int scoreValue = 0;
		
		addScore (x, multiplier);
		if (rotation >= 340) {
			effects.PlayDubstep (10.0f);
			effects.DisplayLightShow (10.0f);
			effects.ShakeScreen (5.0f, 10.0f);
			effects.DisplayWords ("420YOLO", transform.position, 1f);
			Destroy(GameObject.Find("noscopemessage"));
			scoreValue = 1;
		}
		
		return scoreValue;

	}
	
	
	
	public int addScore (int x, float multiplier) {
		score += (int)((float)x * multiplier);
		return score;

	}
		
}
