using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData INSTANCE;

    public Gun equipped;
    public Gun[] inventory = new Gun[7];

    private void Awake() {
        INSTANCE = this;
    }

    public void OrderInventory (OrderType orderType) {
        switch (orderType) {
            case OrderType.Rarity:
                Array.Sort(inventory, new CompareRarity());
                break;
            default:
                OrderInventory(OrderType.Rarity);
                break;
        }

        Array.Reverse(inventory);
    }

    public bool AddToInventory (Gun gun) {
        for (int i = 0; i < inventory.Length; i++) {
            if (inventory[i] == null) {
                inventory[i] = gun;
                OrderInventory(OrderType.Rarity);
                return true;
            }
        }
        return false;
    }

    public void Swap (int i) {
        Gun previouslyEquipped = equipped;
        equipped = inventory[i];
        inventory[i] = previouslyEquipped;
        PlayerCombat.INSTANCE.UpdateEquipped();

        OrderInventory(OrderType.Rarity);
    }

    public void RemoveEquippedGun () {
        if (AddToInventory(equipped)) {
            // Item removed into inventory
            equipped = null;
            PlayerCombat.INSTANCE.UpdateEquipped();
        }
        else {
            // Inventory full
        }
    }
}

public enum OrderType {
    Rarity
}