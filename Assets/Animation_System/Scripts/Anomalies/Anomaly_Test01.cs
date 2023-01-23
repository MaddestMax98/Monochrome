using AnomalySystem.ScriptableObjects;
using PlayerCharacter;
using UnityEngine;

public class Anomaly_Test01 : Anomaly
{
    private Transform _orignalTransform;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _orignalTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>(); 
    }
    public override void Manifest(Player player)
    {
        float randX = Random.Range(-1, 1) * 200f;
        float randY = Random.Range(0.2f, 1) * 200f;
        float randZ = Random.Range(-1, 1) * 200f;

        if (randX + randY == 0)
            randX = 200f;

        if (_isPlayerWitness)
            player.Sanity -= 2;
        else
            player.Sanity -= 1;

        Disable();

        _rigidbody.AddForce(new Vector3(randX, randY, randZ));
    }

    public override void Fix()
    {
        gameObject.transform.position = _orignalTransform.position;
        gameObject.transform.rotation = _orignalTransform.rotation;
    }
}
