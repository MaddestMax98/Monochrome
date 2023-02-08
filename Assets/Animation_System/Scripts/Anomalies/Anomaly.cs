using AnomalySystem.ScriptableObjects;
using PlayerCharacter;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class Anomaly : MonoBehaviour
{
    [SerializeField]
    protected AnomalyData _data;
    private bool _isActive = false;
    private LayerMask _playerLayer = (1 << 10);

    public virtual void Manifest(Player player)
    {
        if (Physics.CheckSphere(transform.position, 4.5f, _playerLayer.value))
        {
            player.Sanity -= 2;
            Debug.Log("Too Close");
        }
        else
        {
            Debug.Log("Too Far");
            player.Sanity -= 1;
        }
            

        Enable();
    }
    public virtual void Fix(Player player) { player.Sanity += 1; }
    public AnomalyData GetData() { return _data; }
    public void SetData(AnomalyData data) { _data = data; }
    public void Enable() { _isActive = true; }
    public void Disable() { _isActive = false; }
    public bool isActive() { return _isActive; }
    public abstract void AlterObject();
}
  
