using AnomalySystem.ScriptableObjects;
using PlayerCharacter;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Anomaly : MonoBehaviour
{
    [SerializeField] protected AnomalyData _data;
    [SerializeField] protected AudioSource _audioSource;

    protected Transform _originalPos;

    private bool _isActive = false;
    private LayerMask _playerLayer = (1 << 10);

    public delegate void OnSanityTaken();
    public static OnSanityTaken onSanityTaken;

    public delegate void OnSanityGiven();
    public static OnSanityGiven onSanityGiven;

    public virtual void Manifest(Player player)
    {
        _isActive = true;

        if (Physics.CheckSphere(transform.position, 4.5f, _playerLayer.value))
        {
            player.SetSanity(-2);
            _audioSource.clip = _data.triggeredNextToPlayer;
            _audioSource.volume = 0.7f;
            _audioSource.Play();
            StartCoroutine(PlayStatic());
        }
        else
        {
            player.SetSanity(-1);
            _audioSource.clip = _data.triggered;
            _audioSource.volume = 0.7f;
            _audioSource.Play();
            StartCoroutine(PlayStatic());
        }
        
        if (onSanityTaken != null)
            onSanityTaken?.Invoke();
    }
    public virtual void Fix(Player player) 
    {
        player.SetSanity(1);
       
        if (onSanityGiven != null)
            onSanityGiven?.Invoke();
    }
    public AnomalyData GetData() { return _data; }
    public void SetData(AnomalyData data) { _data = data; }
    public virtual void Enable() 
    {
        _isActive = true;
        ChangeSoundToStatic();
    }
    public void Disable() { _isActive = false; }
    public bool isActive() { return _isActive; }
    public void setOriginalPos(Vector3 position, Quaternion rotation, Vector3 scale) {
        _originalPos.position = position;
        _originalPos.rotation = rotation;
        _originalPos.localScale = scale;
    }
    IEnumerator PlayStatic()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        ChangeSoundToStatic();
    }

    private void ChangeSoundToStatic()
    {
        _audioSource.clip = _data.staticSound;
        _audioSource.loop = true;
        _audioSource.maxDistance = 6f;
        _audioSource.spatialBlend = 1f;
        _audioSource.rolloffMode = AudioRolloffMode.Linear;
        _audioSource.Play();
    }
    public Transform GetOriginalPos() { return _originalPos; }
    public abstract void AlterObject();
}
  
