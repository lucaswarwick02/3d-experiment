using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject hud;
    public GameObject menu;

    [Header("Menu")]
    public GameObject inventoryContainer;
    public GameObject itemHolderPrefab;
    float columns = 5;

    bool isPaused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) ToggleMenu();
    }

    public void ToggleMenu () {
        isPaused = !isPaused;

        if (isPaused) {
            hud.SetActive(false);
            menu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            generateInventory();
        }
        else {
            hud.SetActive(true);
            menu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            resetInventory();
        }
    }

    public void generateInventory () {
        // Generate the Equipped Gun
        GameObject equippedGunHolder = Instantiate(itemHolderPrefab, inventoryContainer.transform);
        equippedGunHolder.transform.localPosition = new Vector3(0f, 125f, 0f);
        equippedGunHolder.GetComponent<ItemHolder>().GenerateItem(PlayerData.INSTANCE.equipped);
        equippedGunHolder.GetComponent<Button>().onClick.AddListener(delegate{ resetInventory(); PlayerData.INSTANCE.RemoveEquippedGun(); generateInventory(); });

        // Generate the Inventory Array
        for (int i = 0; i < PlayerData.INSTANCE.inventory.Length; i++) {
            Gun gun = PlayerData.INSTANCE.inventory[i];

            GameObject itemHolder = Instantiate(itemHolderPrefab, inventoryContainer.transform);
            int x = (int)(i % columns) - (int)((columns - 1) / 2);
            int y = (int)(i / columns);
            itemHolder.transform.localPosition = new Vector3(x * 225, y * -125, 0);

            itemHolder.GetComponent<ItemHolder>().GenerateItem(gun);

            int index = i;

            itemHolder.GetComponent<Button>().onClick.AddListener(delegate{ resetInventory(); PlayerData.INSTANCE.Swap(index); generateInventory(); });
        }
    }

    public void resetInventory () {
        foreach (Transform child in inventoryContainer.transform) {
            Destroy(child.gameObject);
        }
    }
}
