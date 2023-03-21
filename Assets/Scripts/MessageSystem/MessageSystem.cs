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
    private MessageStorageData _responses;
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

    private List<StatusChanger> _playerTexts = new List<StatusChanger>();

    //For notifications
    private int _initialPlayerTextIndex = 0;
    private bool _isWaitingForAnswer = false;
    private AudioSource _notificationSound;
    public delegate void NotifyPlayer(CurrentUser user);
    public static NotifyPlayer onPlayerNotified;

    private void OnEnable()
    {
        Anomaly.onSanityTaken += StartNewConversation;
        StatusChanger.onPlayerAnswer += RespondToPlayer; 
    }

    private void OnDisable()
    {
        Anomaly.onSanityTaken -= StartNewConversation;
        StatusChanger.onPlayerAnswer -= RespondToPlayer;
    }

    private void Awake()
    {
        _playerMessages.current = 0;
        _wifeMessages.current = 0;
        _psychMessages.current = 0;
        _workMessages.current = 0;
        _photos.current = 0;
    }
    private void Start()
    {
        _player = GetComponent<Player>();
        _notificationSound = GetComponent<AudioSource>();

        GameObject mySignal = GameObject.Find("Signal");
        if (mySignal != null)
            _signalDetection = mySignal.GetComponent<SignalDetection>(); //TODO - Define signal game object name

        if (_responses.isScenePersistenceLinked == false)
        {
            SetInitialDialogues(true);
            _responses.isScenePersistenceLinked = true;
        }
        else
        {
            SetInitialDialogues(false);
            ApplyScenePersistence();
        }

         _initialPlayerTextIndex = _playerMessages.current;
    }

    private void RespondToPlayer()
    {
        _currentUser = CurrentUser.WIFE;
        _responses.totalWifeRespones++;
        NotifyUser();
        _isWaitingForAnswer = false;

        _player.SetSanity(2);

        UpdateSignal(4);
        AddDialogueBox(1, false, false);
        _player.UpdateAnimator();
    }

    private void ApplyScenePersistence()
    {
        for(int i = 0; i < _responses.totalWifeRespones; i++)
        {
            CreateDialogue(messagePrefab[1], null, _photos);
            CreateDialogue(messagePrefab[0], _wifeMessages);
            CreateDialogue(messagePrefab[2], _playerMessages, null, true);
            CreateDialogue(messagePrefab[0], _wifeMessages);
        }

        if (_responses.isWaitingForResponse)
        {
            _responses.isWaitingForResponse = false;
            StartNewConversationWithoutNotification();
        }
    }

    private void StartNewConversation()
    {
        if(_player.Sanity <= 4)
        {
            _responses.isWaitingForResponse = true;
            _currentUser = CurrentUser.WIFE;
            if(AddDialogueBox(1, true, true))
                NotifyUser();
        }
    }

    private void StartNewConversationWithoutNotification()
    {
       _currentUser = CurrentUser.WIFE;
       AddDialogueBox(1, true, true);
    }

    private void Update()
    {
        if (_canRespond == true && _playerMessages.current < _playerMessages.texts.Length)
        {
            CreateDialogue(messagePrefab[2], _playerMessages);
            UpdateSignal(_signalDetection.getStrength());
            _canRespond = false;
        }
    }

    //Changes depending on the sender
    void CreateDialogue(GameObject prefab, MessageData sender = null, ImageData image = null, bool isSelfResponded = false)
    {
        int x = (int)_currentUser;
        GameObject newBox = Instantiate(prefab, _dialogueHolder[x].transform, false) as GameObject;
        
        if(sender != null)
        {
            var TMP = newBox.GetComponentInChildren<TextMeshProUGUI>();
            TMP.text = sender.texts[sender.current];
            sender.current++;

            if (prefab == messagePrefab[2] && !isSelfResponded)
            {
                TMP.alpha = 0f;
                _isWaitingForAnswer = true;
            }
            else
            {
                Button button = newBox.GetComponent<Button>();
                button.onClick.Invoke();
                button.interactable = false;
            }
               
        }
        else
        {
            var theImage = newBox.transform.Find("Image").GetComponent<Image>();
            theImage.sprite = _photos.images[image.current];
            _photos.current++;
        }

        if (prefab == messagePrefab[2])
            _playerTexts.Add(newBox.GetComponent<StatusChanger>());
    }

    private void PrintDate(GameObject prefab, string date)
    {
        int x = (int)_currentUser;

        GameObject newBox = Instantiate(prefab, _dialogueHolder[x].transform, false) as GameObject;
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
    private bool AddDialogueBox(int i, bool hasImage, bool needsAnswer)
    {
        int temp = 0;

        while (temp < i)
        {
            switch (_currentUser)
            {
                case CurrentUser.WIFE:
                    if (_wifeMessages.current < _wifeMessages.texts.Length && _isWaitingForAnswer == false)
                    {
                        CreateNewWifeDialog(hasImage, needsAnswer);
                    }
                    else return false;
                    break;
                case CurrentUser.WORK:
                    if (_workMessages.current < _workMessages.texts.Length)
                    {
                        CreateDialogue(messagePrefab[0], _workMessages);
                        NotifyUser();
                        UpdateSignal(4);
                    }
                    else return false;
                    break;
                case CurrentUser.PSYCHOLOGIST:
                    if (_psychMessages.current < _psychMessages.texts.Length)
                    {
                        CreateDialogue(messagePrefab[0], _psychMessages);
                        NotifyUser();
                        UpdateSignal(4);
                    }
                    else return false;
                    break;
            }

            UpdateSignal(_signalDetection.getStrength());

            temp++;
        }

        return true;
    }

    private void NotifyUser()
    {
        _notificationSound.Play();

        if(onPlayerNotified != null)
            onPlayerNotified?.Invoke(_currentUser);
    }

    private void CreateNewWifeDialog(bool hasImage, bool needsResponse)
    {
        if (_photos.current < _photos.images.Length && hasImage)
        {
            CreateDialogue(messagePrefab[1], null, _photos);
        }

        CreateDialogue(messagePrefab[0], _wifeMessages);

        UpdateSignal(4);

        if (needsResponse)
        {
            _isWaitingForAnswer = true;
            _canRespond = true;
        }
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

    private void SetInitialDialogues(bool notifyUser)
    {
        PrintWifeInitDialogue();
        _currentUser = CurrentUser.WORK;
        if(notifyUser) NotifyUser();
        PrintWorkInitDialogue();
        PrintPsychInitDialogue();

        _currentUser = CurrentUser.WIFE;
    }
    public void UpdateUser(CurrentUser user) => _currentUser = user;
}

