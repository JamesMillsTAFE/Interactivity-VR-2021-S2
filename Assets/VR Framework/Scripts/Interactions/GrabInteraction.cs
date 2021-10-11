using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

using VRFramework.Input;

namespace VRFramework.Interactions
{
    public class GrabInteraction : Interaction
    {
        // The object that is currently being held
        public InteractableObject heldObject;
        public bool hideControllerWhenGrabbed = false;

        // The events to fire when an object is grabbed
        public InteractionEvent grabbed = new InteractionEvent();
        public InteractionEvent released = new InteractionEvent();

        private Transform heldObjectOriginalParent;

        protected override bool CanInteract(InteractableObject _interactable)
        {
            // If the object can be grabbed, and this controller is allowed to grab it, return true
            return _interactable.canGrab &&
                (_interactable.allowedGrabControllers == controller.Source ||
                _interactable.allowedGrabControllers == SteamVR_Input_Sources.Any);
        }

        public override void Setup(VrController _controller)
        {
            base.Setup(_controller);

            // Setup the event handlers for this interaction
            input.onGrabPressed.AddListener(OnGrabPressed);
            input.onGrabReleased.AddListener(OnGrabReleased);
        }

        private void OnGrabPressed(VrInputActionData _data)
        {
            if(interactingObject != null && heldObject == null)
            {
                InteractEventData data = GenerateData(interactingObject.Collider, InteractEventData.Interaction.Grab);

                Grab(data);
                heldObject.OnObjectGrabbed(data);
            }
            else if(heldObject != null && !heldObject.holdButtonToGrab)
            {
                InteractEventData data = GenerateData(heldObject.Collider, InteractEventData.Interaction.Grab);

                heldObject.OnObjectReleased(data);
                Release(data);
            }
        }

        private void OnGrabReleased(VrInputActionData _data)
        {
            if(heldObject != null)
            {
                // If the object doesn't require the button to be held, we don't want to release the object
                if(!heldObject.holdButtonToGrab)
                {
                    return;
                }

                InteractEventData data = GenerateData(heldObject.Collider, InteractEventData.Interaction.Grab);

                heldObject.OnObjectReleased(data);
                Release(data);
            }
        }

        private void Grab(InteractEventData _data)
        {
            // Store the held object as the interacting one and it's original parent
            heldObject = interactingObject;
            heldObjectOriginalParent = heldObject.transform.parent;

            // Set the held objects parent to us and make it kinematic so physics don't affect it
            heldObject.transform.SetParent(transform);
            heldObject.Rigidbody.isKinematic = true;

            // Snap the object and if applicable, hide the controller
            SnapObject(heldObject.snapHandle);
            if (hideControllerWhenGrabbed)
                SetControllerVisibility(false);

            // Fire the grab event with the passed data
            grabbed.Invoke(_data);
        }

        private void Release(InteractEventData _data)
        {
            // Fire the released event with the passed data
            released.Invoke(_data);

            // Reset the held objects parent to its original one and make it affected by physics again
            heldObject.Rigidbody.isKinematic = false;
            heldObject.transform.SetParent(heldObjectOriginalParent);

            // Make the held object retain the velocity of the controller, making it seem like we actually threw it
            heldObject.Rigidbody.angularVelocity = controller.AngularVelocity;
            heldObject.Rigidbody.velocity = controller.Velocity;

            // Force the controller to be active
            SetControllerVisibility(true);

            // Reset the heldObject and heldObjectOriginalParent variables to null
            heldObject = null;
            heldObjectOriginalParent = null;
        }
    }
}
