using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool destroyOnHit = false;
    public float destroyDelay = 2f;

    [HideInInspector] public int rawDamage;

    private void Awake() {
        Destroy(gameObject, destroyDelay);
    }
}
