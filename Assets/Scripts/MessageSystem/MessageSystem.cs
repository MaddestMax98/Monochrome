using ScriptableObjects;
using PlayerCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum CurrentUser { WIFE, WORK, PSYCHOLOGIST };

public class MessageSystem : MonoBehaviour
{
    public GameObject[] messagePrefab;
    
    private Player _player;
    private SignalDetection _signalDetection;

    [SerializeField]
    private MessageData _wifeMessages;
    [SerializeField]
    private MessageData _playerMessages;
    [SerializeField]
    private ImageData _photos;

    private CurrentUser _currentUser;

    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private GameObject _dialogueHolder;

    private bool _canRespond;

    // Keeps track of the transform of the previous addition
    private RectTransform lastRectTrans = null;

    void Awake()
    {
        _wifeMessages.current = 0;
        _photos.current = 0;
        _playerMessages.current = 0;
    }
    private void Start()
    {
        _player = GameObject.Find("Player(Clone)").GetComponent<Player>();
        _signalDetection = GameObject.Find("Signal").GetComponent<SignalDetection>(); //TODO - Define signal game object name
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            AddDialogueBox();
        }
    }

    //Changes depending on the sender
    void CreateDialogue(GameObject prefab, MessageData sender = null, ImageData image = null)
    {
        GameObject newBox = Instantiate(prefab, _dialogueHolder.transform, false) as GameObject;
        
        if(sender != null)
            newBox.GetComponentInChildren<TextMeshProUGUI>().text = sender.texts[sender.current];
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
            if (lastRectTrans.rect.height == newRectTrans.rect.height)
            {
                 newPos = new Vector2(lastRectTrans.localPosition.x,
                                         lastRectTrans.localPosition.y - newRectTrans.rect.height);
            }
            else if (lastRectTrans.rect.height > newRectTrans.rect.height)
            {
                newPos = new Vector2(lastRectTrans.localPosition.x,
                                         lastRectTrans.localPosition.y - newRectTrans.rect.height - 0.1f);
            }
            else
            {
                newPos = new Vector2(lastRectTrans.localPosition.x,
                                         lastRectTrans.localPosition.y - newRectTrans.rect.height + 0.1f);
            }

            newRectTrans.localPosition = newPos;

        }

        lastRectTrans = newRectTrans;
    }

    public void UpdateSignal(int strength)
    {

        for (int i = 0; i < _dialogueHolder.transform.childCount; i++) {

            if(_dialogueHolder.transform.GetChild(i).TryGetComponent<StatusChanger>(out StatusChanger value))
                value.ChangeStatus(strength);
        }
    }
    private void AddDialogueBox()
    {
        switch (_currentUser)
        {
            case CurrentUser.WIFE:
                if (_wifeMessages.current < _wifeMessages.texts.Length && _canRespond == false)
                {
                    if (_photos.current < _photos.images.Length)
                    {
                        CreateDialogue(messagePrefab[1], null, _photos);
                        _photos.current++;
                    }

                    CreateDialogue(messagePrefab[0], _wifeMessages);
                   
                    _canRespond = true;
                    _wifeMessages.current++;
                   
                }
                else if (_playerMessages.current < _playerMessages.texts.Length && _canRespond == true)
                {
                    CreateDialogue(messagePrefab[2], _playerMessages);
                    _canRespond = false;
                    _playerMessages.current++;
                    _player.Sanity += 2;
                }
                break;
                //case CurrentUser.WORK:
                //case CurrentUser.PSYCHOLOGIST:
        }

        UpdateSignal(_signalDetection.getStrength());

    }
}

