using UnityEngine;
using System.Collections;

public class HighScoreScrpt : MonoBehaviour {

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;
	float rx;
	float ry;
	float adjustedWidth;
	private GUISkin skin;
	
	public Texture2D gameBg;
	
	string highScoreCheckKey;
	
	// Use this for initialization
	void Start () {
		
		highScoreCheckKey = "HighScore";
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("GameStart");
		}
	}
	
	
	void OnGUI() {
		rx = Screen.width / nativeWidth;
		ry = Screen.height / nativeHeight;
		
		
		// Scale width the same as height - cut off edges to keep ratio the same
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ry, ry, 1));
		
		// Get width taking into account edges being cut off or extended
		adjustedWidth = nativeWidth * (rx / ry);
		float buttonWidth = adjustedWidth/2.2f;
		float buttonHeight = adjustedWidth/14.0f;
		// new GUI Style.......
		GUIStyle Btnstyle = GUI.skin.GetStyle ("Button");
		Btnstyle.fontSize = (int)adjustedWidth /42;
		
		Btnstyle.fontStyle = FontStyle.BoldAndItalic;
		Btnstyle.normal.textColor = Color.red;
		Btnstyle.normal.background = gameBg;
		Btnstyle.hover.background = gameBg;
		Btnstyle.active.background = gameBg;
		
		if (GUI.Button (
			new Rect ((36 * adjustedWidth / 80), (7.5f * nativeHeight / 20), buttonWidth, buttonHeight), "!! HIGH SCORE !!")) {
			
		}
		if (GUI.Button (
			new Rect ((36 * adjustedWidth / 80), (10.8f * nativeHeight / 20), buttonWidth, buttonHeight), GetHighScore())) {
			
		}
		if (GUI.Button (
			new Rect ((36 * adjustedWidth / 80), (14.1f * nativeHeight / 20), buttonWidth, buttonHeight), "OK")) {
			Application.LoadLevel("GameStart");
		}
	}
	
	string GetHighScore () {
		
		string highScore = "0";
		if(!PlayerPrefs.HasKey (highScoreCheckKey)){
			PlayerPrefs.SetInt(highScoreCheckKey,0);
		}
		else{
			highScore = PlayerPrefs.GetInt (highScoreCheckKey).ToString ();
		}
		return highScore;
	}
}
