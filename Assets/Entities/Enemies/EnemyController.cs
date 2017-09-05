using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject bulletPrefab;
	public float health = 200f;
	public float shotsPerSecond = 2.5f;
	public AudioClip downSound;
	public AudioClip fireSound;

    public float soundVolume;

	Vector2 size;
	ScoreKeeper scoreKeeper;
    private AudioSource audioSource;

	void Start() {
		size = this.GetComponent<SpriteRenderer> ().size;
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
        audioSource = GetComponent<AudioSource>();
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
        //AudioSource.PlayClipAtPoint (fireSound, transform.position, soundVolume);
        SoundManager.PlayClipAt(fireSound, transform.position);
	}

	void Die() {
		scoreKeeper.Score (1);
		SoundManager.PlayClipAt (downSound, transform.position);
		Destroy (gameObject);
	}
    

}
