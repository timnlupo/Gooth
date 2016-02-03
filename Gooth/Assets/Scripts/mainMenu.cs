/*using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class mainMenu : MonoBehaviour {

	private GameCenter gameCenter = null;

	void Awake() {
		#if UNITY_IOS
			Advertisement.Initialize ("40920");
		#endif
		#if UNITY_ANDROID
			Advertisement.Initialize("41204");
		#endif
	}

	void Start() {
		#if UNITY_IOS
			gameCenter = new GameCenter(); 
			gameCenter.Initialize();
		#endif
		#if UNITY_ANDROID
			PlayGamesPlatform.Activate();
			Social.localUser.Authenticate((bool success) => {});
		#endif
	}

	public void StartGame() {
	   	Application.LoadLevel("Game");
	}

	public void StartShop() {
		Application.LoadLevel("Shop");
	}

	public void StartShare() {
		shareText();
	}

	public void startLeaderboard() {
		#if UNITY_IOS
			gameCenter.ShowLeaderboardUI();
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

	void adReward() {
		PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 50);
		Debug.Log(PlayerPrefs.GetInt("Coins"));
	}

	public void shareText(){
		#if UNITY_IOS
			// [DllImport("__Internal")]
			// private static extern void sampleTextMethod (string message);

			// sampleTextMethod ("I Just Share Textual Info");
		#endif
		#if UNITY_ANDROID
			string subject = "Gooth for Android!";
			string body = "I just got my highscore of " + PlayerPrefs.GetInt("High Score") + " on Gooth!  Can you beat me? https://play.google.com/store/apps/details?id=com.timlupo.gooth&hl=en";

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
}*/