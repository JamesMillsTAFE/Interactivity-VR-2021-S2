//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Actions
    {
        
        private static SteamVR_Action_Boolean p_interaction_framework_Teleport;
        
        private static SteamVR_Action_Pose p_interaction_framework_Pose;
        
        private static SteamVR_Action_Boolean p_interaction_framework_InteractUI;
        
        private static SteamVR_Action_Boolean p_interaction_framework_Grab;
        
        private static SteamVR_Action_Boolean p_interaction_framework_Use;
        
        private static SteamVR_Action_Vector2 p_interaction_framework_TouchpadPos;
        
        private static SteamVR_Action_Single p_interaction_framework_Squeeze;
        
        private static SteamVR_Action_Boolean p_interaction_framework_Pointer;
        
        private static SteamVR_Action_Vibration p_interaction_framework_Haptic;
        
        public static SteamVR_Action_Boolean interaction_framework_Teleport
        {
            get
            {
                return SteamVR_Actions.p_interaction_framework_Teleport.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Pose interaction_framework_Pose
        {
            get
            {
                return SteamVR_Actions.p_interaction_framework_Pose.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        public static SteamVR_Action_Boolean interaction_framework_InteractUI
        {
            get
            {
                return SteamVR_Actions.p_interaction_framework_InteractUI.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean interaction_framework_Grab
        {
            get
            {
                return SteamVR_Actions.p_interaction_framework_Grab.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean interaction_framework_Use
        {
            get
            {
                return SteamVR_Actions.p_interaction_framework_Use.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Vector2 interaction_framework_TouchpadPos
        {
            get
            {
                return SteamVR_Actions.p_interaction_framework_TouchpadPos.GetCopy<SteamVR_Action_Vector2>();
            }
        }
        
        public static SteamVR_Action_Single interaction_framework_Squeeze
        {
            get
            {
                return SteamVR_Actions.p_interaction_framework_Squeeze.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Boolean interaction_framework_Pointer
        {
            get
            {
                return SteamVR_Actions.p_interaction_framework_Pointer.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Vibration interaction_framework_Haptic
        {
            get
            {
                return SteamVR_Actions.p_interaction_framework_Haptic.GetCopy<SteamVR_Action_Vibration>();
            }
        }
        
        private static void InitializeActionArrays()
        {
            Valve.VR.SteamVR_Input.actions = new Valve.VR.SteamVR_Action[] {
                    SteamVR_Actions.interaction_framework_Teleport,
                    SteamVR_Actions.interaction_framework_Pose,
                    SteamVR_Actions.interaction_framework_InteractUI,
                    SteamVR_Actions.interaction_framework_Grab,
                    SteamVR_Actions.interaction_framework_Use,
                    SteamVR_Actions.interaction_framework_TouchpadPos,
                    SteamVR_Actions.interaction_framework_Squeeze,
                    SteamVR_Actions.interaction_framework_Pointer,
                    SteamVR_Actions.interaction_framework_Haptic};
            Valve.VR.SteamVR_Input.actionsIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.interaction_framework_Teleport,
                    SteamVR_Actions.interaction_framework_Pose,
                    SteamVR_Actions.interaction_framework_InteractUI,
                    SteamVR_Actions.interaction_framework_Grab,
                    SteamVR_Actions.interaction_framework_Use,
                    SteamVR_Actions.interaction_framework_TouchpadPos,
                    SteamVR_Actions.interaction_framework_Squeeze,
                    SteamVR_Actions.interaction_framework_Pointer};
            Valve.VR.SteamVR_Input.actionsOut = new Valve.VR.ISteamVR_Action_Out[] {
                    SteamVR_Actions.interaction_framework_Haptic};
            Valve.VR.SteamVR_Input.actionsVibration = new Valve.VR.SteamVR_Action_Vibration[] {
                    SteamVR_Actions.interaction_framework_Haptic};
            Valve.VR.SteamVR_Input.actionsPose = new Valve.VR.SteamVR_Action_Pose[] {
                    SteamVR_Actions.interaction_framework_Pose};
            Valve.VR.SteamVR_Input.actionsBoolean = new Valve.VR.SteamVR_Action_Boolean[] {
                    SteamVR_Actions.interaction_framework_Teleport,
                    SteamVR_Actions.interaction_framework_InteractUI,
                    SteamVR_Actions.interaction_framework_Grab,
                    SteamVR_Actions.interaction_framework_Use,
                    SteamVR_Actions.interaction_framework_Pointer};
            Valve.VR.SteamVR_Input.actionsSingle = new Valve.VR.SteamVR_Action_Single[] {
                    SteamVR_Actions.interaction_framework_Squeeze};
            Valve.VR.SteamVR_Input.actionsVector2 = new Valve.VR.SteamVR_Action_Vector2[] {
                    SteamVR_Actions.interaction_framework_TouchpadPos};
            Valve.VR.SteamVR_Input.actionsVector3 = new Valve.VR.SteamVR_Action_Vector3[0];
            Valve.VR.SteamVR_Input.actionsSkeleton = new Valve.VR.SteamVR_Action_Skeleton[0];
            Valve.VR.SteamVR_Input.actionsNonPoseNonSkeletonIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.interaction_framework_Teleport,
                    SteamVR_Actions.interaction_framework_InteractUI,
                    SteamVR_Actions.interaction_framework_Grab,
                    SteamVR_Actions.interaction_framework_Use,
                    SteamVR_Actions.interaction_framework_TouchpadPos,
                    SteamVR_Actions.interaction_framework_Squeeze,
                    SteamVR_Actions.interaction_framework_Pointer};
        }
        
        private static void PreInitActions()
        {
            SteamVR_Actions.p_interaction_framework_Teleport = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/interaction_framework/in/Teleport")));
            SteamVR_Actions.p_interaction_framework_Pose = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/interaction_framework/in/Pose")));
            SteamVR_Actions.p_interaction_framework_InteractUI = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/interaction_framework/in/InteractUI")));
            SteamVR_Actions.p_interaction_framework_Grab = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/interaction_framework/in/Grab")));
            SteamVR_Actions.p_interaction_framework_Use = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/interaction_framework/in/Use")));
            SteamVR_Actions.p_interaction_framework_TouchpadPos = ((SteamVR_Action_Vector2)(SteamVR_Action.Create<SteamVR_Action_Vector2>("/actions/interaction_framework/in/TouchpadPos")));
            SteamVR_Actions.p_interaction_framework_Squeeze = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/interaction_framework/in/Squeeze")));
            SteamVR_Actions.p_interaction_framework_Pointer = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/interaction_framework/in/Pointer")));
            SteamVR_Actions.p_interaction_framework_Haptic = ((SteamVR_Action_Vibration)(SteamVR_Action.Create<SteamVR_Action_Vibration>("/actions/interaction_framework/out/Haptic")));
        }
    }
}
