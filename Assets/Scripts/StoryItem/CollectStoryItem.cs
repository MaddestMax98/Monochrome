using ScripatbleObj;
using UnityEngine;

public class CollectStoryItem : Interactable
{
    [SerializeField]
    private StoryItem itemData;
    private MemoryShard shard;

    public StoryItem ItemData { get => itemData; set => itemData = value; }

    private void Awake()
    {
        shard = GetComponent<MemoryShard>();
    }
    public override void Start()
    {
        if (ItemData.state == StoryItemState.Collected)
        {
            foreach(Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }
            
            Destroy(gameObject);
        }
    }

    public override void Interact()
    {
        shard.PlayMemory();
        ItemData.state = StoryItemState.Collected;
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
