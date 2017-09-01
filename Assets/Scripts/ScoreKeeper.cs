using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	int score = 0;
	private Text scoreText;

	void Start() {
		scoreText = gameObject.GetComponent<Text> ();
		scoreText.text = "0";
	}

	public void Score(int amount) {
		this.score += amount;
		scoreText.text = score.ToString();
	}

	public void Reset() {
		this.score = 0;
		scoreText.text = "0";
	}
}
