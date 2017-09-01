using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	private Text scoreText;

	void Start() {
		scoreText = gameObject.GetComponent<Text> ();
		scoreText.text = "0";
	}

	public void Score(int amount) {
		score += amount;
		scoreText.text = score.ToString();
	}

	public static void Reset() {
		score = 0;
	}
}
