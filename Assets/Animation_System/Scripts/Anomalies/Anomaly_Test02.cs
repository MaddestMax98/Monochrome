using AnomalySystem.ScriptableObjects;
using PlayerCharacter;
using ScripatbleObj;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Anomaly_Test02 : Anomaly
{
    private Transform _orignalTransform;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _orignalTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if (!isActive())
        {
            if (gameObject.transform.position.y >= 1) _rigidbody.AddForce(0, -0.1f, 0);
            else if (gameObject.transform.position.y <= _orignalTransform.position.y + 0.2f) _rigidbody.AddForce(0, 0.1f, 0);
        }     
    }
    public override void Manifest(Player player)
    { 

        if (_isPlayerWitness)
            player.Sanity -= 2;
        else
            player.Sanity -= 1;

        Disable();

        _rigidbody.useGravity = false;
        _rigidbody.AddForce(0, 0.1f, 0);
        _rigidbody.AddTorque(_orignalTransform.forward * 2);
    }

    public override void Fix()
    {
        gameObject.transform.position = _orignalTransform.position;
        gameObject.transform.rotation = _orignalTransform.rotation;
    }
}
