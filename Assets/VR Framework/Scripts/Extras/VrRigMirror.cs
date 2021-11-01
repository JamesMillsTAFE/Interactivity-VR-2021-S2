using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFramework.Extras
{
    public class VrRigMirror : MonoBehaviour
    {
        [System.Serializable]
        public class FakeTransformReference
        {
            public Transform reference;
            public Transform controlled;
            public Vector3 positionOffset;
            public Vector3 rotationOffset;

            public void Apply()
            {
                controlled.position = reference.position + positionOffset;
                controlled.eulerAngles = reference.eulerAngles + rotationOffset;
            }
        }

        public FakeTransformReference leftHand;
        public FakeTransformReference rightHand;
        public FakeTransformReference headset;

        // Update is called once per frame
        private void Update()
        {
            if(leftHand != null)
                leftHand.Apply();
            
            if(rightHand != null)
                rightHand.Apply();
            
            if(headset != null)
                headset.Apply();
        }
    } 
}
