using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject bulletPrefab; 
    public float speed = 10f;
    public float bulletSpeed;

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
            Vector3 bulletPosition = transform.position + new Vector3(0, size.y / 2);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, bulletSpeed);
        }
    }


}
