using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class DisplayWardrobeItems : MonoBehaviour
    {
        [Header("Item location")]
        [SerializeField] private Transform[] Hats;
        [SerializeField] private Transform[] Jackets;
        [SerializeField] private Transform[] Pants;
        [SerializeField] private Transform[] Shoes;

        [Header("Items")]
        [SerializeField] private MannequinItemData[] items;
    }
}

