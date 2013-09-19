using UnityEngine;
using System.Collections;

public class Shoost : MonoBehaviour {
	
	public Transform decal;
	public float raycastDistance = 100.0f;
	
	public string tagCheck = "NPC";
	public bool checkAllTags = false;
	
	private float fireRate = 1.0f;
	
	public int power = 2;
	
	public AudioClip fireSound;
	
	private MLGeffects MLGEffects;
	
	void Start(){
		MLGEffects = GameObject.Find("MLG Effects").GetComponent<MLGeffects>();
	}
	

	
	// Update is called once per frame
	void Update () {
		
		bool foundHit = false;
		
		RaycastHit hit = new RaycastHit();
		
		float multiplier =  GameObject.Find ("Player").GetComponent<RotationCounter>().damage;
		
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (fireRate > 0)
			fireRate -= Time.deltaTime;
		if (fireRate < 0){
		if (Input.GetButton("Fire1")){
			print ("Fire!");
			fireRate = 2.0f;
			audio.PlayOneShot(fireSound);
			foundHit = Physics.Raycast(transform.position, transform.forward, out hit);
		}
		
		
		if (foundHit && !checkAllTags && hit.transform.tag != tagCheck)
			foundHit = false;
		if (foundHit){
			MLGEffects.mlgEffect = 1;
			print (multiplier);
			//decal.position = hit.point;
			//decal.rotation = Quaternion.LookRotation(hit.normal);
			hit.rigidbody.AddForceAtPosition(fwd * power * multiplier, hit.point);
			
		}
			
		}
		
	}
}
