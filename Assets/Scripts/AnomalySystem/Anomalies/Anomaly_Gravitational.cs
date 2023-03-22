using PlayerCharacter;
using UnityEngine;

public class Anomaly_Gravitational : Anomaly
{ 
    private Rigidbody _rigidbody;

    private void Awake()
    {
        setOriginalPos(GetComponent<Transform>().position, GetComponent<Transform>().rotation, GetComponent<Transform>().localScale);
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isActive())
        {
            if (gameObject.transform.position.y >= 1) _rigidbody.AddForce(0, -0.1f, 0);
            else if (gameObject.transform.position.y <= _originalPos.y + 0.2f) _rigidbody.AddForce(0, 0.1f, 0);
        }     
    }
    public override void Manifest(Player player)
    {
        base.Manifest( player);
        AlterObject();
    }
    public override void Enable()
    {
        base.Enable();
        AlterObject();
    }

    public override void Fix(Player player)
    {
        base.Fix(player);
        gameObject.transform.position = _originalPos;
        gameObject.transform.rotation = _originalRot;
    }

    public override void AlterObject()
    {
        _rigidbody.useGravity = false;
        _rigidbody.AddForce(0, 0.1f, 0);
    }
}
