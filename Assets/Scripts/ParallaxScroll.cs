using UnityEngine;
using System.Collections;

public class ParallaxScroll : MonoBehaviour {

	public Renderer background;
	public Renderer foreground;
	
	public float backgroundSpeed = 0.02f;
	public float foregroundSpeed = 0.06f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float backgroundOffset = Time.timeSinceLevelLoad * backgroundSpeed;
		float foregroundOffset = Time.timeSinceLevelLoad * foregroundSpeed;
		
		background.material.mainTextureOffset = new Vector2(backgroundOffset, 0);
		foreground.material.mainTextureOffset = new Vector2(foregroundOffset, 0);
	}
}
