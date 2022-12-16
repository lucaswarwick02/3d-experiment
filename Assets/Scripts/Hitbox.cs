using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public EntityHealth entityHealth;
    public HitboxType hitboxType;

    private void OnTriggerEnter(Collider other) {
        Projectile projectile = other.GetComponent<Projectile>();

        switch (hitboxType) {
            case HitboxType.Normal:
                entityHealth.takeDamage(projectile.rawDamage);
                break;
            case HitboxType.Critical:
                entityHealth.takeDamage(Mathf.CeilToInt(projectile.rawDamage * 1.5f));
                break;
            case HitboxType.Block:
                break;
            default:
                entityHealth.takeDamage(Mathf.CeilToInt(projectile.rawDamage * 0.1f));
                break;
        }

        if (projectile.destroyOnHit) Destroy(other.gameObject);
    }
}


public enum HitboxType {
    Normal,
    Critical,
    Block
}