using Cinemachine;
using Item;
using Manager;
using PlayerCharacter;
using ScripatbleObj;
using ScriptedCamera;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MannequinSystem : Interactable
{
    [SerializeField]
    private MannequinInventoryData mannequinInventoryData;
    [Header("Mannequin Cameras")]
    [SerializeField] private CinemachineVirtualCamera mannequinCamera;
    [SerializeField] private CinemachineVirtualCamera wardrobeCamera;
    [Header("Mannequin parts location")]
    [SerializeField] private Transform Head;
    [SerializeField] private Transform Torso;
    [SerializeField] private Transform Legs;
    [SerializeField] private Transform Feet;
    [Header("Item location")]
    [SerializeField] private Transform[] Hats;
    [SerializeField] private Transform[] Jackets;
    [SerializeField] private Transform[] Pants;
    [SerializeField] private Transform[] Shoes;

    private bool mannequinEnabled = false;
    private bool cameraMode = true;
    private Player playerReference;
    private CinemachineVirtualCamera previousCamera;
    private Ray ray;
    private RaycastHit hit;
    [SerializeField] private LayerMask rayMask;

    //Tutorial
    [SerializeField]
    [Header("Item location")]
    private GameObject _tutorial;
    private bool isActive = false;

    public override void Start()
    {
        base.Start();
        DisplayItems();
            
    }

    private void Update()
    {
        if (mannequinEnabled)
        {
            if (!isActive)
            {
                _tutorial.SetActive(true);
                isActive = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitMannequinMode();
                _tutorial.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cameraMode = !cameraMode;
                if (cameraMode)
                {
                    CameraSwitcher.SwitchCamera(mannequinCamera);
                }
                else
                {
                    CameraSwitcher.SwitchCamera(wardrobeCamera);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.allCameras[0].GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000, rayMask))
                {
                    if (hit.collider.TryGetComponent<MannequinItem>(out MannequinItem item))
                    {
                        if (item.ItemData.isEquiped)
                        {
                            UnEquip(item);

                        }
                        else
                        {
                            Equip(item);
                        }
                    }
                }
            }
        }
    }

    private Transform MannequinPartTransform(ItemType type)
    {
        switch (type)
        {
            case ItemType.Hat:
                return Head;
            case ItemType.Jacket:
                return Torso;
            case ItemType.Pants:
                return Legs;
            case ItemType.Shoes:
                return Feet;
        }
        return null;
    }
    private void Equip(MannequinItem item)
    {
        item.ItemData.isEquiped = true;
        item.gameObject.transform.parent = MannequinPartTransform(item.ItemData.Type);
        item.gameObject.transform.localPosition = Vector3.zero;
    }

    private void UnEquip(MannequinItem item)
    {
        item.ItemData.isEquiped = false;
        item.gameObject.transform.parent = item.WardrobeParent;
        item.gameObject.transform.localPosition = Vector3.zero;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(ray);

    }

    private void DisplayItems()
    {
        int counterHats = 0;
        int counterJackets = 0;
        int counterPants = 0;
        int counterShoes = 0;

        for(int i = 0; i < mannequinInventoryData.Items.Count; i++)
        {
            if (mannequinInventoryData.Items[i].isEquiped)
            {
                SpawnItem(MannequinPartTransform(mannequinInventoryData.Items[i].Type), mannequinInventoryData.Items[i]);
            }
            else if (mannequinInventoryData.Items[i].isPickedUp && !mannequinInventoryData.Items[i].isEquiped)
            {
                switch (mannequinInventoryData.Items[i].Type)
                {
                    case ItemType.Hat:
                        SpawnItem(Hats[counterHats].transform, mannequinInventoryData.Items[i]);
                        counterHats++;
                        break;
                    case ItemType.Jacket:
                        SpawnItem(Jackets[counterJackets].transform, mannequinInventoryData.Items[i]);
                        counterJackets++;
                        break;
                    case ItemType.Pants:
                        SpawnItem(Pants[counterPants].transform, mannequinInventoryData.Items[i]);
                        counterPants++;
                        break;
                    case ItemType.Shoes:
                        SpawnItem(Shoes[counterShoes].transform, mannequinInventoryData.Items[i]);
                        counterShoes++;
                        break;
                }
            }

        }
    }

    private void SpawnItem(Transform transform, MannequinItemData obj)
    {
        GameObject item = new GameObject(obj.name);
        item.layer = LayerMask.NameToLayer("Item");
        item.AddComponent<MeshFilter>();
        item.AddComponent<MeshRenderer>();
        item.GetComponent<MeshFilter>().mesh = obj.mesh;
        item.GetComponent<Renderer>().material = obj.material;
        item.AddComponent<MannequinItem>();
        item.GetComponent<MannequinItem>().ItemData = obj;
        item.AddComponent<BoxCollider>();


        item.transform.parent = transform;
        item.GetComponent<MannequinItem>().WardrobeParent = item.transform.parent;

        item.transform.localPosition = Vector3.zero;
    }

    public void EnterMannequinMode(Player player)
    {
        playerReference = player;
        playerReference.CanMove = false;
        playerReference.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        mannequinEnabled = true;
        previousCamera = CameraManager.ActiveCamera;
        CameraSwitcher.SwitchCamera(mannequinCamera);
    }

    public void ExitMannequinMode()
    {
        mannequinEnabled = false;
        playerReference.CanMove = true;
        playerReference.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        CameraSwitcher.SwitchCamera(previousCamera);
    }

    public override void Interact()
    {
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        EnterMannequinMode(p);
    }
}
