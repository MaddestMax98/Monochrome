using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    public virtual void Awake()
    {
        if (this.GetComponent<Outline>() == null)
        {
            this.AddComponent<Outline>();
            this.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
            this.GetComponent<Outline>().OutlineWidth = 0;
            this.GetComponent<Outline>().OutlineColor = Color.red;
        }
    }
}
