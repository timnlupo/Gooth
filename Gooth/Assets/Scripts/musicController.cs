using UnityEngine;
using System.Collections;

public class musicController : MonoBehaviour {

	public AudioSource music;

	void Awake() {
		GameObject[] gameMusic = GameObject.FindGameObjectsWithTag("themeMusic");
		if (gameMusic.Length > 1)
			Destroy(gameMusic[1]);
		DontDestroyOnLoad(gameObject);
	}

	void Update() {
		if (PlayerPrefs.GetInt ("Audio") == 0) {
			music.volume = 0f;
		}
		else if (PlayerPrefs.GetInt ("Audio") == 1) {
			music.volume = 0.75f;
		}
		else {
			Debug.Log("music error");
		}
	}
}
