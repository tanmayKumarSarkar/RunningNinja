using UnityEngine;
using System.Collections;

public class ShuricneScrpt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Destroy (gameObject,2.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate(0,0,10);
	}

	void OnCollisionEnter2D (Collision2D hitObj){

		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D Coll) {

		Destroy (gameObject);
		//Debug.Log (Coll.gameObject.name);
	}
}
