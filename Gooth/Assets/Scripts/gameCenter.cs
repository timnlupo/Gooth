using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class gameCenter : MonoBehaviour {
    
    void Start () {
        // Authenticate and register a ProcessAuthentication callback
        // This call needs to be made before we can proceed to other calls in the Social API
        Social.localUser.Authenticate (ProcessAuthentication);
    }

    // This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    void ProcessAuthentication (bool success) {
        if (success) {
            Debug.Log ("Authenticated, checking score");

	        ILeaderboard leaderboard = Social.CreateLeaderboard();
			leaderboard.id = "com.timlupo.gooth";
			leaderboard.LoadScores(result => {
				Debug.Log("Received " + leaderboard.scores.Length + " scores");
				foreach (IScore score in leaderboard.scores)
					Debug.Log(score);
			});
        }
        else
            Debug.Log ("Failed to authenticate");
    }
}