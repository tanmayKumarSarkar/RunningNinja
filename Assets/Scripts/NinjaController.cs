using UnityEngine;
using System.Collections;

public class NinjaController : MonoBehaviour {

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;
	float rx;
	float ry;
	float adjustedWidth;
	//private GUISkin skin;

	public float RunSpeed = 4.0f;
	public float JumpRunSpeed;
	private float TempRunSpeed;
	public float JumpHeight = 70.0f;
	public float ShurcnSpeed = 10.5f;
	public GameObject ShuricanePrefab;

	public Texture2D fireBtn;
	public Texture2D upBtn;
	public Texture2D fire2Btn;
	public Texture2D genBtn;

	public Texture2D pointIconTexture;

	private bool dblJump = true;
	public Animator anim; 
	private bool onGround;
	private bool IsJump;
	private bool IsJump2;
	private bool SwordSlash2;
	private bool Shuricane;
	private bool Ninja_Die;

	private bool isShuricaneThrow;
	private bool isSwordSlash;
	private bool isDieOnce;

	public int point = 0;

	public bool pauseEnabled;
	public bool otherMenuOpened;

	// Audios.....
	public AudioClip BtnSnd;
	public AudioSource FtStepSnd;
	public AudioClip EnmyDieSnd;
	public AudioClip GameOverSnd1;
	public AudioClip CoinClctSnd;
	public AudioClip SwordSnd;
	public AudioClip ShrcnSnd;

	string highScoreCheckKey;

	// Use this for initialization
	void Start () {

		//skin = Resources.Load("MenuBtnGUISkin") as GUISkin;
		onGround = true;
		IsJump = false;
		IsJump2 = false;
		SwordSlash2 = false;
		Shuricane = false;
		Ninja_Die = false;

		isShuricaneThrow = true;
		isSwordSlash = true;
		isDieOnce = true; 

		TempRunSpeed = RunSpeed;
		JumpRunSpeed = RunSpeed * 0.85f;

		pauseEnabled = false;
		otherMenuOpened = false;
		Time.timeScale = 1;

		highScoreCheckKey = "HighScore";
	}


	void OnGUI()
	{
		rx = Screen.width / nativeWidth;
		ry = Screen.height / nativeHeight;
		
		
		// Scale width the same as height - cut off edges to keep ratio the same
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ry, ry, 1));
		
		// Get width taking into account edges being cut off or extended
		adjustedWidth = nativeWidth * (rx / ry);
		float buttonWidth = adjustedWidth/7.5f;
		float buttonHeight = adjustedWidth/6.0f;
		// new GUI Style.......
		GUIStyle Btnstyle = GUI.skin.GetStyle ("Button");
		Btnstyle.fontSize = (int)adjustedWidth / 30;

		Btnstyle.normal.background = upBtn;
		Btnstyle.hover.background = upBtn;
		Btnstyle.active.background = upBtn;
		if (GUI.Button (
			new Rect ((1 * adjustedWidth / 400), (7 * nativeHeight / 10), buttonWidth, buttonHeight), "")) {
			Jump();
		}
		Btnstyle.normal.background = fireBtn;
		Btnstyle.hover.background = fireBtn;
		Btnstyle.active.background = fireBtn;
		if (GUI.Button (
			new Rect ((69 * adjustedWidth / 80), (7 * nativeHeight / 10), buttonWidth, buttonHeight), "")) {
			SwordSlash();
		}
		Btnstyle.normal.background = fire2Btn;
		Btnstyle.hover.background = fire2Btn;
		Btnstyle.active.background = fire2Btn;
		if (GUI.Button (
			new Rect ((69 * adjustedWidth / 80), (3 * nativeHeight / 10), buttonWidth, buttonHeight), "")) {
			ShuricaneThrow();
		}

		if(Ninja_Die == true && pauseEnabled == true){
			DisplayRestart2Btn();
		}

		DisplayPointsCount ();

		if (pauseEnabled && !otherMenuOpened){
			DisplayRestart3Btn ();
		}
	}

	
	// Update is called once per frame
	void Update() {

		if (Input.GetMouseButtonDown(0)) {
			//animation["Ninja_Sword_Slash1"].layer = 1;
			//animation.Play("Ninja_Sword_Slash1");
			//animation["Ninja_Sword_Slash1"].weight = 0.4f;
			SwordSlash();
		}

		anim.SetBool("Shuricane",Shuricane);
		if (Input.GetButtonDown ("Fire2")) {
			ShuricaneThrow();
		}

		// Menu && Back button....
		if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown (KeyCode.Menu)) && !otherMenuOpened){
			if (pauseEnabled == true){
				pauseEnabled = false;
				Time.timeScale = 1;
			}
			else if (pauseEnabled == false){
				pauseEnabled =true;
				Time.timeScale = 0;
			}
		}

		if (!onGround || Ninja_Die || pauseEnabled) {
			FtStepSnd.enabled = false;
		}
		if(onGround && !Ninja_Die && !pauseEnabled){
			FtStepSnd.enabled = true;
		}

	}

	void FixedUpdate () {

		Vector2 ninVelocity = rigidbody2D.velocity;
		ninVelocity.x = RunSpeed;
		rigidbody2D.velocity = ninVelocity;
	
		

		// jump
		if (Input.GetKeyDown (KeyCode.Space) == true) {
			Jump();		
		}

	}

	void Jump() {

		if (onGround == true) {
			rigidbody2D.AddForce(Vector3.up* JumpHeight,ForceMode2D.Impulse);
			//rigidbody2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
			IsJump = true;
			IsJump2 = false;
			onGround = false;
			RunSpeed = JumpRunSpeed;
		} 
		else if (!onGround && dblJump) {
			rigidbody2D.AddForce(Vector3.up* JumpHeight,ForceMode2D.Impulse);
			//rigidbody2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
			IsJump = false;
			IsJump2 = true;
			onGround = false;
			dblJump = false;
			RunSpeed = JumpRunSpeed;
		}
		//anim.SetFloat("VerticalMovement", verticalMovement);
		anim.SetBool("OnGround", onGround);
		anim.SetBool("IsJump",IsJump);
		anim.SetBool("IsJump2",IsJump2);

	}

	void SwordSlash(){

		SwordSlash2 = true;
		if (isSwordSlash) {
			isSwordSlash = false;	
			anim.SetBool("SwordSlash2",SwordSlash2);
			StartCoroutine (SwrdSlshIE());
		}
		SwordSlash2 = false;
	}
	IEnumerator SwrdSlshIE(){
		
		yield return new WaitForSeconds (0.15f);
		AudioSource.PlayClipAtPoint(SwordSnd, transform.position);
		yield return new WaitForSeconds (0.3f);
		isSwordSlash = true;
	}

	void ShuricaneThrow(){

		//GameObject.Find("weapons_sword_n").SetActive(false);
		//GameObject.Find("weapons_shuriken_n").SetActive (true);
		Shuricane = true;
		if (isShuricaneThrow) {
			isShuricaneThrow = false;
			StartCoroutine (ShuricaneThrowIE());
			anim.SetBool("Shuricane",Shuricane);		
		}
		Shuricane = false;
	}
	IEnumerator ShuricaneThrowIE(){

		yield return new WaitForSeconds (0.1f);
		AudioSource.PlayClipAtPoint(ShrcnSnd, transform.position);
		GameObject Shurcn = Instantiate (ShuricanePrefab) as GameObject;
		Shurcn.transform.position = GameObject.Find("ShuricanePoint").transform.position;
		Shurcn.rigidbody2D.velocity = new Vector2 (ShurcnSpeed, 0.0f);
		yield return new WaitForSeconds (0.30f);
		isShuricaneThrow = true;
	}


	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.name == "Floor"){
			onGround = true;
			dblJump = true;
			IsJump = false;
			IsJump2 =false;
			anim.SetBool("OnGround", onGround);
			anim.SetBool("IsJump",IsJump);
			anim.SetBool("IsJump2",IsJump2);
			RunSpeed = TempRunSpeed;
		}

		if (hit.gameObject.name =="enemy_Sword" || hit.gameObject.name =="Spike(Clone)"){
			Ninja_Die = true;
			if(isDieOnce){
				isDieOnce = false;
				anim.SetBool("Ninja_Die",Ninja_Die);
				AudioSource.PlayClipAtPoint(GameOverSnd1, transform.position);
				StartCoroutine(NinjaDie ());
			}
		}
	
	}

	void OnTriggerEnter2D(Collider2D hit) {
		//print(hit.gameObject.name+" "+ gameObject.name);
		if (hit.gameObject.name =="Spike(Clone)" || hit.gameObject.name == "Shuricane_E(Clone)" || hit.gameObject.name == "SBlade"){
			Ninja_Die = true;
			if(isDieOnce){
				isDieOnce = false;
				anim.SetBool("Ninja_Die",Ninja_Die);
				AudioSource.PlayClipAtPoint(GameOverSnd1, transform.position);
				StartCoroutine(NinjaDie ());
			}
		}
		if (hit.gameObject.CompareTag ("Coins")) {
			CollectCoin (hit);
		}
	}


	IEnumerator NinjaDie() {

		RunSpeed = 0;
		pauseEnabled = true;
		otherMenuOpened = true;
		yield return new WaitForSeconds (0.65f);
		Time.timeScale = 0;
		if (CheckForHighScore()){
			Application.LoadLevel("NewHighScore");
		}
	}

	void CollectCoin(Collider2D coinCollider) {
		AudioSource.PlayClipAtPoint(CoinClctSnd, transform.position);
		point = point + 5;		
		Destroy(coinCollider.gameObject);
	}


	void DisplayPointsCount() {
		Rect pointIconRect = new Rect((1*adjustedWidth/400),(1*nativeHeight/400),(adjustedWidth/7.5f),(adjustedWidth/9.0f));
		GUI.DrawTexture(pointIconRect, pointIconTexture);  

		// new GUI Style.......
		GUIStyle style = new GUIStyle();
		style.fontSize = (int)adjustedWidth / 50;
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = Color.red;
		
		Rect labelRectTxt = new Rect((pointIconRect.x+adjustedWidth /65), (pointIconRect.y+adjustedWidth/50),(adjustedWidth/9f),(adjustedWidth/20f));
		GUI.Label(labelRectTxt,"SCORE :", style);
		Rect labelRectPnt = new Rect((pointIconRect.x+adjustedWidth /45), (pointIconRect.y+adjustedWidth /20),(adjustedWidth/9f),(adjustedWidth/20f));
		GUI.Label(labelRectPnt, point.ToString(), style);
	}


	void DisplayRestart2Btn() {

		float buttonWidth = adjustedWidth/4.0f;
		float buttonHeight = adjustedWidth/11.0f;
		// new GUI Style.......
		GUIStyle Btnstyle = GUI.skin.GetStyle ("Button");
		Btnstyle.fontSize = (int)adjustedWidth / 32;

		Btnstyle.normal.textColor = Color.green;

		Btnstyle.normal.background = genBtn;
		Btnstyle.hover.background = genBtn;
		Btnstyle.active.background = genBtn;

		if (GUI.Button (
			new Rect ((3 * adjustedWidth / 16), (2 * nativeHeight / 5), buttonWidth, buttonHeight), "Restart")) {
			Application.LoadLevel("GameScenes");
		}
		if (GUI.Button (
			new Rect ((9 * adjustedWidth / 16), (2 * nativeHeight / 5), buttonWidth, buttonHeight), "Menu")) {
			Application.LoadLevel("GameStart");
		}
	}

	void DisplayRestart3Btn() {
		
		float buttonWidth = adjustedWidth/4.5f;
		float buttonHeight = adjustedWidth/11.5f;
		// new GUI Style.......
		GUIStyle Btnstyle = GUI.skin.GetStyle ("Button");
		Btnstyle.fontSize = (int)adjustedWidth / 32;
		
		Btnstyle.normal.textColor = Color.green;
		
		Btnstyle.normal.background = genBtn;
		Btnstyle.hover.background = genBtn;
		Btnstyle.active.background = genBtn;

		if (GUI.Button (
			new Rect ((2.0f * adjustedWidth / 16), (2 * nativeHeight / 5), buttonWidth, buttonHeight), "Resume")) {
			pauseEnabled = false;
			Time.timeScale = 1;
		}
		if (GUI.Button (
			new Rect ((5.8f * adjustedWidth / 16), (2 * nativeHeight / 5), buttonWidth, buttonHeight), "Restart")) {
			Application.LoadLevel (Application.loadedLevel);
		}
		if (GUI.Button (
			new Rect ((9.6f * adjustedWidth / 16), (2 * nativeHeight / 5), buttonWidth, buttonHeight), "Menu")) {
			Application.LoadLevel("GameStart");
		}
	}


	// Check For High Score......
	bool CheckForHighScore () {
		bool isHighScore = false;
		if(!PlayerPrefs.HasKey (highScoreCheckKey)){
			PlayerPrefs.SetInt(highScoreCheckKey,0);
		}
		else{
			if(point > PlayerPrefs.GetInt (highScoreCheckKey)){
				PlayerPrefs.SetInt(highScoreCheckKey,point);
				isHighScore = true;
			}
		}
		Debug.Log (PlayerPrefs.GetInt (highScoreCheckKey)+point);
		return isHighScore;
	}


}
