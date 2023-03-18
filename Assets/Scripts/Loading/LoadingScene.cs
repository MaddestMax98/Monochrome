using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadingScene
{
    private class LoadingMonoBehaviour : MonoBehaviour { }

    public enum Scene
    {
        MainMenu,
        Loading,
        SaveRoom,
        Hallway,
        Lobby,
        DressingRoom,
        Maintance,
        ProjectorRoom,
        Bathroom,
        TheatreRoom
    }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene scene)
    {
       
        //Set the loading callback action to load the target scene
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
           
        };
        //Load the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoadingCallback()
    {
        //Triggered after the first update which lets the screen refresh
        //Execute the loading callback action which will load the target scene
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        
        while(!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if(loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }
}
