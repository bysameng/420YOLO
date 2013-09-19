using UnityEngine;
using System.Collections;

public class triggerMLG : MonoBehaviour {
	
	public GameObject mlgHuge;
	
	private bool spawned;

	// Use this for initialization
	void Start () {
		spawned = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "Player" && !spawned){
			Debug.Log ("TriggerMLG");
			GameObject mlg = (GameObject)Instantiate(mlgHuge, new Vector3(3, 71, 4), Quaternion.identity);
			mlg.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
		}
	}
}
