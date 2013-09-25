using UnityEngine;
using System.Collections;

public class RotationCounter : MonoBehaviour {
	
	private float rotationLast = 0f;
	private float rotationCurrent = 0f;
	private float rotationDelta = 0f;
	private float rotSecond = 0.5f;
	private float rotationAgo;
	
	//true means clockwise
	private bool rotationDirection = true;
	private bool rotationDirectionLast = false;
	private float directionFrameCounter = 0.1f;
	private int jumpQuantity = 0;
	private bool powerAdd = true;
	private float timer = 1.0f;
	
	public float rotationPower = 0f;
	private float rotationPowerLast = 0f;
	public float rotationPowerShot;
	public float powerToGUI;
	public float damage;
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public float UpdateRotation (float rotation) {
		if (rotationPowerLast > 0) {
			rotSecond -= Time.deltaTime;
			if (rotSecond <= 0) {
				rotSecond = .3f;
				rotationPowerLast = 0f;
			}
		}
		
		rotationDelta = Mathf.Abs (rotation);
		
		if (Input.GetAxis ("Mouse X") > 0) {
			rotationDirection = true;
		} else if (Input.GetAxis ("Mouse X") < 0) {
			rotationDirection = false;
		}
		
		if (rotationDirection != rotationDirectionLast) {
			rotationPowerLast = rotationPower;
			rotationPower = 0f;
			powerAdd = false;
		}

		if (rotationDelta < 3.0f) {
			timer -= Time.deltaTime;
		} else
			timer = 1.0f;
		
		rotationPower += rotationDelta;
		
		if (rotationPower < 0) {
			rotationPower = rotationDelta;
		}

		
		if (timer <= 0) {
			timer = 1.0f;
			rotationPower = rotationDelta;
		}
		
		rotationLast = rotationCurrent;
		rotationDirectionLast = rotationDirection;
		
		powerToGUI = rotationPower;
		if (rotationPowerLast > rotationPower) {
			damage = calculatePower (rotationPowerLast);
			rotationPowerShot = rotationPowerLast;
		} 
		else {
			damage = calculatePower (rotationPower);
			rotationPowerShot = rotationPower;
		}
		
		return damage;

	}
	
	
	
	
	float calculatePower (float distance) {
		float damage;
		if (distance <= 460)
			damage = ((distance + 300.0f) * (distance + 300.0f)) / 2000.0f;
		else damage = Mathf.Log10 (((0.25f) * distance - 87.2f)) * 200.0f;
		
		return damage;
	}
}
