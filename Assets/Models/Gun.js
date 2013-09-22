var bullet : GameObject;
var fireDelay : float = 0.1;
var exitPoint : Vector3;
var recoil : float = 100.0;
var sound : AudioClip;

private var canFire : boolean = true;


function Update () {
	if(Input.GetMouseButton(0))
	FireOneShot();
}

function FireOneShot() {
	if(!canFire)
	return;
	
	canFire = false;
	
	audio.PlayOneShot(sound);
	transform.parent.rigidbody.AddTorque(Random.insideUnitSphere * recoil);
	yield WaitForSeconds(0.1);//allows time for the recoil to move the barrel, otherwise the first shot would always be completely accurate
	Destroy(Instantiate(bullet, transform.position + transform.TransformDirection(exitPoint), transform.rotation), 10.0);
	
	yield WaitForSeconds(fireDelay);
	canFire = true;
}