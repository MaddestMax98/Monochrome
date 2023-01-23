using PlayerCharacter;
using TMPro;
using UnityEngine;

public class SanityCounter : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textMeshPro.text = "Sanity: " + _player.Sanity.ToString();
    }
}
