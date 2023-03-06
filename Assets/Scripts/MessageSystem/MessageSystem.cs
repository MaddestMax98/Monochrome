using ScriptableObjects;
using PlayerCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MessageSystem : MonoBehaviour
{
    public GameObject[] messagePrefab;
    
    private Player _player;
    private SignalDetection _signalDetection;

    [SerializeField]
    private MessageData _playerMessages;
    [SerializeField]
    private MessageData _wifeMessages;
    [SerializeField]
    private MessageData _workMessages;
    [SerializeField]
    private MessageData _psychMessages;
    [SerializeField]
    private ImageData _photos;

    private CurrentUser _currentUser;

    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private List<GameObject> _dialogueHolder;

    private bool _canRespond;

    // Keeps track of the transform of the previous addition
    private RectTransform lastRectTrans = null;
    private List<StatusChanger> _playerTexts = new List<StatusChanger>();

    void Awake()
    {
        _wifeMessages.current = 0;
        _photos.current = 0;
        _playerMessages.current = 0;
        _workMessages.current = 0;
        _psychMessages.current = 0;
    }
    private void Start()
    {
        _player = GetComponent<Player>();
        _signalDetection = GameObject.Find("Signal").GetComponent<SignalDetection>(); //TODO - Define signal game object name
    }
    private void Update()
    {
        //Debug Purposes
        //TODO - Make Pretty

        if (Input.GetKeyUp(KeyCode.Keypad1))
        { 
            _currentUser = CurrentUser.WIFE;
        }

        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            _currentUser = CurrentUser.WORK;

        }
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            _currentUser = CurrentUser.PSYCHOLOGIST;
        }
        if (Input.GetKeyUp(KeyCode.F1))
        {
            AddDialogueBox();
        }

        if (_canRespond == true && _playerMessages.current < _playerMessages.texts.Length)
        {
            CreateDialogue(messagePrefab[2], _playerMessages);
            _canRespond = false;
            _playerMessages.current++;
            _player.Sanity += 2;

            UpdateSignal(_signalDetection.getStrength());
        }
    }

    //Changes depending on the sender
    void CreateDialogue(GameObject prefab, MessageData sender = null, ImageData image = null)
    {
        int x = (int)_currentUser;
        GameObject newBox = Instantiate(prefab, _dialogueHolder[x].transform, false) as GameObject;
        
        if(sender != null)
        {
            var TMP = newBox.GetComponentInChildren<TextMeshProUGUI>();
            TMP.text = sender.texts[sender.current];
            
            if(prefab == messagePrefab[2])
                TMP.alpha = 0f;
        }
        else
        {
            var theImage = newBox.transform.Find("Image").GetComponent<Image>();
            theImage.sprite = _photos.images[image.current];
        }

        RectTransform newRectTrans = newBox.GetComponent<RectTransform>();

        // If this isn't the first dialog box being added
        if (lastRectTrans != null)
        {
            Vector2 newPos;
       
            newPos = new Vector2(lastRectTrans.localPosition.x,
                                 lastRectTrans.localPosition.y - newRectTrans.rect.height + 1f);
      
            newRectTrans.localPosition = newPos;

        }

        lastRectTrans = newRectTrans;

        if (prefab == messagePrefab[2])
            _playerTexts.Add(newBox.GetComponent<StatusChanger>());
    }

    public void UpdateSignal(int strength)
    {
        
        if(_playerTexts != null)
        {
            int x = (int)_currentUser;

            for (int i = 0; i < _playerTexts.Count; i++)
            {

                if (_playerTexts[i].GetStatus() < strength)
                    _playerTexts[i].ChangeStatus(strength);
            }
        }
       
    }
    private void AddDialogueBox()
    {
        //TODO - Message Audio SOUND

        switch (_currentUser)
        {
            case CurrentUser.WIFE:
                if (_wifeMessages.current < _wifeMessages.texts.Length)
                {
                    bool canText = true;

                    if(_playerTexts.Count > 0)
                    {
                        if (_playerTexts[_playerTexts.Count - 1].HasResponded() == false)
                            canText = false;
                    }

                    if (canText)
                    {
                        if (_photos.current < _photos.images.Length)
                        {
                            CreateDialogue(messagePrefab[1], null, _photos);
                            _photos.current++;
                        }

                        CreateDialogue(messagePrefab[0], _wifeMessages);

                        _canRespond = true;
                        _wifeMessages.current++;
                        UpdateSignal(4);
                    }
                   
                }
                break;
            case CurrentUser.WORK:
                if (_workMessages.current < _workMessages.texts.Length)
                {
                    CreateDialogue(messagePrefab[0], _workMessages);
                    _workMessages.current++;
                    UpdateSignal(4);
                }
                break;
            case CurrentUser.PSYCHOLOGIST:
                if (_psychMessages.current < _psychMessages.texts.Length)
                {
                    CreateDialogue(messagePrefab[0], _psychMessages);
                    _psychMessages.current++;
                    UpdateSignal(4);
                }
                break;
        }

        UpdateSignal(_signalDetection.getStrength());

    }
    public void UpdateUser(CurrentUser user) => _currentUser = user;
}

