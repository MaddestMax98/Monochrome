using PlayerCharacter;
using UnityEngine;

public class Anomaly_Gravitational : Anomaly
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
        if (isActive())
        {
            if (gameObject.transform.position.y >= 1) _rigidbody.AddForce(0, -0.1f, 0);
            else if (gameObject.transform.position.y <= _orignalTransform.position.y + 0.2f) _rigidbody.AddForce(0, 0.1f, 0);
        }     
    }
    public override void Manifest(Player player)
    {
        base.Manifest( player);
        AlterObject();
    }

    public override void Fix(Player player)
    {
        base.Fix(player);
        gameObject.transform.position = _orignalTransform.position;
        gameObject.transform.rotation = _orignalTransform.rotation;
    }

    public override void AlterObject()
    {
        _rigidbody.useGravity = false;
        _rigidbody.AddForce(0, 0.1f, 0);
        _rigidbody.AddTorque(_orignalTransform.forward * 2);
    }
}
