using ScriptableObjects;
using UnityEngine;

public class SetTutorial : MonoBehaviour
{
    [SerializeField] private MessageStorageData isMessageLinked;

    private void Awake()
    {
        if(isMessageLinked.isScenePersistenceLinked)
            gameObject.SetActive(false);
    }
}
