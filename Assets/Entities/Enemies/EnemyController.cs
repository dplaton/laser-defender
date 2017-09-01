using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject bulletPrefab;
	public float health = 200f;
	public float shotsPerSecond = 0.5f;
	public AudioClip downSound;
	public AudioClip fireSound;

	Vector2 size;
	ScoreKeeper scoreKeeper;

	void Start() {
		size = this.GetComponent<SpriteRenderer> ().size;
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile bullet = collider.gameObject.GetComponent<Projectile> ();

		if (bullet) {
			health -= bullet.GetDamage ();
			bullet.Hit ();
			if (health <= 0) {
				Die ();
			}
		}
	}

	void Update() {
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void Fire() {
		GameObject bullet = Instantiate (bulletPrefab, transform.position, Quaternion.identity) as GameObject;
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -10f);
		AudioSource.PlayClipAtPoint (fireSound, transform.position,0.7f);
	}

	void Die() {
		scoreKeeper.Score (1);
		AudioSource.PlayClipAtPoint (downSound, transform.position, 1f);
		Destroy (gameObject);
	}
}
