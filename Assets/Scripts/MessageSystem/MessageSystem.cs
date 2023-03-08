using ScriptableObjects;
using PlayerCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MessageSystem : MonoBehaviour
{
    public GameObject[] messagePrefab; // [Normal Message (0), Picture (1), Respond (2)]
    
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

    [SerializeField] private GameObject _datePrefab;
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

        // Set up previous wife texts:
        SetInitialDialogues();
       

    }
    private void Update()
    {
        //Debug Purposes
        //TODO - Make Pretty

        if (Input.GetKeyUp(KeyCode.F1))
        {
            AddDialogueBox();
        }

        if (_canRespond == true && _playerMessages.current < _playerMessages.texts.Length)
        {
            CreateDialogue(messagePrefab[2], _playerMessages);
            _canRespond = false;
            _player.Sanity += 2;

            UpdateSignal(_signalDetection.getStrength());
        }
    }

    //Changes depending on the sender
    void CreateDialogue(GameObject prefab, MessageData sender = null, ImageData image = null, bool isSelfRespnded = false)
    {
        int x = (int)_currentUser;
        GameObject newBox = Instantiate(prefab, _dialogueHolder[x].transform, false) as GameObject;
        
        if(sender != null)
        {
            var TMP = newBox.GetComponentInChildren<TextMeshProUGUI>();
            TMP.text = sender.texts[sender.current];
            sender.current++;
            
            if(prefab == messagePrefab[2] && !isSelfRespnded)
                TMP.alpha = 0f;
            else
                newBox.GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            var theImage = newBox.transform.Find("Image").GetComponent<Image>();
            theImage.sprite = _photos.images[image.current];
        }

        RectTransform newRectTrans = newBox.GetComponent<RectTransform>();

        if (prefab == messagePrefab[2])
            _playerTexts.Add(newBox.GetComponent<StatusChanger>());
    }

    private void PrintDate(GameObject prefab, string date)
    {
        int x = (int)_currentUser;

        GameObject newBox = Instantiate(prefab, _dialogueHolder[x].transform, false) as GameObject;
        RectTransform newRectTrans = newBox.GetComponent<RectTransform>();
        newBox.GetComponent<UpdateDate>().UpdateCurrentDate(date);
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
                        }

                        CreateDialogue(messagePrefab[0], _wifeMessages);

                        _canRespond = true;
                        UpdateSignal(4);
                    }
                   
                }
                break;
            case CurrentUser.WORK:
                if (_workMessages.current < _workMessages.texts.Length)
                {
                    CreateDialogue(messagePrefab[0], _workMessages);
                    UpdateSignal(4);
                }
                break;
            case CurrentUser.PSYCHOLOGIST:
                if (_psychMessages.current < _psychMessages.texts.Length)
                {
                    CreateDialogue(messagePrefab[0], _psychMessages);
                    UpdateSignal(4);
                }
                break;
        }

        UpdateSignal(_signalDetection.getStrength());

    }

    private void PrintWifeInitDialogue()
    {
        _currentUser = CurrentUser.WIFE;

        PrintDate(_datePrefab, "10/09");

        while (_wifeMessages.current < 4)
        {
            CreateDialogue(messagePrefab[0], _wifeMessages);

            if (_wifeMessages.current != 4)
                CreateDialogue(messagePrefab[2], _playerMessages, null, true);

            UpdateSignal(4);
        }

        PrintDate(_datePrefab, "11/09");

        while (_playerMessages.current < 6)
        {
            CreateDialogue(messagePrefab[2], _playerMessages, null, true);
            CreateDialogue(messagePrefab[2], _playerMessages, null, true);
            CreateDialogue(messagePrefab[0], _wifeMessages);
            CreateDialogue(messagePrefab[0], _wifeMessages);

            UpdateSignal(4);
        }

        PrintDate(_datePrefab, "12/09");
    }

    private void PrintWorkInitDialogue()
    {
        _currentUser = CurrentUser.WORK;

        PrintDate(_datePrefab, "12/09");
        
        while(_workMessages.current < _workMessages.texts.Length)
        {
            CreateDialogue(messagePrefab[0], _workMessages);
        }
       
        UpdateSignal(4);
    }

    private void PrintPsychInitDialogue()
    {
        _currentUser = CurrentUser.PSYCHOLOGIST;

        PrintDate(_datePrefab, "01/09");

        while (_psychMessages.current < _psychMessages.texts.Length)
        {
            CreateDialogue(messagePrefab[0], _psychMessages);
        }

        UpdateSignal(4);
    }

    private void SetInitialDialogues()
    {
        PrintWifeInitDialogue();
        PrintWorkInitDialogue();
        PrintPsychInitDialogue();

        _currentUser = CurrentUser.WIFE;
    }
    public void UpdateUser(CurrentUser user) => _currentUser = user;
}

