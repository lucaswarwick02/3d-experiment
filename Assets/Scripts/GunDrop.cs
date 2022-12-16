using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDrop : MonoBehaviour
{
    public Gun gun;
    //public Light pointLight;
    Outline outline;

    private void Start() {
        GameObject model = Gun.CreateGunObject(gun, false);
        model.transform.parent = transform;
        SetLayerRecursively(gameObject, gameObject.layer);

        outline = model.AddComponent<Outline>();
        outline.enabled = false;
        outline.OutlineWidth = 7.5f;
        outline.OutlineColor = Item.GetRarityColor(gun.Rarity);

        model.transform.localPosition = Vector3.zero;
        //pointLight.color = Item.GetRarityColor(gun.Rarity);

        if (gun.Rarity == Rarity.Legendary || gun.Rarity == Rarity.Mythical) {
            //pointLight.intensity = 50f;
        }
    }

    public void EnableOutline () {
        outline.enabled = true;
    }

    public void DisableOutline () {
        outline.enabled = false;
    }

    public void Interact () {
        PlayerData.INSTANCE.AddToInventory(gun);
        Destroy(gameObject);
    }

    void SetLayerRecursively(GameObject child, LayerMask layer) {
        child.layer = layer;
        foreach (Transform childTransform in child.transform) {
            SetLayerRecursively(childTransform.gameObject, layer);
        }
    }
}
