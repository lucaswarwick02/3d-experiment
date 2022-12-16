using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{

    Vector3 itemScale = new Vector3(17.5f, 17.5f, 17.5f);

    public void GenerateItem (Gun gun) {
        if (gun != null) {
            GameObject gunModel = Gun.CreateGunObject(gun, true);
            gunModel.transform.parent = transform;

            Outline outline = gunModel.AddComponent<Outline>();
            outline.enabled = true;
            outline.OutlineWidth = 0.35f;
            outline.OutlineColor = Item.GetRarityColor(gun.Rarity);

            gunModel.transform.localScale = itemScale;
            gunModel.transform.localPosition = new Vector3(0, -20, 0);
            gunModel.layer = LayerMask.NameToLayer("UI");
            gunModel.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("UI");
        }
    }
}
