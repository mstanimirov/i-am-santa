// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""b5628568-a2a0-4968-ad80-39622fe72741"",
            ""actions"": [
                {
                    ""name"": ""ActionP"",
                    ""type"": ""Button"",
                    ""id"": ""ea8288a3-6139-4947-8a05-a11e3c3db86e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionS"",
                    ""type"": ""Button"",
                    ""id"": ""4235930f-7125-4f94-b83d-c7ce15961931"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""663b134f-e78a-4826-8ad4-879f2192912e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""ActionP"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c7f04d8-df31-4502-8fff-a3c0e88d81d8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""ActionS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_ActionP = m_Gameplay.FindAction("ActionP", throwIfNotFound: true);
        m_Gameplay_ActionS = m_Gameplay.FindAction("ActionS", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_ActionP;
    private readonly InputAction m_Gameplay_ActionS;
    public struct GameplayActions
    {
        private @InputMaster m_Wrapper;
        public GameplayActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @ActionP => m_Wrapper.m_Gameplay_ActionP;
        public InputAction @ActionS => m_Wrapper.m_Gameplay_ActionS;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @ActionP.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionP;
                @ActionP.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionP;
                @ActionP.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionP;
                @ActionS.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionS;
                @ActionS.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionS;
                @ActionS.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionS;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ActionP.started += instance.OnActionP;
                @ActionP.performed += instance.OnActionP;
                @ActionP.canceled += instance.OnActionP;
                @ActionS.started += instance.OnActionS;
                @ActionS.performed += instance.OnActionS;
                @ActionS.canceled += instance.OnActionS;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnActionP(InputAction.CallbackContext context);
        void OnActionS(InputAction.CallbackContext context);
    }
}
