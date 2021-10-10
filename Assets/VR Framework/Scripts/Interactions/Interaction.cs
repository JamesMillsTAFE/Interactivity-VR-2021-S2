using UnityEngine;

using VRFramework.Input;

namespace VRFramework.Interactions
{
    public abstract class Interaction : MonoBehaviour
    {
        public VrController controller;
        public VrControllerInput input;
        // This is the object that is currently being interacted with.
        // It is not set when no interaction is occuring
        public InteractableObject interactingObject;

        // This contains the current setup state of the interaction.
        // Can be used for safety checking things
        public bool IsSetup { get; private set; } = false;

        // This is the function that is called when the controller is ready
        // to be interacting with objects.
        public virtual void Setup(VrController _controller)
        {
            controller = _controller;
            input = controller.Input;

            IsSetup = true;
        }
         
        // Allow us to attach an object to the specified location giving us more control over how the object looks when grabbed
        protected void SnapObject(Transform _snapHandle)
        {
            Rigidbody attachPoint = controller.Rigidbody;
            if(_snapHandle == null)
            {
                interactingObject.transform.position = attachPoint.transform.position;
            }
            else
            {
                // This calculation allows us to orient the object along the forward axis of the snap handle
                interactingObject.transform.rotation = attachPoint.transform.rotation * 
                    (Quaternion.Euler(_snapHandle.localEulerAngles));

                // This calculation allows us to place the object in the correct position relative to the controller
                interactingObject.transform.position = attachPoint.transform.position - 
                    (_snapHandle.position - interactingObject.transform.position);
            }
        }

        protected void SetCollidingObject(Collider _collider)
        {
            // Check that there is either an object already colliding with the controller or no InteractableObject
            // script on the colliding object.
            InteractableObject interactable = _collider.GetComponent<InteractableObject>();
            if (interactingObject != null || interactable == null)
                return;

            // Check that the InteractableObject can actually be interacted with
            if (!CanInteract(interactable))
                return;

            // We can interact with this object so store it
            interactingObject = interactable;
        }

        protected abstract bool CanInteract(InteractableObject _interactable);

        // This function allows us to make the controller visible or not when interacting with something.
        protected void SetControllerVisibility(bool _visible)
        {

        }
    } 
}
