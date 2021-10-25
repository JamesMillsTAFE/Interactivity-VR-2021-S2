using UnityEngine;
using UnityEngine.XR;

using System.Collections.Generic;

namespace VRFramework
{
    public static class XrUtility
    {
        private static List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();

        /// <summary> The variable that dictates if VR/AR are enabled or allows us to set the enabled state </summary>
        public static bool XREnabled
        {
            get
            {
                // Get all connected XR Devices
                SubsystemManager.GetInstances(subsystems);

                // Loop through all XR Devices
                foreach (XRInputSubsystem subsystem in subsystems)
                {
                    // Check if the XR Device is active, if so return true
                    if(subsystem.running)
                    {
                        return true;
                    }
                }

                // No Active XR Device
                return false;
            }

            set
            {
                // Get all connected XR Devices
                SubsystemManager.GetInstances(subsystems);

                // Loop through all XR Devices
                foreach (XRInputSubsystem subsystem in subsystems)
                {
                    // If we want to enable it, start it, otherwise stop it
                    if(value)
                    {
                        subsystem.Start();
                    }
                    else
                    {
                        subsystem.Stop();
                    }
                }
            }
        }
    } 
}
