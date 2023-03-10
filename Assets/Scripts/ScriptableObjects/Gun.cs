using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Gun", fileName ="Gun")]
public class Gun : ScriptableObject
{
    [Header("Positions")]
    public Vector3 offset;
    public Vector3 firePoint;

    [Header("Visuals")]
    [SerializeField] private GameObject _model;
    public GameObject Model { get { return _model; } }
    [SerializeField] private Texture2D _skin;
    public Texture2D Skin { get { return _skin; } }

    [Header("Values")]
    [SerializeField] private int _damage = 10;
    public int Damage { get { return _damage; } }
    [SerializeField] private float _fireRate = 0.25f;
    public float FireRate { get { return _fireRate; } }
    [SerializeField] private bool _isAuto = true;
    public bool IsAuto { get { return _isAuto; } }
    [SerializeField, Range(30f, 60f)] private float _zoom = 40f;
    public float Zoom { get { return _zoom; } }
    [SerializeField] private float _recoil = 5f;
    public float Recoil { get { return _recoil; } }
    [SerializeField] private float _recoilRecovery = 20f;
    public float RecoilRecovery { get { return _recoilRecovery; } }
    [SerializeField] private int _count = 1;
    public int Count { get { return _count; } }
    [SerializeField] private float _accuracy = 1f;
    public float Accuracy { get { return _accuracy; } }

    [Header("Vanity")]
    [SerializeField] private string _name = "Example Gun";
    public string Name { get { return _name; } }
    [SerializeField] private string _description = "Example Description";
    public string Description { get { return _description; } }
    [SerializeField] private Rarity _rarity = Rarity.Common;
    public Rarity Rarity { get { return _rarity; } }


    [ContextMenu("Generate AR")]
    void GenerateAR () {
        // Positions
        offset = new Vector3(0.47f,-0.77f,0.95f);
        firePoint = new Vector3(0.48f, -0.21f, 2.7f);

        // Values
        _damage = 8;
        _fireRate = 0.1f;
        _isAuto = true;
        _zoom = 45f;
        _recoil = 10f;
        _recoilRecovery = 20f;
        _count = 1;
        _accuracy = 2f;
    }

    [ContextMenu("Generate Pistol")]
    void GeneratePistol () {
        // Positions
        offset = new Vector3(0.5f, -0.65f, 1.1f);
        firePoint = new Vector3(0.49f, -0.24f, 2.21f);
        
        // Values
        _damage = 25;
        _fireRate = 0.15f;
        _isAuto = false;
        _zoom = 40f;
        _recoil = 20f;
        _recoilRecovery = 20f;
        _count = 1;
        _accuracy = 1f;
    }

    /// <summary>
    /// Instantiate a gun into the scene
    /// </summary>
    /// <param name="gun">Gun being referenced</param>
    /// <param name="removeCollider">Whether to remove the MeshCollider component</param>
    /// <returns></returns>
    public static GameObject CreateGunObject (Gun gun, bool removeCollider) {
        GameObject gunObject = Instantiate(gun.Model, Vector3.zero, Quaternion.identity);

        // Set the scale of the gun (vox exports are always far too large)
        gunObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        // Set the Skin
        Material gunMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        gunMaterial.SetTexture("_BaseMap", gun.Skin);
        gunMaterial.SetFloat("_Smoothness", 0f);
        gunObject.GetComponentInChildren<MeshRenderer>().material = gunMaterial;
        gunObject.GetComponentInChildren<MeshRenderer>().receiveShadows = false;

        // Remove the MeshCollider
        if (removeCollider) {
            MeshCollider[] colliders = gunObject.GetComponentsInChildren<MeshCollider>();
            foreach (MeshCollider collider in colliders) {
                Destroy(collider);
            }
        }
        else {
            MeshCollider[] colliders = gunObject.GetComponentsInChildren<MeshCollider>();
            foreach (MeshCollider collider in colliders) {
                collider.convex = true;
            }
        }

        return gunObject;
    }
}
