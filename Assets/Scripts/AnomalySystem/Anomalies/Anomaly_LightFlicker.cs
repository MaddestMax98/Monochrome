using PlayerCharacter;
using UnityEngine;

public class Anomaly_LightFlicker : Anomaly
{
    private bool _isFlickering = false;
    private Light _light;

    private void Awake()
    {
        _originalPos = GetComponent<Transform>();
        _light = GetComponent<Light>();
    }
    private void Update()
    {
        if (_isFlickering)
        {
            _light.intensity = Mathf.PingPong(Time.time * 45, 6);
        }
    }

    public override void AlterObject()
    {
        _isFlickering = true;
    }

    public override void Fix(Player player)
    {
        base.Fix(player);
        _isFlickering = false;
    }

    public override void Manifest(Player player)
    {
        base.Manifest(player);
        AlterObject();
    }
}
