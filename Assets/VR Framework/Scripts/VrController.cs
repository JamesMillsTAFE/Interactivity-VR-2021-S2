using UnityEngine;
using VRFramework.Input;
using Valve.VR;

namespace VRFramework
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(VrControllerInput))]
    [RequireComponent(typeof(SteamVR_Behaviour_Pose))]
    public class VrController : MonoBehaviour
    {
        public VrControllerInput Input
        {
            get;
            private set;
        }

        public Rigidbody Rigidbody
        {
            get;
            private set;
        }

        public Collider Collider
        {
            get;
            private set;
        }

        public SteamVR_Behaviour_Pose Pose
        {
            get;
            private set;
        }

        public SteamVR_Input_Sources Source
        {
            get;
            private set;
        }

        public Vector3 Velocity
        {
            get;
            private set;
        }

        public Vector3 AngularVelocity
        {
            get;
            private set;
        }

        public void Setup()
        {
            // Get the rigidbody component from the gameobject
            Rigidbody = gameObject.GetComponent<Rigidbody>();

            // Attempt to get the collider component from the gameObject, if we fail, add a spherecollider
            Collider = gameObject.GetComponent<Collider>();
            if (Collider == null)
                Collider = gameObject.AddComponent<SphereCollider>();

            // Setup the collider and the rigidbody in the way we need them
            Collider.isTrigger = true;
            Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            Rigidbody.useGravity = false;

            // Get the BehaviourPose component and it's InputSource
            Pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
            Source = Pose.inputSource;

            // Get the controllerinput component from the gameObject and set it up
            Input = gameObject.GetComponent<VrControllerInput>();
            Input.Setup(this);
        }

        public void Process()
        {
            // Get the velocity data from the pose
            Velocity = Pose.GetVelocity();
            AngularVelocity = Pose.GetAngularVelocity();

            Input.Process();
        }
    } 
}
