using UnityEngine;

public class DebtPapers : MemoryShard
{
    [SerializeField] private AudioSource projector;
    [SerializeField] private AudioSource dialog;
    [SerializeField] private Canvas screen;
    [SerializeField] private Light projectorLight;
    private bool isWaiting = false;
    private float waitingTime = 0f;

    private void Update()
    {
        if (isWaiting)
        {
            if (waitingTime > 0f)
                waitingTime -= Time.deltaTime;
            else
                EndScene();
        }
           
    }

    private void EndScene()
    {
        isWaiting = false;
        projector.Stop();
        projectorLight.enabled = false;
        screen.enabled = false;

        screen.gameObject.transform.localScale = new Vector3(0, 0, 0);

        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        projectorLight.range = 0.0001f;
        foreach (Transform child in projectorLight.transform)
        {
            child.gameObject.GetComponent<Light>().range = 0.0001f;
        }

        Destroy(gameObject);
    }

    public override void PlayMemory()
    {
        projector.Play();
        dialog.Play();

        projectorLight.range = 9f;
        foreach (Transform child in projectorLight.transform)
        {
            child.gameObject.GetComponent<Light>().range = 8f;
        }

        screen.gameObject.transform.localScale = new Vector3(1, 1, 1);

        waitingTime = dialog.clip.length;
        isWaiting = true;  
    }
}
