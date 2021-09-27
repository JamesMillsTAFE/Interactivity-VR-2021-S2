using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace VRFramework
{
    public class VrRig : MonoBehaviour
    {
        [SerializeField] private new Transform camera;
        [SerializeField] private Transform leftController;
        [SerializeField] private Transform rightController;

        private VrController left;
        private VrController right;

        private static VrRig instance = null;

        /// <summary>
        /// Attempts to get the transform for the passed source that is being tracked by our system.
        /// </summary>
        /// <param name="_source">The source being requested.</param>
        public static Transform GetTrackedTransform(SteamVR_Input_Sources _source)
        {
            switch (_source)
            {
                case SteamVR_Input_Sources.LeftHand: return instance.leftController;
                case SteamVR_Input_Sources.RightHand: return instance.rightController;
                case SteamVR_Input_Sources.LeftFoot:
                    break;
                case SteamVR_Input_Sources.RightFoot:
                    break;
                case SteamVR_Input_Sources.Head: return instance.camera;
            }

            // The passed source isn't tracked by our system, so return null
            return null;
        }

        /// <summary>
        /// Attempts to get the controller associated with the passed in source.
        /// </summary>
        /// <param name="_source">The source we are attempting to get the controller for.</param>
        public static VrController GetController(SteamVR_Input_Sources _source)
        {
            if(_source == SteamVR_Input_Sources.LeftHand)
            {
                return instance.left;
            }
            else if(_source == SteamVR_Input_Sources.RightHand)
            {
                return instance.right;
            }

            // The source wasn't a valid controller source, so return null
            return null;
        }

        private void Awake()
        {
            // If the instance hasn't been set, assign it to this component
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                // The instance has already been set and it isn't this object, so destroy this object and ignore the future code
                Destroy(gameObject);
                return;
            }

            // Get the controller components from the left and right controllers so that we can access them later
            left = leftController.GetComponent<VrController>();
            right = rightController.GetComponent<VrController>();

            // Setup the two controller components
            left.Setup();
            right.Setup();
        }

        // Update is called once per frame
        void Update()
        {
            // Process the update loop for the controllers
            left.Process();
            right.Process();
        }
    } 
}
