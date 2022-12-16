using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat INSTANCE;

    [Header("Gun")]
    public Transform gunParent;
    Gun currentGun;
    GameObject gunObject;
    float gunRotation = -90f;
    [HideInInspector] public bool isZoom;
    float currentFireRate;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Gun Sway")]
    float swayAmount = 0.08f;
    float swaySmoothSpeed = 5.0f;
    Vector3 initialPosition;

    [Header("Gun Recoil")]
    Quaternion initialRotation;

    private void Awake() {
        INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateEquipped();
    }

    private void Update() {
        float movementX = Input.GetAxis("Mouse X") * swayAmount;
        float movementY = Input.GetAxis("Mouse Y") * swayAmount;

        if (currentGun != null) {
            isZoom = Input.GetMouseButton(1);
            if (isZoom) {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, currentGun.Zoom, Time.deltaTime * 10f);
            }
            else {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, Time.deltaTime * 10f);
            }

            Vector3 finalPosition = new Vector3(-movementX, -movementY, 0) + initialPosition;
            gunObject.transform.localPosition = Vector3.Lerp(gunObject.transform.localPosition, finalPosition, Time.deltaTime * swaySmoothSpeed);

            if ((currentGun.IsAuto && Input.GetButton("Fire1")) || (!currentGun.IsAuto && Input.GetButtonDown("Fire1"))) {
                if (currentFireRate <= 0f) {
                    Shoot();
                }
                else {
                    // Lerp recoil position back to the initial rotation
                    gunObject.transform.localRotation = Quaternion.Lerp(gunObject.transform.localRotation, initialRotation, Time.deltaTime * currentGun.RecoilRecovery);
                    currentFireRate = Mathf.Clamp(currentFireRate - Time.deltaTime, 0f, Mathf.Infinity);
                }
            }
            else {
                // Lerp recoil position back to the initial rotation
                gunObject.transform.localRotation = Quaternion.Lerp(gunObject.transform.localRotation, initialRotation, Time.deltaTime * currentGun.RecoilRecovery);
                currentFireRate = Mathf.Clamp(currentFireRate - Time.deltaTime, 0f, Mathf.Infinity);
            }
        }
    }

    void Shoot () {
        for (int i = 0; i < currentGun.Count; i++) {

            Quaternion accuracy = Quaternion.Euler(Random.Range(-currentGun.Accuracy, currentGun.Accuracy), -90 + Random.Range(-currentGun.Accuracy, currentGun.Accuracy), Random.Range(-currentGun.Accuracy, currentGun.Accuracy));
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * accuracy);
            RaycastHit hitPoint;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitPoint);
            Vector3 velocity = new Vector3();
            if (hitPoint.point == Vector3.zero) {
                // Did not hit anything, shoot towards arbitrary point
                velocity = Camera.main.transform.position + (Camera.main.transform.forward * 50f) - firePoint.position;
            }
            else {
                // Shoot towards point
                velocity = hitPoint.point - firePoint.position;
            }
            bullet.GetComponent<Rigidbody>().velocity = velocity.normalized * 80f;
            bullet.GetComponent<Projectile>().rawDamage = currentGun.Damage;
        }

        // Recoil the gun
        Quaternion recoilRotation = Quaternion.Euler(-currentGun.Recoil, Random.Range(-3f, 3f), 0);
        gunObject.transform.localRotation = recoilRotation * initialRotation;

        currentFireRate = currentGun.FireRate;
    }

    public void UpdateEquipped () {
        Destroy(gunObject);

        currentGun = PlayerData.INSTANCE.equipped;

        HUD.INSTANCE.updateGunName(currentGun);

        if (currentGun != null) {
            gunObject = Gun.CreateGunObject(PlayerData.INSTANCE.equipped, true);
            gunObject.transform.parent = gunParent;

            firePoint.transform.localPosition = currentGun.firePoint;
            gunObject.transform.localPosition = currentGun.offset;
            gunObject.transform.localRotation = Quaternion.Euler(0, gunRotation, 0);

            initialPosition = gunObject.transform.localPosition;
            initialRotation = gunObject.transform.localRotation;
        }
    }
}
