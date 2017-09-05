using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontRowController : RootFormationController {

    public GameObject enemyPrefab;

    public float speed = 2.5f;
    public float width;
    public float height;

    float xMin;
    float xMax;

    private bool movingRight = true;

    // Use this for initialization
    void Start () {
        Debug.Log("Spawning formation " + name);
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

    protected override GameObject GetEnemyPrefab() {
        return enemyPrefab;
    }

	protected override int GetAnimationId() {
		return 2;
	}

}

