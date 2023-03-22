using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLights : MonoBehaviour
{
    [SerializeField]
    private GameObject brokenItem;
    private Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        switch (brokenItem.GetComponent<BrokenItem>().Data.state)
        {
            case BrokenItemState.NotImportant:
                return;
            case BrokenItemState.CurrentTask:
                break;
            case BrokenItemState.IsRepaired:
                return;
            case BrokenItemState.Cascade:
                break;
        }
            _light.range = Mathf.PingPong(Time.time * 25, _light.intensity);

    }
}
