using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private GameObject UIMainMenu;
        [SerializeField] private GameObject UINewGame;
        [SerializeField] private GameObject UILoadSaves;
        [SerializeField] private GameObject UITutorial;
        [SerializeField] private GameObject UISettings;
        [SerializeField] private GameObject UIQuit;

        private void Awake()
        {
            ShowMainMenu();
        }

        public void NewGame()
        {
            GetComponent<SetupGame>().SetupDemo();
            LoadingScene.Load(LoadingScene.Scene.SaveRoom);
        }

        public void LoadSave()
        {
            
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ShowMainMenu()
        {
            UIMainMenu.SetActive(true);
            UINewGame.SetActive(false);
            UILoadSaves.SetActive(false);
            UITutorial.SetActive(false);
            UISettings.SetActive(false);
            UIQuit.SetActive(false);
        }

        public void ShowNewGame()
        {
            UIMainMenu.SetActive(false);
            UINewGame.SetActive(true);
        }

        public void ShowLoadSaves()
        {
            UIMainMenu.SetActive(false);
            UILoadSaves.SetActive(true);
        }

        public void ShowTutorial()
        {
            UIMainMenu.SetActive(false);
            UITutorial.SetActive(true);
        }

        public void ShowSettings()
        {
            UIMainMenu.SetActive(false);
            UISettings.SetActive(true);
        }

        public void ShowQuit()
        {
            UIMainMenu.SetActive(false);
            UIQuit.SetActive(true);
        }
    }
}

