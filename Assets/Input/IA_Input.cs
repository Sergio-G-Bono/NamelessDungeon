//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input/IA_Input.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @IA_Input: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @IA_Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""IA_Input"",
    ""maps"": [
        {
            ""name"": ""AM_CharControl"",
            ""id"": ""93bee6e3-c43a-4416-8785-c861c0c4def9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""25c07348-ec3a-484f-91d6-8854418f92a2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""db25a51f-9ac7-4f38-8b19-325c5ee19c10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PointMouseMov"",
                    ""type"": ""Button"",
                    ""id"": ""f9e44182-2a17-491e-9125-f1d495e0307d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""569b7185-1edc-4852-b048-0d6f4b8a490e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d69cd836-cea0-4c8a-9924-5ea43a2b79bd"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d919e7a6-d3cb-4fbf-a202-508e5538152e"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d2f4104-d6a8-496b-9288-070430376896"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PointMouseMov"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // AM_CharControl
        m_AM_CharControl = asset.FindActionMap("AM_CharControl", throwIfNotFound: true);
        m_AM_CharControl_Move = m_AM_CharControl.FindAction("Move", throwIfNotFound: true);
        m_AM_CharControl_Run = m_AM_CharControl.FindAction("Run", throwIfNotFound: true);
        m_AM_CharControl_PointMouseMov = m_AM_CharControl.FindAction("PointMouseMov", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // AM_CharControl
    private readonly InputActionMap m_AM_CharControl;
    private List<IAM_CharControlActions> m_AM_CharControlActionsCallbackInterfaces = new List<IAM_CharControlActions>();
    private readonly InputAction m_AM_CharControl_Move;
    private readonly InputAction m_AM_CharControl_Run;
    private readonly InputAction m_AM_CharControl_PointMouseMov;
    public struct AM_CharControlActions
    {
        private @IA_Input m_Wrapper;
        public AM_CharControlActions(@IA_Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_AM_CharControl_Move;
        public InputAction @Run => m_Wrapper.m_AM_CharControl_Run;
        public InputAction @PointMouseMov => m_Wrapper.m_AM_CharControl_PointMouseMov;
        public InputActionMap Get() { return m_Wrapper.m_AM_CharControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AM_CharControlActions set) { return set.Get(); }
        public void AddCallbacks(IAM_CharControlActions instance)
        {
            if (instance == null || m_Wrapper.m_AM_CharControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_AM_CharControlActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @PointMouseMov.started += instance.OnPointMouseMov;
            @PointMouseMov.performed += instance.OnPointMouseMov;
            @PointMouseMov.canceled += instance.OnPointMouseMov;
        }

        private void UnregisterCallbacks(IAM_CharControlActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @PointMouseMov.started -= instance.OnPointMouseMov;
            @PointMouseMov.performed -= instance.OnPointMouseMov;
            @PointMouseMov.canceled -= instance.OnPointMouseMov;
        }

        public void RemoveCallbacks(IAM_CharControlActions instance)
        {
            if (m_Wrapper.m_AM_CharControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IAM_CharControlActions instance)
        {
            foreach (var item in m_Wrapper.m_AM_CharControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_AM_CharControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public AM_CharControlActions @AM_CharControl => new AM_CharControlActions(this);
    public interface IAM_CharControlActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnPointMouseMov(InputAction.CallbackContext context);
    }
}
