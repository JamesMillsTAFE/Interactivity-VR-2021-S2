using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace VRFramework.Input
{
    public class VrControllerInput : MonoBehaviour
    {
        // Contains the current state of the steam actions for non-event based usage
        public bool IsGrabPressed { get; private set; } = false;
        public bool IsUsePressed { get; private set; } = false;
        public bool IsPointerPressed { get; private set; } = false;
        public bool IsTeleportPressed { get; private set; } = false;
        public Vector2 TouchpadAxis { get; private set; } = Vector2.zero;

        [Header("SteamVR Input Actions")]
        // These are the input actions that steam will send when the relevant buttons on the controller
        // are sent. Each type of action has a unique set of values, ie SteamVR_Action_Boolean contains
        // a boolean for when they are pressed and SteamVR_Action_Vector2 contains the vector2 relating
        // to the touchpad/joystick position
        [SerializeField] private SteamVR_Action_Boolean grabAction;
        [SerializeField] private SteamVR_Action_Boolean useAction;
        [SerializeField] private SteamVR_Action_Boolean pointerAction;
        [SerializeField] private SteamVR_Action_Boolean teleportAction;
        [SerializeField] private SteamVR_Action_Vector2 touchpadPosAction;

        [Header("Unity Input Actions")]
        // These are the events we can listen to in Unity for when any of the InputActions of steam get fired
        public VrInputEvent onGrabPressed = new VrInputEvent();
        public VrInputEvent onGrabReleased = new VrInputEvent();
        public VrInputEvent onUsePressed = new VrInputEvent();
        public VrInputEvent onUseReleased = new VrInputEvent();
        public VrInputEvent onPointerPressed = new VrInputEvent();
        public VrInputEvent onPointerReleased = new VrInputEvent();
        public VrInputEvent onTeleportPressed = new VrInputEvent();
        public VrInputEvent onTeleportReleased = new VrInputEvent();
        public VrInputEvent onTouchpadPosChanged = new VrInputEvent();

        private VrController controller;

        private void OnGrabPressed(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source)
        {
            onGrabPressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }

        private void OnGrabReleased(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source)
        {
            onGrabReleased.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }

        private void OnUsePressed(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source)
        {
            onUsePressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }

        private void OnUseReleased(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source)
        {
            onUseReleased.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }

        private void OnPointerPressed(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source)
        {
            onPointerPressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }

        private void OnPointerReleased(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source)
        {
            onPointerReleased.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }

        private void OnTeleportPressed(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source)
        {
            onTeleportPressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }

        private void OnTeleportReleased(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source)
        {
            onTeleportReleased.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }

        // Axis is the current position of the touchpad whereas Delta is the amount it changed between calls
        private void OnTouchpadPosChanged(SteamVR_Action_Vector2 _data, SteamVR_Input_Sources _source, Vector2 _axis, Vector2 _delta)
        {
            onTouchpadPosChanged.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, _axis));
        }

        public void Setup(VrController _controller)
        {
            controller = _controller;

            // Link the functions to the Down and Up states of the SteamVR Actions
            grabAction.AddOnStateDownListener(OnGrabPressed, controller.Source);
            grabAction.AddOnStateUpListener(OnGrabReleased, controller.Source);

            useAction.AddOnStateDownListener(OnUsePressed, controller.Source);
            useAction.AddOnStateUpListener(OnUseReleased, controller.Source);

            pointerAction.AddOnStateDownListener(OnPointerPressed, controller.Source);
            pointerAction.AddOnStateUpListener(OnPointerPressed, controller.Source);

            teleportAction.AddOnStateDownListener(OnTeleportPressed, controller.Source);
            teleportAction.AddOnStateUpListener(OnTeleportReleased, controller.Source);

            touchpadPosAction.AddOnChangeListener(OnTouchpadPosChanged, controller.Source);
        }

        public void Process()
        {
            // Copy the current states of the actions into the corresponding variables
            IsGrabPressed = grabAction.state;
            IsUsePressed = useAction.state;
            IsPointerPressed = pointerAction.state;
            IsTeleportPressed = teleportAction.state;
            TouchpadAxis = touchpadPosAction.axis;
        }
    } 
}