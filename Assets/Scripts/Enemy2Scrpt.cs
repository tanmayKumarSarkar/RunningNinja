using UnityEngine;
using System.Collections;

public class Enemy2Scrpt : MonoBehaviour {

	private bool Enemy_Attack;
	private bool Enemy2_Die;
	public Animator Enmanim;
	public float Nin_Enm1_Distance;
	public GameObject ShuricanePrefab;
	public float ShurcnSpeed;

	private bool isWait;
	private bool pointOnce;

	// Audio Clip..
	public AudioClip EnmyDieSnd;
	public AudioClip ShrcnSnd;

	// Other Objects Component
	NinjaController NinjaControllerScriptE2S;

	// Use this for initialization
	void Start () {
	
		Enemy_Attack = false;
		Enemy2_Die = false;
		isWait = false;

		pointOnce = false;
		NinjaControllerScriptE2S = GameObject.Find ("Ninja").GetComponent<NinjaController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		FindEnemyToAttack ();
	}

	void FindEnemyToAttack() {

		float NinX = GameObject.Find ("Ninja").transform.position.x;
		float EnmX = transform.position.x;
		//Debug.Log (EnmX - NinX +" < "+ Nin_Enm1_Distance);
		if (EnmX - NinX < Nin_Enm1_Distance) {
			Enemy_Attack = true;
			Enmanim.SetBool ("Enemy2_Attack", Enemy_Attack);	
			if (!isWait && !Enemy2_Die){
				ShuricaneThrow ();
			}
		} else {
			Enemy_Attack = false;
			Enmanim.SetBool ("Enemy2_Attack", Enemy_Attack);		
		}
	}

	void OnTriggerEnter2D(Collider2D Coll) {
		//print(Coll.gameObject.name);
		if ((Coll.gameObject.name == "weapons_sword_n") ||(Coll.gameObject.name == "weapons_shuriken_n") || (Coll.gameObject.name =="Shuricane(Clone)")) {
			
			Enemy2_Die = true;
			Enmanim.SetBool ("Enemy2_Die",Enemy2_Die);

			if( pointOnce == false){
				AudioSource.PlayClipAtPoint(EnmyDieSnd, transform.position);
				NinjaControllerScriptE2S.point = NinjaControllerScriptE2S.point + 10;
				pointOnce = true;
			}
		}
	}

	void ShuricaneThrow () {

		isWait = true;
		AudioSource.PlayClipAtPoint(ShrcnSnd, transform.position);
		GameObject Shurcn = Instantiate (ShuricanePrefab) as GameObject;
		Shurcn.transform.position = GameObject.Find("ShrcnThrwPoint").transform.position;
		Shurcn.rigidbody2D.velocity = new Vector2 (ShurcnSpeed, 0.0f);
		StartCoroutine (WaitThrowIE ());
	}

	IEnumerator WaitThrowIE(){

		yield return new WaitForSeconds (0.7f);
		isWait = false;
	}


}
