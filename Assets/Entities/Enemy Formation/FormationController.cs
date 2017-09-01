using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

    public GameObject enemyPrefab;

    public float speed = 2.5f;
    public float width;
    public float height;
	public float spawnDelay = 0.5f;

    float xMin;
    float xMax;

    private bool movingRight;

    // Use this for initialization
    void Start () {
		SpawnUntilFull ();
        // the distance between the camera and the object's plane
        float distance = transform.position.z - Camera.main.transform.position.z;

        // these coordinates are relative to the BOTTOM-LEFT of the screen
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xMin = leftMost.x;
        xMax = rightMost.x;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update () {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        transform.position += direction * speed * Time.deltaTime;
 
        float rightEdge = transform.position.x + (0.5f * width);
        float leftEdge = transform.position.x - (0.5f * width);

        if (rightEdge > xMax) {
            movingRight = false;
        } else if (leftEdge < xMin) {
            movingRight = true;
        }

		if (AllMembersAreDead ()) {
			Debug.Log ("Squad killed");
			SpawnUntilFull ();
		}
    }

	bool AllMembersAreDead() {
		foreach (Transform childPosition in transform) {
			// the enemy ship is the child of the position
			if (childPosition.childCount > 0){
				return false;
			}
		}
		return true;
	}

	Transform NextFreePosition() {
		foreach (Transform child in transform) {
			if (child.childCount == 0) {
				return child;
			}
		}

		return null;
	}

	void SpawnUntilFull() {
		Transform nextFreePosition = NextFreePosition ();
		if (nextFreePosition != null) {
			Debug.Log ("Spawning enemy ship...");
			GameObject enemy = Instantiate (enemyPrefab, nextFreePosition.position, Quaternion.identity) as GameObject;
			enemy.GetComponent<Animator> ().SetInteger ("animation",Random.Range(1,3));
			enemy.transform.parent = nextFreePosition;
		}
		if (NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}

	}
}

