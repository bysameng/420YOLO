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
	
	void Start(){
		ScoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
	}
	

	
	// Update is called once per frame
	void Update () {
		
		bool foundHit = false;
		
		RaycastHit hit = new RaycastHit();
		
		float multiplier = GameObject.Find ("RotationCounterObject").GetComponent<RotationCounter>().damage;
		//float rotationcount = GameObject.Find ("RotationCounterObject").GetComponent<RotationCounter>().rotationPower;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (fireRate > 0)
			fireRate -= Time.deltaTime;
		if (fireRate < 0){
		if (Input.GetButton("Fire1")){
			GameObject.Find ("RotationCounterObject").GetComponent<RotationCounter>().rotationPower = 0f;
			fireRate = 2.0f;
			foundHit = Physics.Raycast(transform.position, transform.forward, out hit);
		}
		
		
		if (foundHit && !checkAllTags && hit.transform.tag != tagCheck)
			foundHit = false;
		if (foundHit && hit.transform.tag == tagCheck){
			//decal.position = hit.point;
			//decal.rotation = Quaternion.LookRotation(hit.normal);
			enemy = hit.collider.gameObject.GetComponent<EnemyIdentity>();
			if (enemy.ApplyDamage(power * multiplier))
					ScoreKeeper.addScore(enemy.worth, multiplier);
			hit.rigidbody.AddForceAtPosition(fwd * power * multiplier, hit.point);
			
			}
		}
			
	}
			
}
		
	

