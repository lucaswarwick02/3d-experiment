using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    // The player's game object.
    public GameObject player;

    // The speed at which the object will rotate.
    public float rotationSpeed = 10.0f;

    void Update()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        // Get the direction to the player.
        Vector3 direction = player.transform.position - transform.position;

        // Rotate the object towards the player at the specified speed.
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
    }
}
