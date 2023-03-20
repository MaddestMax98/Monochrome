using UnityEngine;
using ScripatbleObj;

public abstract class MemoryShard : MonoBehaviour
{
    [SerializeField] protected StoryItem itemData;
    [SerializeField] protected AudioSource alterPhoneSound;
    void Start()
    {
        if(itemData.state == StoryItemState.Hidden)
        {
            itemData.state = StoryItemState.Active;
            alterPhoneSound.Play();
        }
       
    }

    public abstract void PlayMemory();
}
