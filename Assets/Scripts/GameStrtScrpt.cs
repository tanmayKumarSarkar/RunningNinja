using UnityEngine;
using System.Collections;

public class GameStrtScrpt : MonoBehaviour {

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;
	float rx;
	float ry;
	float adjustedWidth;
	private GUISkin skin;

	public Texture2D gameBg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI() {
		rx = Screen.width / nativeWidth;
		ry = Screen.height / nativeHeight;
		
		
		// Scale width the same as height - cut off edges to keep ratio the same
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ry, ry, 1));
		
		// Get width taking into account edges being cut off or extended
		adjustedWidth = nativeWidth * (rx / ry);
		float buttonWidth = adjustedWidth/3.4f;
		float buttonHeight = adjustedWidth/15.9f;
		// new GUI Style.......
		GUIStyle Btnstyle = GUI.skin.GetStyle ("Button");
		Btnstyle.fontSize = (int)adjustedWidth /42;

		Btnstyle.fontStyle = FontStyle.BoldAndItalic;
		Btnstyle.normal.textColor = Color.red;
		Btnstyle.normal.background = gameBg;
		Btnstyle.hover.background = gameBg;
		Btnstyle.active.background = gameBg;

		if (GUI.Button (
			new Rect ((55 * adjustedWidth / 80), (4 * nativeHeight / 20), buttonWidth, buttonHeight), "START")) {
			Application.LoadLevel("GameScenes");
		}
		if (GUI.Button (
			new Rect ((55 * adjustedWidth / 80), (7.33f * nativeHeight / 20), buttonWidth, buttonHeight), "HIGH SCORE")) {
			Application.LoadLevel("HighScore");
		}
		if (GUI.Button (
			new Rect ((55 * adjustedWidth / 80), (10.66f * nativeHeight / 20), buttonWidth, buttonHeight), "HOW TO PLAY")) {
			Application.LoadLevel("HowToPlay");
		}
		if (GUI.Button (
			new Rect ((55 * adjustedWidth / 80), (14 * nativeHeight / 20), buttonWidth, buttonHeight), "EXIT")) {
			Application.Quit();
		}
	}


}
