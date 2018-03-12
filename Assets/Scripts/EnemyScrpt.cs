using UnityEngine;
using System.Collections;

public class EnemyScrpt : MonoBehaviour {

	private bool Enemy_Attack;
	private bool Enemy_Die;
	public Animator Enmanim;
	public float Nin_Enm1_Distance;

	// Audio Clip..
	public AudioClip EnmyDieSnd;
	public AudioClip SwordSnd;
	private bool isSndWait;

	private bool pointOnce;
	// Other Objects Component
	NinjaController NinjaControllerScriptES;

	// Use this for initialization
	void Start () {
	
		Enemy_Attack = false;
		Enemy_Die = false;
		GameObject.Find("enemy_Sword").SetActive (true);
		pointOnce = false;

		isSndWait = false;

		NinjaControllerScriptES = GameObject.Find ("Ninja").GetComponent<NinjaController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		FindEnemyToAttack ();
	}

	void FindEnemyToAttack() {

		float NinX = GameObject.Find ("Ninja").transform.position.x;
		float EnmX = transform.position.x;
		//Debug.Log (NinX+" "+EnmX);
		if (EnmX - NinX < Nin_Enm1_Distance) {
			Enemy_Attack = true;
			Enmanim.SetBool ("Enemy_Attack", Enemy_Attack);	
		} else {
			Enemy_Attack = false;
			if(!isSndWait){
				isSndWait = true;
				//AudioSource.PlayClipAtPoint(SwordSnd, transform.position);	
			}
			Enmanim.SetBool ("Enemy_Attack", Enemy_Attack);		
		}
	}

	IEnumerator WaitSndIE(){
		
		yield return new WaitForSeconds (1.0f);
		isSndWait = false;
	}

	void OnTriggerEnter2D(Collider2D Coll) {
		//print(Coll.gameObject.name);

		if ((Coll.gameObject.name == "weapons_sword_n") ||(Coll.gameObject.name == "weapons_shuriken_n") || (Coll.gameObject.name =="Shuricane(Clone)")) {
			
			Enemy_Die = true;
			Enmanim.SetBool ("Enemy_Die",Enemy_Die);

			if( pointOnce == false){
				AudioSource.PlayClipAtPoint(EnmyDieSnd, transform.position);
				NinjaControllerScriptES.point = NinjaControllerScriptES.point + 10;
				pointOnce = true;
			}

			if(GameObject.Find("enemy_Sword")){
				//GameObject.Find("enemy_Sword").SetActive (false);
			}
		}
	}
	
}
