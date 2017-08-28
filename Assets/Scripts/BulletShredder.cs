using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShredder : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject o = collision.gameObject;
		Debug.Log ("Object shredded");
        Destroy(o);
    }
}
