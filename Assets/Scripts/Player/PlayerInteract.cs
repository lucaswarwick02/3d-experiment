using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask layerMask;

    GameObject hoveredObject;

    // Update is called once per frame
    void Update()
    {
        bool interact = Input.GetKeyDown(KeyCode.E);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5f, layerMask)) {
            // Hit something, 
            if (hoveredObject == null) {
                // Hit a new object
                Focus(hit.rigidbody.gameObject);
            }
            if (hoveredObject != hit.rigidbody.gameObject) {
                // Hit a new object
                Focus(hit.rigidbody.gameObject);
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                hoveredObject.SendMessage("Interact");
            }

            // if (hit.rigidbody.tag.Equals("Interactable") && hit.distance < 5f) {
            //     if (hoveredObject == null) {
            //         // New Focus
            //         hoveredObject = hit.rigidbody.gameObject;
            //         HUD.INSTANCE.ActivateInteract("Pickup Item");
            //         hoveredObject.SendMessage("EnableOutline");
            //         Debug.Log(hoveredObject.tag);
            //         Debug.Log(hoveredObject.name);
            //     }
            //     if (hoveredObject != hit.rigidbody.gameObject) {
            //         // New Focus
            //         hoveredObject = hit.rigidbody.gameObject;
            //         HUD.INSTANCE.ActivateInteract("Pickup Item");
            //         hoveredObject.SendMessage("EnableOutline");
            //         Debug.Log(hoveredObject.tag);
            //         Debug.Log(hoveredObject.name);
            //     }

            //     if (interact) {
            //         hoveredObject.SendMessage("Interact");
            //     }
            // }
            // else {
            //     LoseFocus();
            // }
        }
        else {
            LoseFocus();
        }
    }

    void Focus (GameObject rbObj) {
        hoveredObject = rbObj;
        HUD.INSTANCE.ActivateInteract("Interact");
        hoveredObject.SendMessage("EnableOutline");
    }

    void LoseFocus () {
        HUD.INSTANCE.DeactivateInteract();
        if (hoveredObject) {
            hoveredObject.SendMessage("DisableOutline", null, SendMessageOptions.DontRequireReceiver);
            hoveredObject = null;
        }
    }
}
