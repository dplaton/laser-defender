using UnityEngine;
using System.Collections;

abstract public class RootFormationController : MonoBehaviour {

    float spawnDelay = 0.5f;
    private int spawnedCnt = 0; 

    protected bool AllMembersAreDead() {
        foreach (Transform childPosition in transform) {
            // the enemy ship is the child of the position
            if (childPosition.childCount > 0) {
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
    protected void SpawnUntilFull() {
        Transform nextFreePosition = NextFreePosition();
        if (nextFreePosition != null) {
            GameObject enemy = Instantiate(GetEnemyPrefab(), nextFreePosition.position, Quaternion.identity) as GameObject;
            spawnedCnt++;
			enemy.GetComponent<Animator> ().SetInteger ("animation", GetAnimationId ());
            enemy.transform.parent = nextFreePosition;
        }
        if (NextFreePosition()) {
            Invoke("SpawnUntilFull", spawnDelay);
        }

    }

    protected abstract GameObject GetEnemyPrefab();

	protected abstract int GetAnimationId ();

}
