using UnityEngine;
using System.Collections;

public class triggerMLG : MonoBehaviour {
	
	public GameObject noscopemessage;
	
	private bool spawned = false;

	// Use this for initialization
	
	// Update is called once per frame
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "AdvancedPlayer" && !spawned){
			Debug.Log ("Trigger Message DO A 360 NOSCOPE");
			//GameObject message = (GameObject)Instantiate(noscopemessage, new Vector3(38.73414f, -5.71203f, 29.17138f), Quaternion.Euler (new Vector3(0, 90f, 0)));
			spawned = true;
		}
	}
}
