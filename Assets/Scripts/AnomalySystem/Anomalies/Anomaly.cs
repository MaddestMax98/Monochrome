using AnomalySystem.ScriptableObjects;
using PlayerCharacter;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Anomaly : Interactable
{
    [SerializeField] protected AnomalyData _data;
    [SerializeField] protected AudioSource _audioSource;

    protected Vector3 _originalPos;
    protected Quaternion _originalRot;
    protected Vector3 _originalScale;

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
        _isActive = false;
        _audioSource.Stop();
       
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
        _originalPos = position;
        _originalRot = rotation;
        _originalScale = scale;
    }
    IEnumerator PlayStatic()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        ChangeSoundToStatic();
    }
    public override void Interact()
    {
        if (_isActive)
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Fix(player);
        }
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
    public Vector3 GetOriginalPos() { return _originalPos; }
    public Quaternion GetOriginalRotation() { return _originalRot; }
    public Vector3 GetOriginalScale() { return _originalScale; }
    public abstract void AlterObject();
}
  
