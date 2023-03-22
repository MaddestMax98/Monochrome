using PlayerCharacter;
using UnityEngine;

public class Anomaly_LightFlicker : Anomaly
{
    private bool _isFlickering = false;
    [SerializeField] private int _maxIntensity = 3;
    private Light _light;

    private void Awake()
    {
        setOriginalPos(GetComponent<Transform>().position, GetComponent<Transform>().rotation, GetComponent<Transform>().localScale);
        _light = GetComponent<Light>();
    }
    private void Update()
    {
        if (_isFlickering)
        {
            _light.range = Mathf.PingPong(Time.time * 45, _maxIntensity);
        }
    }

    public override void Enable()
    {
        base.Enable();
        AlterObject();
    }
    public override void AlterObject()
    {
        _isFlickering = true;
    }

    public override void Fix(Player player)
    {
        base.Fix(player);
        _isFlickering = false;
        _light.range = _maxIntensity;
    }

    public override void Manifest(Player player)
    {
        base.Manifest(player);
        AlterObject();
    }
}
