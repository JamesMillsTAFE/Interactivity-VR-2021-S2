using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace VRFramework.Interactions
{
    // This is the component that the objects that will be able to have some sort of interactions
    // on them. This controls whether or not they can have certain interactions, and also the physics
    // components attached to them.
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableObject : MonoBehaviour
    {
        public bool canGrab = true;
        // Whilst the grab button is pressed, the object will stay grabbed. If false, it will require you to
        // press the grab button again to release the object
        public bool holdButtonToGrab = true;
        // This defines which controllers can grab the object, if any is set, all controllers will be able to grab it
        public SteamVR_Input_Sources allowedGrabControllers = SteamVR_Input_Sources.Any;

        public bool canTouch = false;
        public SteamVR_Input_Sources allowedTouchControllers = SteamVR_Input_Sources.Any;

        public bool canUse = false;
        public SteamVR_Input_Sources allowedUseControllers = SteamVR_Input_Sources.Any;

        private new Rigidbody rigidbody;
        private new Collider collider;

        // Start is called before the first frame update
        void Start()
        {
            // Get the rigidbody component from the object
            rigidbody = gameObject.GetComponent<Rigidbody>();

            // Try and get a Collider from the object, if it has none, add a BoxCollider
            collider = gameObject.GetComponent<Collider>();
            if(collider == null)
                collider = gameObject.AddComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    } 
}
