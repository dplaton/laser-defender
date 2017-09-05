using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour {

	public float maxHealth = 250f;
	public float curHealth = 0f;

	// Use this for initialization
	void Start () {
		Debug.Log ("Running start");
		curHealth = maxHealth;
	}

	public void DecreaseHealth(float amount) {
		curHealth -= amount; 
		UpdateHealthBar (curHealth / maxHealth);
	}

	void UpdateHealthBar(float health) {
		transform.localScale = new Vector2 (Mathf.Clamp(health, 0, 1), transform.localScale.y);
	}
}
