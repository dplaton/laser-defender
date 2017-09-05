using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject bulletPrefab; 
    public float speed = 10f;
    public float bulletSpeed;
    public float firingRate = 0.2f;
	public float health = 250f;
	public AudioClip fireSound;
	public HealthBarController healthBar;

    float padding;
    float minX;
    float maxX;

    Vector2 size;

    private void Start() {
        size = this.GetComponent<SpriteRenderer>().size;
        padding = size.x;

        // the distance between the camera and the object's plane
        float distance = transform.position.z - Camera.main.transform.position.z;

        // these coordinates are relative to the BOTTOM-LEFT of the screen
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        minX = leftMost.x + padding ;
        maxX = rightMost.x - padding;
    }

    void Fire() {
        Vector3 bulletPosition = transform.position + new Vector3(0, size.y / 2);
		GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, bulletSpeed);
		SoundManager.PlayClipAt(fireSound, transform.position);
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, 0);

        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("Fire", 0.0000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("Fire");
        }
    }

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile bullet = collider.gameObject.GetComponent<Projectile> ();
		if (bullet) {
			bullet.Hit ();
//			healthBar.DecreaseHealth (bullet.GetDamage ());
			health -= bullet.GetDamage ();
			if (health <= 0) {
				Die ();
			}
		}
	}
	void Die() {
		LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager> ();
		levelManager.LoadLevel ("Win Screen");
		Destroy (gameObject);
	}


}
