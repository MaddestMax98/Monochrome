using Item;
using ScripatbleObj;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinSystem : MonoBehaviour
{
    [SerializeField]
    private MannequinInventoryData mannequinInventoryData;
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

    // Start is called before the first frame update
    void Start()
    {
        DisplayItems();
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
                switch (mannequinInventoryData.Items[i].Type)
                {
                    case ItemType.Hat:
                        SpawnItem(Head, mannequinInventoryData.Items[i]);
                        break;
                    case ItemType.Jacket:
                        SpawnItem(Torso, mannequinInventoryData.Items[i]);
                        break;
                    case ItemType.Pants:
                        SpawnItem(Legs, mannequinInventoryData.Items[i]);
                        break;
                    case ItemType.Shoes:
                        SpawnItem(Feet, mannequinInventoryData.Items[i]);
                        break;
                }
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
                        SpawnItem(Hats[counterJackets].transform, mannequinInventoryData.Items[i]);
                        counterJackets++;
                        break;
                    case ItemType.Pants:
                        SpawnItem(Hats[counterPants].transform, mannequinInventoryData.Items[i]);
                        counterPants++;
                        break;
                    case ItemType.Shoes:
                        SpawnItem(Hats[counterShoes].transform, mannequinInventoryData.Items[i]);
                        counterShoes++;
                        break;
                }
            }

        }
    }

    private void SpawnItem(Transform transform, MannequinItemData obj)
    {
        GameObject item = new GameObject(obj.name);
        item.AddComponent<MeshFilter>();
        item.AddComponent<MeshRenderer>();
        item.GetComponent<MeshFilter>().mesh = obj.mesh;
        item.GetComponent<Renderer>().material = obj.material;

        item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;
    }
}
