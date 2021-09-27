using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRFramework.Input
{
    // The custom Unity event that we can use for when an input action is activated
    [System.Serializable]
    public class VrInputEvent : UnityEvent<VrInputActionData> { }

    [System.Serializable]
    public class VrInputActionData
    {
        // The controller that the InputAction was fired on
        public VrController sender;

        // The collider of the controller the InputAction was fired on
        public Collider collider;

        // The rigidbody of the controller the InputAction was fired on
        public Rigidbody rigidbody;

        // The position of the touchpad that the InputAction was fired on.
        // This is filled out even if the event was not to do with the position
        // of the touchpad changing.
        public Vector2 touchpadPosition;

        public VrInputActionData(VrController _sender, Collider _collider, Rigidbody _rigidbody, Vector2 _touchpadPosition)
        {
            this.sender = _sender;
            this.collider = _collider;
            this.rigidbody = _rigidbody;
            this.touchpadPosition = _touchpadPosition;
        }
    }
}
