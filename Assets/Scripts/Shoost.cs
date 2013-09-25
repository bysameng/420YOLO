using UnityEngine;
using System.Collections;

public class Shoost : MonoBehaviour {
	
	public float raycastDistance = 100.0f;
	public string tagCheck = "NPC";
	public bool checkAllTags = false;
	private float fireRate = 1.0f;
	public int power = 7;
	
	private ScoreKeeper ScoreKeeper;
	private EnemyIdentity enemy;
	
	public float lastShootPower;
	public float lastShootRotation;
	private MLGeffects effects;
	
	void Start () {
		ScoreKeeper = GameObject.Find ("ScoreKeeper").GetComponent<ScoreKeeper> ();
		effects = GameObject.Find ("MLGeffectsObject").GetComponent<MLGeffects> ();
	}
	


	
	// Update is called once per frame
	void Update () {
		
		bool foundHit = false;
		
		RaycastHit hit = new RaycastHit ();
		
		float multiplier = GameObject.Find ("RotationCounterObject").GetComponent<RotationCounter> ().damage;
		//float rotationcount = GameObject.Find ("RotationCounterObject").GetComponent<RotationCounter>().rotationPower;
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		if (fireRate > 0)
			fireRate -= Time.deltaTime;
		if (fireRate < 0) {
			if (Input.GetButton ("Fire1")) {
				lastShootPower = power * multiplier;
				lastShootRotation = GameObject.Find ("RotationCounterObject").GetComponent<RotationCounter> ().rotationPowerShot;

				GameObject.Find ("RotationCounterObject").GetComponent<RotationCounter> ().rotationPower = 0f;
				fireRate = 2.0f;
			
				foundHit = Physics.Raycast (transform.position, transform.forward, out hit);

				effects.DisplayWords (lastShootRotation, transform.position, .05f);
			}
		
		
			if (foundHit && !checkAllTags && hit.transform.tag != tagCheck)
				foundHit = false;
			if (foundHit && hit.transform.tag == tagCheck) {
				enemy = hit.collider.gameObject.GetComponent<EnemyIdentity> ();
			
				if (enemy.ApplyDamage (lastShootPower)) 
				if (ScoreKeeper.addScore (enemy.worth, lastShootPower, lastShootRotation) == 1) {
					hit.rigidbody.AddForceAtPosition (fwd * lastShootPower, hit.point);
					effects.ParticleShow (hit.point, 10.0f);
				}
			}
		}
			
	}
			
}
		
	

