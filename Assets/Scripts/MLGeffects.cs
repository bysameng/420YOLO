using UnityEngine;
using System.Collections;

public class MLGeffects : MonoBehaviour {
	
	public int mlgEffect;
	public bool displayingMLG;
	
	public Texture2D MLG1;
	public Texture2D MLG2;
	public GameObject light1;
	public GameObject SuperParticle;
	public GameObject ttext1;
	public AudioClip dubstep;
	
	private float effectTimer;
	
	private bool playingAudio = false;
	private bool shakingScreen = false;
	private float shakeStrength;
	private bool timeSlowed = false;
	
	private string[] wordsArray = {"BANG", "SWAG", "YOLO", "MLG", "BLAZIT", 
					"BlAzEiT", "sWaG", "y0lO", "FGT", "1v1 ME SCRUB",
					"LOL", "LMFAO", "B00M", "1337", "Sw3G",
					"MGL", "WEED", "BONG", "B0NG", "SW4g",
					"FUCK YOU", "Bl4zE1t", "lolfgt", "SWAG", "YOLOLOL"};
	
	private string[] wordsArray360 = {"360BLAZITFGT", "LOL360", "tHrEeSiXtY", "<360>", "360sWaG",
					"~x360x~", "360NOSCOPE", "lol 360", "360YOLO", "360"};
	
	private const int MESSAGEARRAYSIZE = 25;
	private const int MESSAGE360ARRAYSIZE = 10;
	
	private vp_FPCamera playerCamera;
	
	
	
	// Use this for initialization
	void Start () {
		effectTimer = 5.0f;
		mlgEffect = 0;
		playerCamera = GameObject.Find ("FPSCamera").GetComponent<vp_FPCamera> ();
	}
	
	
	
	void OnGUI () {
	}
	
	
	
	void Update () {
		if (shakingScreen)
			playerCamera.ShakeSpeed = shakeStrength;
		else 
			playerCamera.ShakeSpeed = 0;
		
		//test
		if (Input.GetKeyDown (KeyCode.K) && mlgEffect != 3)
			mlgEffect = 3;
		if (mlgEffect == 3) {
			ShakeScreen (5f, .5f);
			mlgEffect = 0;
		}
	}
	
	
	
	public void DisplayLightShow (float duration) {
		StartCoroutine (LightShow (0.05f, duration));
	}
	
	
	
	public void PlayDubstep (float duration) {
		if (!playingAudio) {
			StartCoroutine (DubstepPlayer (duration));
			playingAudio = true;
		}	
	}
	
	
	
	public void ShakeScreen (float strength, float duration) {
		if (!shakingScreen) {
			shakingScreen = true;
			shakeStrength = strength;
			StartCoroutine (ScreenShaker (duration));	
		}
	}
	
	
	
	public void ParticleShow (Vector3 location, float duration) {
		StartCoroutine (ParticleShower (location, duration));
	}
	
	
	
	public void DisplayWords (string words, Vector3 location, float duration) {
		StartCoroutine (WordDisplayer (words, location, duration));
	}
	
	
	
	public void DisplayWords (float rotation, Vector3 location, float duration) {
		int rand;
		string message;
		if (rotation > 330 && rotation < 390)
			message = wordsArray360 [Random.Range (0, MESSAGE360ARRAYSIZE)];
		else
			message = wordsArray [Random.Range (0, MESSAGEARRAYSIZE)];
		DisplayWords (message, location, duration);
	}
	
	
	
	public void SlowTime (float strength, float duration) {
		if (!timeSlowed) {
			Time.timeScale = strength;
		}
	}
	
	
	
	IEnumerator WordDisplayer (string words, Vector3 location, float duration) {
		GameObject textObject = (GameObject)Instantiate (ttext1, location, playerCamera.transform.rotation);
		textObject.transform.parent = transform;
		textObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 5;
		textObject.GetComponent<TextMesh> ().text = words;
		yield return new WaitForSeconds(duration);
		Destroy (textObject);
	}
	
	
	
	IEnumerator ParticleShower (Vector3 location, float duration) {
		GameObject particles = (GameObject)Instantiate (SuperParticle, location, Quaternion.identity);
		yield return new WaitForSeconds(duration);
		Destroy (particles);
	}
	
	
	
	IEnumerator ScreenShaker (float duration) {
		yield return new WaitForSeconds(duration);
		shakingScreen = false;
	}
	
	
	
	IEnumerator DubstepPlayer (float duration) {
		audio.clip = dubstep;
		audio.Play ();
		yield return new WaitForSeconds(duration);
		audio.Stop ();
		playingAudio = false;
	}
	
	
	
	IEnumerator LightShow (float freq, float seconds) {
		GameObject lightGameObject = new GameObject ("The Light");
		lightGameObject.AddComponent<Light> ();
		lightGameObject.transform.position = transform.position;
		lightGameObject.GetComponent<Light> ().intensity = 1;
		lightGameObject.GetComponent<Light> ().range = 20;
		for (int i = 0; i < seconds*100; i++) {
			lightGameObject.GetComponent<Light> ().color = new Color (Mathf.Sin (freq * i + 0) * 127.0f + 20, Mathf.Sin (freq * i + 2.0f) * 127 + 20, Mathf.Sin (freq * i + 4) * 127 + 20);
			lightGameObject.transform.parent = transform;
			yield return new WaitForSeconds(.01f);
		}
		Destroy (lightGameObject);
	}
		
	
}
