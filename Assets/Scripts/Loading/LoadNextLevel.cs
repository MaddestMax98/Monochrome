using PlayerCharacter;
using AnomalySystem;
using UnityEngine;

public class LoadNextLevel : Interactable
{
    private AnomalyHandler _aSceneHandler;

    [SerializeField]
    private LoadingScene.Scene scene;
    [SerializeField]
    private string spawnName;

    [SerializeField]
    private bool isTrigger = false;
    private void Awake()
    {
        if(GameObject.FindGameObjectWithTag("AnomalyHandler").gameObject.TryGetComponent<AnomalyHandler>(out AnomalyHandler temp))
        {
            _aSceneHandler = temp;
        }
    }
    public void LoadNextScene()
    {
        if(_aSceneHandler != null) _aSceneHandler.UpdateAnomalyHandler();

        PlayerPrefs.SetString("CURRENT_SPAWN_POINT", spawnName);
        LoadingScene.Load(scene);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            if (isTrigger)
            {
                LoadNextScene();
            }
        }

    }

    public override void Interact()
    {
        LoadNextScene();
    }
}
