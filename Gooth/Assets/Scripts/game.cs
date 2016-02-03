using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class game : MonoBehaviour {	

	public GameObject square;
	public GameObject objects;
	private int rotation = 0;
	public float thrust = 1f;
	public int score = 0;
	public int highscore;
	public int timesplayed;
	private int taps;
	public GameObject scoreboard;
	public GameObject highscoreboard;
	public Image sound;
	public Sprite soundOn;
	public Sprite soundOff;
	public Text startText;
	public AudioSource goodhit;
	public AudioSource badhit;
	public AudioSource nuetralhit;
	private bool isPlay;

	//Assign Colors
	public Color red;
	public Color blue;
	public Color green;
	public Color yellow;
	public Color themeYellow;
	public Color themeBlue;

	void Awake() { updateTheme(); }

	void Start(){
		scoreSetter(0);
		highscoreUpdate();
		timesPlayed ();
		taps = 0;
		isPlay = true;

		#if UNITY_ANDROID
			PlayGamesPlatform.Activate();
			Social.localUser.Authenticate((bool success) => {});
		#endif
	}

	void Update() {
		rotate(15f);
	}

	//code for randomly switching ball color
	void color() {
		switch (Random.Range(0, 4)) {
		case 3:
			GetComponent<SpriteRenderer> ().color = red;
			break;
		case 2:
			GetComponent<SpriteRenderer> ().color = blue;
			break;
		case 1:
			GetComponent<SpriteRenderer> ().color = green;
			break;
		case 0:
			GetComponent<SpriteRenderer> ().color = yellow;
			break;
		}
	}
		
	//code for ball launch
	IEnumerator launch() {
		color ();
		yield return new WaitForSeconds (1f);
		switch (Random.Range(0, 2)) {
			case 1:
				if (GetComponent<SpriteRenderer> ().color == red) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2(-1, Random.Range(.35f, .3f)));
					break;
				}
				else if (GetComponent<SpriteRenderer> ().color == blue) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2(Random.Range(.35f, .3f), -1));
					break;
				}
				else if (GetComponent<SpriteRenderer> ().color == green) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2(1, Random.Range(.35f, .3f)));
					break;
				}
				else if (GetComponent<SpriteRenderer> ().color == yellow) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2(Random.Range(.35f, .3f), 1));
					break;
				}
				else {
					Debug.Log ("error");
					break;
				}
			case 0:
				if (GetComponent<SpriteRenderer> ().color == red) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2(-1, Random.Range(-.35f, -.3f)));
					break;
				}
				else if (GetComponent<SpriteRenderer> ().color == blue) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2(Random.Range(-.35f, -.3f), -1));
					break;
				}
				else if (GetComponent<SpriteRenderer> ().color == green) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2(1, Random.Range(-.35f, -.3f)));
					break;
				}
				else if (GetComponent<SpriteRenderer> ().color == yellow) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2(Random.Range(-.35f, -.3f), 1));
					break;
				}
				else {
					Debug.Log ("error");
					break;
				}
		}
	}

	//code for square rotation
	void rotate(float rotateSpeed) {
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began && (isPlay == true)) {
			if (taps > 0){
				switch (rotation) {
					case 3:
						square.GetComponent<Animation>().Play("squareRotation4");
						rotation = 0;
						break;
					case 2:
						square.GetComponent<Animation>().Play("squareRotation3");
						rotation += 1;
						break;
					case 1:
						square.GetComponent<Animation>().Play("squareRotation2");
						rotation += 1;
						break;
					case 0:
						square.GetComponent<Animation>().Play("squareRotation1");
						rotation += 1;
						break;
				}
				Debug.Log(rotation);
			}
			tapStart();
		}
	}

	//Code to start game on first tap
	void tapStart() {
		if (taps == 0){
			StartCoroutine(launch());
			startText.text = " ";
		}
		taps += 1;
	}

	//save highscore
	void highScoreSetter (int score){
		highscore = PlayerPrefs.GetInt("High Score");
		if (score > highscore) {
			highscore = score;
			PlayerPrefs.SetInt ("High Score", highscore);
			reportScore();
		}
	}

	void reportScore () {
			long longhighscore = (long)(PlayerPrefs.GetInt("High Score"));
			Debug.Log("Longhighscore = " + longhighscore);
			#if UNITY_IOS
				Social.ReportScore(longhighscore, "com.timlupo.gooth", success => {
					Debug.Log(success ? "Reported score successfully" : "Failed to report score");
				});
			#endif
			#if UNITY_ANDROID
				Social.ReportScore((PlayerPrefs.GetInt("High Score")), "CgkInsufqvYBEAIQAQ", (bool success) => {});
			#endif
	}

	//save times played
	void timesPlayed() {
		timesplayed = PlayerPrefs.GetInt("Times Played");
		timesplayed += 1;
		PlayerPrefs.SetInt ("Times Played", timesplayed);
	}

	//update highscore
	void highscoreUpdate() {
		highscore = PlayerPrefs.GetInt("High Score");
		highscoreboard.GetComponent<Text>().text = "" + highscore;
		//highscoreboard.GetComponent<Animator>().Play("scoreCounter", -1, 0);
	}
	
	//set last score
	void scoreSetter(int score) {
		PlayerPrefs.SetInt("Score", score);
	}

	//allows for theme change
	void updateTheme() {
		GameObject[] DBgameobjs = GameObject.FindGameObjectsWithTag("defaultBlue");
		GameObject[] DYgameobjs = GameObject.FindGameObjectsWithTag("defaultYellow");
		if (PlayerPrefs.GetString ("Theme") == ("DefaultYellow")){
			foreach (GameObject DBgameobj in DBgameobjs) {
            	if (DBgameobj.GetComponent<SpriteRenderer>() != null)
            		DBgameobj.GetComponent<SpriteRenderer>().color = themeBlue;
            	else if (DBgameobj.GetComponent<Text>() != null)
            		DBgameobj.GetComponent<Text>().color = themeBlue;
            	else if (DBgameobj.GetComponent<TextMesh>() != null)
            		DBgameobj.GetComponent<TextMesh>().color = themeBlue;
            	else if (DBgameobj.GetComponent<Image>() != null)
            		DBgameobj.GetComponent<Image>().color = themeBlue;
        	}
        	foreach (GameObject DYgameobj in DYgameobjs) {
            	if (DYgameobj.GetComponent<SpriteRenderer>() != null)
            		DYgameobj.GetComponent<SpriteRenderer>().color = themeYellow;
            	else if (DYgameobj.GetComponent<Text>() != null)
            		DYgameobj.GetComponent<Text>().color = themeYellow;
            	else if (DYgameobj.GetComponent<TextMesh>() != null)
            		DYgameobj.GetComponent<TextMesh>().color = themeYellow;
            	else if (DYgameobj.GetComponent<Image>() != null)
            		DYgameobj.GetComponent<Image>().color = themeYellow;
        	}
		}
		else if (PlayerPrefs.GetString ("Theme") == ("DefaultBlue")){
			foreach (GameObject DBgameobj in DBgameobjs) {
            	if (DBgameobj.GetComponent<SpriteRenderer>() != null)
            		DBgameobj.GetComponent<SpriteRenderer>().color = themeYellow;
            	else if (DBgameobj.GetComponent<Text>() != null)
            		DBgameobj.GetComponent<Text>().color = themeYellow;
            	else if (DBgameobj.GetComponent<TextMesh>() != null)
            		DBgameobj.GetComponent<TextMesh>().color = themeYellow;
            	else if (DBgameobj.GetComponent<Image>() != null)
            		DBgameobj.GetComponent<Image>().color = themeYellow;
        	}
        	foreach (GameObject DYgameobj in DYgameobjs) {
            	if (DYgameobj.GetComponent<SpriteRenderer>() != null)
            		DYgameobj.GetComponent<SpriteRenderer>().color = themeBlue;
            	else if (DYgameobj.GetComponent<Text>() != null)
            		DYgameobj.GetComponent<Text>().color = themeBlue;
            	else if (DYgameobj.GetComponent<TextMesh>() != null)
            		DYgameobj.GetComponent<TextMesh>().color = themeBlue;
            	else if (DYgameobj.GetComponent<Image>() != null)
            		DYgameobj.GetComponent<Image>().color = themeBlue;
        	}
		}
	}

	//logic for what happens on side hit
	IEnumerator OnTriggerEnter2D(Collider2D other){
		if (GetComponent<SpriteRenderer> ().color == other.GetComponent<SpriteRenderer> ().color){
			score += 1;
			if (PlayerPrefs.GetInt ("Audio") == 1) {
				goodhit.Play();
			}
			scoreboard.GetComponent<TextMesh>().text = "" + score;
			scoreboard.GetComponent<Animator>().Play("scoreCounter", -1, 0);
			highscoreUpdate();
			highScoreSetter(score);
			scoreSetter(score);
			yield return new WaitForSeconds(.01f);
			color ();
		}
		else if (other.tag == "corner") {
			if (PlayerPrefs.GetInt ("Audio") == 1) {
				nuetralhit.Play();
			}
		}
		else {
			highScoreSetter(score);
			if (PlayerPrefs.GetInt ("Audio") == 1) {
				badhit.Play();
			}
			isPlay = false;
			square.GetComponent<Animation>().Play("squareToRespawn");
			objects.GetComponent<Animation>().Play("objectExit");
			yield return new WaitForSeconds(1f);
			Application.LoadLevel ("Respawn");
		}
	}
}