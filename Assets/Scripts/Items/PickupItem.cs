using UnityEngine;

namespace Item
{
    public abstract class PickupItem: Interactable
    {
        public abstract void Pickup();

        public override void Interact()
        {
            Pickup();
        }
    }
}

