using PlayerCharacter;
using UnityEngine;

public class Anomaly_Auditive : Anomaly
{
    private void Awake()
    {
        _originalPos = GetComponent<Transform>();
    }
    public override void AlterObject()
    {
        //Just plays sounds so nothing really happens here :S
    } 

    public override void Fix(Player player)
    {
        base.Fix(player);
    }

    public override void Manifest(Player player)
    {
        base.Manifest(player);
    }
}
