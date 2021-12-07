using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRFramework.Interactions
{
    [System.Serializable]
    public class InteractionEvent : UnityEvent<InteractEventData> { }

    [System.Serializable]
    public class InteractEventData
    {
        public enum Interaction
        {
            Touch,
            Grab,
            Use,
            Object
        }

        // The object being interacted with
        public InteractableObject interactable;

        // The controller interacting with the interactable object
        public VrController controller;

        // The collider of the interactable object
        public Collider collider;

        // The rigidbody of the interactable object
        public Rigidbody rigidbody;

        // The type of interaction that is happening in this event
        public Interaction interaction;

        public InteractEventData(
            InteractableObject _interactable, 
            VrController _controller, 
            Collider _collider, 
            Rigidbody _rigidbody, 
            Interaction _interaction)
        {
            this.interactable = _interactable;
            this.controller = _controller;
            this.collider = _collider;
            this.rigidbody = _rigidbody;
            this.interaction = _interaction;
        }
    } 
}
