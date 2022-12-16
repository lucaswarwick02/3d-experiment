using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

    float speed = 10.5f;
    float gravity = -19.62f;

    Vector3 velocity;
    Vector3 move;

    [Header("Double Jump")]
    float jumpHeight = 2f;
    int maxJumps = 2;
    int jumps;
    float djCooldown = 1f;
    float djCooldownTimer = 1f;

    [Header("Dash")]
    float dashDuration = 0.25f;
    float dashSpeed = 15f;
    float dashCooldown = 1f;
    float dashCooldownTimer = 1f;
    bool isDashing;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        jumps = maxJumps;
    }

    private void Update() {
        if (controller.isGrounded && velocity.y < 0) {
            velocity.y = -2f;
            jumps = maxJumps;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer >= dashCooldown && !isDashing) {
            StartCoroutine(dash());
        }

        HUD.INSTANCE.updateDashTimer(dashCooldownTimer / dashCooldown);
        dashCooldownTimer = Mathf.Clamp(dashCooldownTimer + Time.deltaTime, 0f, dashCooldown);

        controller.Move(move * speed * multiplier() * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && jumps > 0 && djCooldownTimer >= djCooldown) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumps--;
            if (jumps != maxJumps - 1) {
                // If it isn't the first jump...
                djCooldownTimer = 0f;
            }
        }

        HUD.INSTANCE.updateDoubleJumpTimer(djCooldownTimer / djCooldown);
        djCooldownTimer = Mathf.Clamp(djCooldownTimer + Time.deltaTime, 0f, djCooldown);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator dash () {
        isDashing = true;
        dashCooldownTimer = 0f;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration) {
            controller.Move(move * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }

    float multiplier () {
        if (PlayerCombat.INSTANCE.isZoom) {
            return 0.5f;
        }
        
        return 1f;
    }
}