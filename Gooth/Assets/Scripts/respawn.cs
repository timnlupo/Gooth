using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.IO;
using System.Runtime.InteropServices;

public class respawn : MonoBehaviour {

	#if UNITY_IOS
		[DllImport("__Internal")]
		private static extern void sampleTextMethod (string message, string url);
	#endif

	public int timesrespawned;
	public Text score;
	public Text highscore;
	public Color themeYellow;
	public Color themeBlue;
	public GameObject scene;
	public static respawn Instance;

	void Awake() {
		updateTheme();
		#if UNITY_IOS
			Advertisement.Initialize ("40920");
		#endif
		#if UNITY_ANDROID
			Advertisement.Initialize("41204");
		#endif
		Instance = this;
	}

	void Start() {
		timesRespawned();
		score.text = "" + PlayerPrefs.GetInt("Score");
		highscore.text = "Best: " + PlayerPrefs.GetInt("High Score");
		#if UNITY_ANDROID
			PlayGamesPlatform.Activate();
			Social.localUser.Authenticate((bool success) => {});
		#endif
		scene.GetComponent<Animation>().Play("buttonAppear");
	}

	public void StartGame() {
	   	StartCoroutine(startScene("Main"));
	}

	public void StartShop() {
		StartCoroutine(startScene("Shop"));
	}

	public void StartShare() {
		shareText();
	}

	public void StartLeaderboard() {
		#if UNITY_IOS
			Social.ShowLeaderboardUI();
		#endif
		#if UNITY_ANDROID
			Social.ShowLeaderboardUI();
		#endif
	}

	public void StartCoin() {
		if(Advertisement.isReady()) { 
			Advertisement.Show(null, new ShowOptions { 
				pause = false, resultCallback = result => { 
					if ((result.ToString()) == ("Finished")) {
						adReward();
					}
				}
			});
		}
	}

	IEnumerator startScene(string name) {
		scene.GetComponent<Animation>().Play("fadeOut");
	   	yield return new WaitForSeconds(1);
	   	Application.LoadLevel(name);
	}

	void timesRespawned() {
		timesrespawned = PlayerPrefs.GetInt("Times Respawned");
		timesrespawned += 1;
		PlayerPrefs.SetInt ("Times Respawned", timesrespawned);
	}

	void adReward() {
		PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 50);
		Debug.Log(PlayerPrefs.GetInt("Coins"));
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

	public void shareText() {
		#if UNITY_IOS
			string body = ("I just got my highscore of " + PlayerPrefs.GetInt("High Score") + " on Gooth!  Can you beat me?");
			string url = "http://bit.ly/1Lv28s1";
			sampleTextMethod (body, url);
		#endif
		#if UNITY_ANDROID
			string subject = "Gooth for Android!";
			string body = "I just got my highscore of " + PlayerPrefs.GetInt("High Score") + " on Gooth!  Can you beat me? http://bit.ly/1Lv28s1";

			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			currentActivity.Call ("startActivity", intentObject);
		#endif
   }
}