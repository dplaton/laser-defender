using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public GameObject bulletPrefab;
	public float health = 200f;
	public float shotsPerSecond = 0.5f;

	Vector2 size;

	void Start() {
		size = this.GetComponent<SpriteRenderer> ().size;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile bullet = collider.gameObject.GetComponent<Projectile> ();
		Debug.Log("Enemy hit!");
		if (bullet) {
			health -= bullet.GetDamage ();
			bullet.Hit ();
			if (health <= 0) {
			//	Destroy (gameObject);
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
		Vector3 bulletPosition = transform.position - new Vector3(0, size.y);
		GameObject bullet = Instantiate (bulletPrefab, bulletPosition, Quaternion.identity) as GameObject;
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -10f);
	}
}
