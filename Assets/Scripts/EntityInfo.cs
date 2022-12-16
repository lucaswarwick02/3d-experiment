using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityInfo : MonoBehaviour
{
    public Image healthBar;
    public EntityHealth entityHealth;

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = entityHealth.health / (float) entityHealth.maxHealth;
    }
}
