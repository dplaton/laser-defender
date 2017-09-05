using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) { 
        SceneManager.LoadScene(name);
	}

	public void QuitRequest(){
		Application.Quit ();
	}

}
