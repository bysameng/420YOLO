using UnityEngine;
using System.Collections;

public class EnemyIdentity : MonoBehaviour {
	
	public float hitpoints;
	public int worth;
	public Color color;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool ApplyDamage(float damage){
		hitpoints -= damage;
		if (hitpoints <= 0){
			return true;
		}
		else return false;
	}
}
