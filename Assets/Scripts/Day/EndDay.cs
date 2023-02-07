using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDay : Interactable
{
    [SerializeField]
    GameObject endDayUI;

    private void Awake()
    {
        endDayUI.SetActive(false);
    }

    public override void Start()
    {
        base.Start();

    }

    public override void Interact()
    {
        if (GameObject.Find("SceneManager").GetComponent<DaySystem>().IsMainTasksDone())
        {
            endDayUI.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
