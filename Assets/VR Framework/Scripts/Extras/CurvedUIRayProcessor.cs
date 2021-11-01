using UnityEngine;
using VRFramework.Input;

namespace VRFramework.Extras
{
    public class CurvedUIRayProcessor : MonoBehaviour
    {
        public VrController leftController;
        public VrController rightController;
        public bool useRight = true;

        // Update is called once per frame
        private void Update()
        {
            //Ternary operator... similar to a one line if statement that can be
            //used to set things.
            // ? = if
            // : = else
            VrControllerInput input = useRight ? rightController.Input : leftController.Input;
            CurvedUIInputModule.CustomControllerRay = new Ray(
                input.transform.position, 
                input.transform.forward);
            CurvedUIInputModule.CustomControllerButtonState = input.IsInteractUIPressed;
        }
    }
}