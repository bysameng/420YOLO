using UnityEngine;
using System.Collections;

public class MLGeffects : MonoBehaviour {
	
	public int mlgEffect;
	
	public bool displayingMLG;
	
	public Texture2D MLG1;
	public Texture2D MLG2;
	
	public GameObject light1;
	
	private float effectTimer;
	
	public AudioClip dubstep;
	private bool playingAudio = false;
	
	private bool shakingScreen = false;
	private float shakeStrength;

	// Use this for initialization
	void Start () {
		effectTimer = 5.0f;
		mlgEffect = 0;
	}
	
	void OnGUI () {
	}
	
	
	
	void Update(){
		
		if (shakingScreen)
			GameObject.Find ("FPSCamera").GetComponent<vp_FPCamera>().ShakeSpeed = shakeStrength;
		
		
		if (effectTimer < 0){
			mlgEffect = 0;
			effectTimer = 0.1f;
		}
		
		//lightshow
		if (Input.GetKeyDown(KeyCode.K) && mlgEffect != 3){
			mlgEffect = 3;
		}
		if (mlgEffect == 3){
			ShakeScreen(5f, 5.0f);
			mlgEffect = 0;
		}
	}
	
	public void DisplayLightShow(float duration){
		StartCoroutine(LightShow(0.05f, duration));
	}

	public void PlayDubstep(float duration){
		if (!playingAudio){
			StartCoroutine(DubstepPlayer (duration));
			playingAudio = true;
		}	
	}
	
	public void ShakeScreen(float strength, float duration){
		if (!shakingScreen){
			shakeStrength = strength;
			StartCoroutine(ScreenShaker(strength, duration));
			shakingScreen = true;
		}
	}
	
	IEnumerator ScreenShaker(float strength, float duration){
		yield return new WaitForSeconds(duration);
		shakingScreen = false;
	}
	
	

	
	
	
	IEnumerator DubstepPlayer(float duration){
			audio.clip = dubstep;
			audio.Play();
			yield return new WaitForSeconds(duration);
			audio.Stop ();
			playingAudio = false;
	}
			
	
	IEnumerator LightShow(float freq, float seconds){
		GameObject lightGameObject = new GameObject("The Light");
        lightGameObject.AddComponent<Light>();
        lightGameObject.transform.position = transform.position;
		lightGameObject.GetComponent<Light>().intensity = 1;
		lightGameObject.GetComponent<Light>().range = 20;
		for (int i = 0; i < seconds*100; i++){
			//lightGameObject.GetComponent<Light>().color = new Color(Mathf.Sin(freq*i + 0) * 127.0f + 128.0f, Mathf.Sin(freq*i + 2.0f) * 127 + 128, Mathf.Sin(freq*i + 4) * 127 + 128);
			//lightGameObject.GetComponent<Light>().color = new Color(Mathf.Sin(freq*i + 0) * 127 + 128, 0, 0);
			lightGameObject.GetComponent<Light>().color = new Color(Mathf.Sin(freq*i + 0) * 127.0f+20, Mathf.Sin(freq*i + 2.0f) * 127+20, Mathf.Sin(freq*i + 4) * 127+20);
			lightGameObject.transform.parent = transform;
			yield return new WaitForSeconds(.01f);
		}
		Destroy(lightGameObject);
	}
		
		
		
}
