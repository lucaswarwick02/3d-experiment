using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [HideInInspector] public int health;

    public GameObject root;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void takeDamage (int rawDamage) {
        health = Mathf.Clamp(health - rawDamage, 0, maxHealth);

        if (health <= 0) {
            Destroy(root);
        }
    }
}
