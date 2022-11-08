using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField]
    private LoadingScene.Scene scene;

    public void LoadNextScene()
    {
        LoadingScene.Load(scene);
    }
}
