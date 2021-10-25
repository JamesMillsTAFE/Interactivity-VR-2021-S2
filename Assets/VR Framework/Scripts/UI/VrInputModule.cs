using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using Valve.VR;

using VRFramework.Input;

namespace VRFramework.UI
{
    [AddComponentMenu("VR Framework/UI/VR Input Module")]
    public class VrInputModule : BaseInputModule
    {
        public static VrInputModule instance = null;

        [SerializeField] private LayerMask layerMask;

        private Camera uiCamera;
        private PhysicsRaycaster raycaster;

        private Dictionary<VrControllerInput, UiControllerData> controllerData = new Dictionary<VrControllerInput, UiControllerData>();

        protected override void Awake()
        {
            base.Awake();

            if(instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
        }

        protected override void Start()
        {
            base.Start();

            // Create a new camera that will be used for raycasts
            uiCamera = new GameObject("UI Camera").AddComponent<Camera>();
            uiCamera.transform.SetParent(transform);

            // Add PhysicsRaycaster so that the pointer events are sent to 3d objects
            raycaster = uiCamera.gameObject.AddComponent<PhysicsRaycaster>();
            uiCamera.clearFlags = CameraClearFlags.Nothing;
            uiCamera.enabled = false;
            uiCamera.fieldOfView = 5;
            uiCamera.nearClipPlane = 0.01f;

            // Find all canvases in the scene and assign them to our custom camera
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                canvas.worldCamera = uiCamera;
            }
        }

        public void AddController(VrControllerInput _controller)
        {
            if(!controllerData.ContainsKey(_controller))
            {
                controllerData.Add(_controller, new UiControllerData());
            }
        }

        public void RemoveController(VrControllerInput _controller)
        {
            if(controllerData.ContainsKey(_controller))
            {
                controllerData.Remove(_controller);
            }
        }

        public void ClearSelection()
        {
            if(eventSystem.currentSelectedGameObject != null)
            {
                eventSystem.SetSelectedGameObject(null);
            }
        }

        protected void UpdateCameraPosition(VrControllerInput _controller)
        {
            uiCamera.transform.position = _controller.transform.position;
            uiCamera.transform.rotation = _controller.transform.rotation;
        }

        private void Select(GameObject _go)
        {
            ClearSelection();

            if(ExecuteEvents.GetEventHandler<ISelectHandler>(_go) != null)
            {
                eventSystem.SetSelectedGameObject(_go);
            }
        }

        private void UpdateEventData(UiControllerData _data, VrControllerInput _input)
        {
            if(_data.eventData == null)
            {
                _data.eventData = new VrPointerEventData(eventSystem);
            }
            else
            {
                _data.eventData.Reset();
            }

            _data.eventData.controller = _input;
            _data.eventData.delta = Vector2.zero;
            _data.eventData.position = new Vector2(uiCamera.pixelWidth * 0.5F, uiCamera.pixelHeight * 0.5f);
        }

        private void Raycast(UiControllerData _data)
        {
            eventSystem.RaycastAll(_data.eventData, m_RaycastResultCache);
            _data.eventData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            m_RaycastResultCache.Clear();
        }

        public override void Process()
        {
            raycaster.eventMask = layerMask;

            // Loop through all the controller data that exists
            foreach (KeyValuePair<VrControllerInput, UiControllerData> dataPair in controllerData)
            {
                // Copy the values out of the pair for easier accessing
                VrControllerInput controllerInput = dataPair.Key;
                UiControllerData data = dataPair.Value;

                // Handle updating the camera position, event data and raycasting
                UpdateCameraPosition(controllerInput);
                UpdateEventData(data, controllerInput);
                Raycast(data);

                // Set the hit GUI element
                data.currentPoint = data.eventData.pointerCurrentRaycast.gameObject;

                // Handle the entering and exiting of GUI elements
                HandlePointerExitAndEnter(data.eventData, data.currentPoint);

                // Handle the pointer down and up states
                if(controllerInput.IsInteractUIDown)
                {
                    ProcessPress(data, controllerInput);
                }

                if(controllerInput.IsInteractUIUp)
                {
                    ProcessRelease(data, controllerInput);
                }

                // Handle dragging and selection
                ProcessDrag(data, controllerInput);
                ProcessSelect(data);
            }
        }

        private void ProcessPress(UiControllerData _data, VrControllerInput _input)
        {
            ClearSelection();

            // Setup the pointer pressing components
            _data.eventData.pressPosition = _data.eventData.position;
            _data.eventData.pointerPressRaycast = _data.eventData.pointerCurrentRaycast;
            _data.eventData.pointerPress = null;

            // Update the current pressed if the cursor is over an element
            if(_data.currentPoint != null)
            {
                // Assign the current pressed and the event data current to the hovered element
                _data.currentPressed = _data.currentPoint;
                _data.eventData.current = _data.currentPressed;

                // Get the pressed object from the event system
                GameObject newPressed = ExecuteEvents.ExecuteHierarchy(_data.currentPressed, _data.eventData, ExecuteEvents.pointerDownHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.pointerDownHandler);

                if(newPressed == null)
                {
                    // Some UI Elements might only have click handler and not pointerdown handler
                    newPressed = ExecuteEvents.ExecuteHierarchy(_data.currentPressed, _data.eventData, ExecuteEvents.pointerClickHandler);
                    ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.pointerClickHandler);

                    // If the new pressed is set, we can update our current pressed object to the new one
                    if(newPressed != null)
                    {
                        _data.currentPressed = newPressed;
                    }
                }
                else
                {
                    _data.currentPressed = newPressed;
                    // Because of headset jitter, we are going to process click handlers at the sametime as down handlers
                    // instead of the up handling like regular mouse input
                    ExecuteEvents.Execute(newPressed, _data.eventData, ExecuteEvents.pointerClickHandler);
                    ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.pointerClickHandler);
                }

                // If the new pressed was found, set the selected object to it
                if(newPressed != null)
                {
                    _data.eventData.pointerPress = newPressed;
                    _data.currentPressed = newPressed;
                    Select(_data.currentPressed);
                }

                // Run the begin drag handling
                ExecuteEvents.Execute(_data.currentPressed, _data.eventData, ExecuteEvents.beginDragHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.beginDragHandler);

                // Assign the drag objects to the pressed ones
                _data.eventData.pointerDrag = _data.currentPressed;
                _data.currentDragging = _data.currentPressed;
            }
        }

        private void ProcessRelease(UiControllerData _data, VrControllerInput _input)
        {
            // Handle end dragging
            if(_data.currentDragging != null)
            {
                _data.eventData.current = _data.currentDragging;
                ExecuteEvents.Execute(_data.currentDragging, _data.eventData, ExecuteEvents.endDragHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.endDragHandler);
                // Handle dropping after dragging
                if(_data.currentPoint != null)
                {
                    ExecuteEvents.Execute(_data.currentPoint, _data.eventData, ExecuteEvents.dropHandler);
                }
                // Reset the drag objects
                _data.eventData.pointerDrag = null;
                _data.currentDragging = null;
            }

            // Handle pointer up
            if(_data.currentPressed != null)
            {
                _data.eventData.current = _data.currentPressed;
                // Execute the pointer up events on the selected object
                ExecuteEvents.Execute(_data.currentPressed, _data.eventData, ExecuteEvents.pointerUpHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.pointerUpHandler);

                // Reset the pressed objects
                _data.eventData.rawPointerPress = null;
                _data.eventData.pointerPress = null;
                _data.currentPressed = null;
            }
        }

        private void ProcessDrag(UiControllerData _data, VrControllerInput _input)
        {
            if(_data.currentDragging != null)
            {
                _data.eventData.current = _data.currentPressed;
                ExecuteEvents.Execute(_data.currentDragging, _data.eventData, ExecuteEvents.dragHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.dragHandler);
            }
        }

        // Update the selected element to allow keyboard focus
        private void ProcessSelect(UiControllerData _data)
        {
            if(eventSystem.currentSelectedGameObject != null)
            {
                _data.eventData.current = eventSystem.currentSelectedGameObject;
                ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, GetBaseEventData(), ExecuteEvents.updateSelectedHandler);
            }
        }
    }
}
