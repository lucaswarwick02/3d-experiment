using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD INSTANCE;

    public TextMeshProUGUI gunName;
    public TextMeshProUGUI interact;

    public Image dash;
    public Image doubleJump; 

    public Image miniDash;
    public Image miniDoubleJump;

    private void Awake() {
        INSTANCE = this;
    }

    public void updateDashTimer (float percentage) {
        dash.fillAmount = percentage;
        miniDash.enabled = percentage >= 1f;
    }

    public void updateDoubleJumpTimer (float percentage) {
        doubleJump.fillAmount = percentage;
        miniDoubleJump.enabled = percentage >= 1f;
    }

    public void updateGunName (Gun gun) {
        if (gun != null) {
            gunName.text = gun.Name;
            gunName.color = Item.GetRarityColor(gun.Rarity);
        }
        else {
            gunName.text = "";
        }
    }

    public void ActivateInteract (string prompt) {
        interact.enabled = true;
        interact.text = prompt;
    }
    public void DeactivateInteract () {
        interact.enabled = false;
    }
}
