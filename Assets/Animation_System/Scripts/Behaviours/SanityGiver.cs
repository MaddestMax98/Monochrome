using PlayerCharacter;
using System.Collections;
using UnityEngine;

public class SanityTaker : MonoBehaviour
{
    public delegate void OnSanityTaken();
    public static OnSanityTaken onSanityTaken;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().Sanity -= 10;
            if(onSanityTaken != null)
                onSanityTaken?.Invoke();
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
