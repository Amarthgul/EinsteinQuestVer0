//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Inputs/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""8580997b-dc8a-40e9-9622-d5aa649c5353"",
            ""actions"": [
                {
                    ""name"": ""JoystickMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0093e3c6-5429-4568-96f6-d7911af653e9"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""KeyboardMovement"",
                    ""type"": ""Button"",
                    ""id"": ""c0048adf-145a-4858-83ca-fa7aab96c591"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""ef69a9fa-13e8-46a3-b84a-2c94e5c13f23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""33bea155-45b9-4c8e-a93b-f83b6dc9c99a"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoystickMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96223719-3ef8-49b4-a6b5-361af0a08457"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc0714d3-59d2-45a6-a264-ad9e6afbebf0"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Global"",
            ""id"": ""7b896438-0929-4b37-83e2-cf09b012c185"",
            ""actions"": [
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""4084f1eb-0901-4f85-8c04-c54107141e4c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""25bde512-ecf8-4dce-a046-a23461c5bb40"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_JoystickMovement = m_Player1.FindAction("JoystickMovement", throwIfNotFound: true);
        m_Player1_KeyboardMovement = m_Player1.FindAction("KeyboardMovement", throwIfNotFound: true);
        m_Player1_Newaction = m_Player1.FindAction("New action", throwIfNotFound: true);
        // Global
        m_Global = asset.FindActionMap("Global", throwIfNotFound: true);
        m_Global_Quit = m_Global.FindAction("Quit", throwIfNotFound: true);
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

    // Player1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_JoystickMovement;
    private readonly InputAction m_Player1_KeyboardMovement;
    private readonly InputAction m_Player1_Newaction;
    public struct Player1Actions
    {
        private @PlayerInput m_Wrapper;
        public Player1Actions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @JoystickMovement => m_Wrapper.m_Player1_JoystickMovement;
        public InputAction @KeyboardMovement => m_Wrapper.m_Player1_KeyboardMovement;
        public InputAction @Newaction => m_Wrapper.m_Player1_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @JoystickMovement.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJoystickMovement;
                @JoystickMovement.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJoystickMovement;
                @JoystickMovement.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJoystickMovement;
                @KeyboardMovement.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnKeyboardMovement;
                @KeyboardMovement.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnKeyboardMovement;
                @KeyboardMovement.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnKeyboardMovement;
                @Newaction.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @JoystickMovement.started += instance.OnJoystickMovement;
                @JoystickMovement.performed += instance.OnJoystickMovement;
                @JoystickMovement.canceled += instance.OnJoystickMovement;
                @KeyboardMovement.started += instance.OnKeyboardMovement;
                @KeyboardMovement.performed += instance.OnKeyboardMovement;
                @KeyboardMovement.canceled += instance.OnKeyboardMovement;
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Global
    private readonly InputActionMap m_Global;
    private IGlobalActions m_GlobalActionsCallbackInterface;
    private readonly InputAction m_Global_Quit;
    public struct GlobalActions
    {
        private @PlayerInput m_Wrapper;
        public GlobalActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Quit => m_Wrapper.m_Global_Quit;
        public InputActionMap Get() { return m_Wrapper.m_Global; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GlobalActions set) { return set.Get(); }
        public void SetCallbacks(IGlobalActions instance)
        {
            if (m_Wrapper.m_GlobalActionsCallbackInterface != null)
            {
                @Quit.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnQuit;
            }
            m_Wrapper.m_GlobalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
            }
        }
    }
    public GlobalActions @Global => new GlobalActions(this);
    public interface IPlayer1Actions
    {
        void OnJoystickMovement(InputAction.CallbackContext context);
        void OnKeyboardMovement(InputAction.CallbackContext context);
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IGlobalActions
    {
        void OnQuit(InputAction.CallbackContext context);
    }
}
