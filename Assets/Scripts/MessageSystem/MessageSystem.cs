using ScriptableObjects;
using System.Collections.Generic;
using PlayerCharacter;
using TMPro;
using UnityEngine;

enum CurrentUser { WIFE, WORK, PSYCHOLOGIST };

public class MessageSystem : MonoBehaviour
{
    public GameObject[] messagePrefab;
    
    private Player _player;

    [SerializeField]
    private MessageData _wifeMessages;
    [SerializeField]
    private MessageData _playerMessages;

    private CurrentUser _currentUser;

    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private GameObject dialogueHolder;

    RectTransform containerRectTrans;

    private bool _canRespond;

    // Keeps track of the transform of the previous addition
    private RectTransform lastRectTrans = null;

    void Awake()
    {
        containerRectTrans = dialogueContainer.GetComponent<RectTransform>();
    }
    private void Start()
    {
        _player = GameObject.Find("Player(Clone)").GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            AddDialogueBox();
        }
    }

    //Changes depending on the sender
    void CreateDialogue(MessageData sender, GameObject prefab)
    {
        RectTransform containerRectTrans = dialogueContainer.GetComponent<RectTransform>();

        GameObject newBox = Instantiate(prefab, dialogueContainer.transform, false) as GameObject;
        newBox.transform.SetParent(dialogueHolder.transform, false);
        newBox.GetComponentInChildren<TextMeshProUGUI>().text = sender.texts[sender.current];


        RectTransform newRectTrans = newBox.GetComponent<RectTransform>();

        // If this isn't the first dialog box being added
        if (lastRectTrans != null)
        {
            Vector2 newPos = new Vector2(lastRectTrans.localPosition.x,
                                         lastRectTrans.localPosition.y - newRectTrans.rect.height);

            newRectTrans.localPosition = newPos;

        }

        lastRectTrans = newRectTrans;
    }
    void AddDialogueBox()
    {
        switch (_currentUser)
        {
            case CurrentUser.WIFE:
                if (_wifeMessages.current < _wifeMessages.texts.Length && _canRespond == false)
                {
                    CreateDialogue(_wifeMessages, messagePrefab[0]);
                    _canRespond = true;
                    _wifeMessages.current++;
                    Debug.Log(_player.Sanity);
                }
                else if (_playerMessages.current < _playerMessages.texts.Length && _canRespond == true)
                {
                    CreateDialogue(_playerMessages, messagePrefab[2]);
                    _canRespond = false;
                    _playerMessages.current++;
                    _player.Sanity += 2;
                }
                break;
                //case CurrentUser.WORK:
                //case CurrentUser.PSYCHOLOGIST:
        }



    }
}

