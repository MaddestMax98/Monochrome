using AnomalySystem.ScriptableObjects;
using PlayerCharacter;
using UnityEngine;

public abstract class Anomaly : MonoBehaviour
{
    [SerializeField]
    private AnomalyData _data;
    private bool _isActive = true;
    protected bool _isPlayerWitness = false;
    public abstract void Manifest(Player player);
    public abstract void Fix();
    public  AnomalyData GetData() { return _data; }
    public void SetData(AnomalyData data) { _data = data; }
    public void Enable() { _isActive = true; }
    public void Disable() { _isActive = false; }
    public bool isActive() { return _isActive; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") _isPlayerWitness = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") _isPlayerWitness = false;
    }
}
