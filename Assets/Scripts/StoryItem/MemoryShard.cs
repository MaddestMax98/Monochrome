using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScripatbleObj;

public class MemoryShard : MonoBehaviour
{
    [SerializeField] private StoryItem itemData;
    void Start()
    {
        itemData.state = StoryItemState.Active;
    }
}
