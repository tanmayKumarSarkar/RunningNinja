using UnityEngine;
using System.Collections;

public class CamFollowNinja : MonoBehaviour {

	public GameObject ninjaObj;
	private float distanceToTarget;

	// Use this for initialization
	void Start () {
	
		distanceToTarget = transform.position.x - ninjaObj.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
	
		float ninjaObjX = ninjaObj.transform.position.x;
		Vector3 newCamPosition = transform.position;
		newCamPosition.x = ninjaObjX+distanceToTarget/4;
		transform.position = newCamPosition;
	}
}
