using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class menu : MonoBehaviour {	

	public Image sound;
	public Sprite soundOn;
	public Sprite soundOff;
	public Color themeYellow;
	public Color themeBlue;
	public AudioSource theme;
	static bool AudioBegin = false;
	public GameObject scene;

	void Awake() { 
		updateTheme();
		if (PlayerPrefs.GetInt ("Audio") == 1){
			sound.sprite = soundOn;
		}
		else if (PlayerPrefs.GetInt ("Audio") == 0){
			sound.sprite = soundOff;
		}
	}

	void Start() {
		#if UNITY_ANDROID
			PlayGamesPlatform.Activate();
			Social.localUser.Authenticate((bool success) => {});
		#endif
		scene.GetComponent<Animation>().Play("fadeIn");
	}

	public void startGame() {
		Application.LoadLevel("Game");
	}

	public void startAudio() {
		if (PlayerPrefs.GetInt ("Audio") == 1){
			PlayerPrefs.SetInt ("Audio", 0);
			sound.sprite = soundOff;
		}
		else if (PlayerPrefs.GetInt ("Audio") == 0){
			PlayerPrefs.SetInt ("Audio", 1);
			sound.sprite = soundOn;
		}
		else {
			PlayerPrefs.SetInt ("Audio", 1);
			sound.sprite = soundOn;
		}
		Debug.Log(PlayerPrefs.GetInt ("Audio"));
	}

	public void startTheme() {
		if (PlayerPrefs.GetString ("Theme") == ("DefaultBlue")){
			PlayerPrefs.SetString ("Theme", "DefaultYellow");
		}
		else if (PlayerPrefs.GetString ("Theme") == ("DefaultYellow")){
			PlayerPrefs.SetString ("Theme", "DefaultBlue");
		}
		else {
			PlayerPrefs.SetString ("Theme", "DefaultYellow");
		}
		updateTheme();
	}

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
}