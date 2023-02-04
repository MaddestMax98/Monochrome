using PlayerCharacter;
using System.Collections;
using UnityEngine;

public class SanityGiver : MonoBehaviour
{
    public delegate void OnSanityGiven();
    public static OnSanityGiven onSanityGiven;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().Sanity += 10;
            if(onSanityGiven != null)
                onSanityGiven?.Invoke();
            DisableForFewSeconds();
        }
    }

    IEnumerator DisableForFewSeconds()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
