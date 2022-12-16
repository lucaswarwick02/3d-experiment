using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnvironment : MonoBehaviour
{
    public Transform environmentContainer;
    public GameObject floorTile;

    [ContextMenu("Create Example Floor")]
    void CreateExampleFloor () {
        int radius = 25;
        float scale = 4f;

        for (int x = -radius; x < radius; x++) {
            for (int z = -radius; z < radius; z++) {
                GameObject tile = Instantiate(floorTile, environmentContainer);
                tile.transform.localPosition = new Vector3(x * scale, 0 , z * scale);
            }
        }
    }

    [ContextMenu("Reset Environment")]
    void ResetEnvironment () {
        while (environmentContainer.childCount != 0) {
            foreach (Transform child in environmentContainer) {
                DestroyImmediate(child.gameObject);
            }
        }
    }

}
