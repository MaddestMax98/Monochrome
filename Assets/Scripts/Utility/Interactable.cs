using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();
    /*
    public virtual void Awake()
    {

    }
    */
    //We use start instead of awake since in the level manager we first setup the object state
    //And then on object start we apply the effect
    public virtual void Start()
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
