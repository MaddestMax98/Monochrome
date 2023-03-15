using AnomalySystem.ScriptableObjects;
using PlayerCharacter;
using UnityEngine;

public abstract class Anomaly : MonoBehaviour
{
    [SerializeField]
    protected AnomalyData _data;
    protected Transform _originalPos;

    private bool _isActive = false;
    private LayerMask _playerLayer = (1 << 10);

    public delegate void OnSanityTaken();
    public static OnSanityTaken onSanityTaken;

    public delegate void OnSanityGiven();
    public static OnSanityGiven onSanityGiven;

    public virtual void Manifest(Player player)
    {

        if (Physics.CheckSphere(transform.position, 4.5f, _playerLayer.value)) player.SetSanity(-2);
        else player.SetSanity(1);
        
        if (onSanityTaken != null)
            onSanityTaken?.Invoke();

        Enable();
    }
    public virtual void Fix(Player player) 
    {
        player.SetSanity(1);
       
        if (onSanityGiven != null)
            onSanityGiven?.Invoke();
    }
    public AnomalyData GetData() { return _data; }
    public void SetData(AnomalyData data) { _data = data; }
    public void Enable() { _isActive = true; }
    public void Disable() { _isActive = false; }
    public bool isActive() { return _isActive; }
    public void setOriginalPos(Vector3 position, Quaternion rotation, Vector3 scale) {
        _originalPos.position = position;
        _originalPos.rotation = rotation;
        _originalPos.localScale = scale;
    }
    public Transform GetOriginalPos() { return _originalPos; }
    public abstract void AlterObject();
}
  
