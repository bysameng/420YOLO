using UnityEngine;
using System.Collections;

public class RotationCounter : MonoBehaviour {
	
	private float rotationLast = 0f;
	private float rotationCurrent = 0f;
	private float rotationDelta = 0f;
	
	//true means clockwise
	private bool rotationDirection = true;
	private bool rotationDirectionLast = false;
	private float directionFrameCounter = 0.1f;
	
	private int jumpQuantity = 0;
	
	private bool powerAdd = true;
	
	private float timer = 1.0f;
	
	public float rotationPower = 0f;
	
	public float powerToGUI;
	
	public float damage;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		rotationCurrent = transform.localEulerAngles.y;
		
		if (Input.GetAxis("Mouse X") > 0)
			rotationDirection = true;
		else if (Input.GetAxis ("Mouse X") < 0)
			rotationDirection = false;
		
		if (rotationDirection == true)
			rotationDelta = Mathf.Abs((rotationCurrent - rotationLast + 360f) % 360);
		else if (rotationDirection == false)
			rotationDelta = Mathf.Abs((rotationLast - rotationCurrent + 360f) % 360);
		
		if (rotationDirection != rotationDirectionLast){
			powerAdd = false;
		}
		
		if (rotationDelta < 0.3f)
			timer -= Time.deltaTime;
		else timer = 1.0f;
		
		
		
		if (powerAdd)
			rotationPower += rotationDelta;
		
		if (!powerAdd){
			directionFrameCounter -= Time.deltaTime;
			timer -= Time.deltaTime;
		}
		
		if (directionFrameCounter <= 0){
			powerAdd = true;
			timer = 1.0f;
			rotationPower = rotationDelta;
			directionFrameCounter = 0.1f;
		}
		
		
		if (rotationPower < 0){
			//rotationPower = rotationDelta;
		}

		
		if (timer <= 0){
			timer = 1.0f;
			rotationPower = rotationDelta;
		}
		
		rotationLast = rotationCurrent;
		rotationDirectionLast = rotationDirection;
		
		powerToGUI = rotationPower;
	
		damage = calculatePower(rotationPower);
		
	
	}
	
	
	float calculatePower(float distance){
		float damage;
		if (distance <= 460)
			damage = ((distance+300.0f)*(distance+300.0f))/2000.0f;
		else{
			damage = Mathf.Log10 (((0.25f) * distance - 87.2f)) * 200.0f;
		}
		return damage;
	}
}
