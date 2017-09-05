using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip endClip;
	public AudioClip gameMusic;

	private AudioSource music;

	void Awake() {
		music = this.GetComponent<AudioSource>();
	}

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
		
	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene theScene, LoadSceneMode mode) {
		if (music == null) {
			Debug.LogError ("Music is null");
			return;
		}
		
		music.Stop ();

		if (theScene.name.Equals ("Start Menu")) {
			music.clip = startClip;
		} else if (theScene.name.Equals ("Game")) {
			music.clip = gameMusic;
		} else if (theScene.name.Equals ("Win Screen")) {
			music.clip = endClip;
		}
		music.loop = true;
		music.Play();
	}
}
