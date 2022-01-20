// GENERATED AUTOMATICALLY FROM 'Assets/Input Controls/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""GroundMovement"",
            ""id"": ""3dcc7b35-5f40-4650-89d9-7fadbeca449d"",
            ""actions"": [
                {
                    ""name"": ""Walking"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bce3c6d2-ac43-43ec-8e26-d7c2c6a40b6a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7fc5ee8b-42da-43e1-820d-286b244f22fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8c0f13b5-78ce-4919-8ceb-91d9fb058d2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""477a87c3-0de7-4776-ac8e-837d6346d427"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""aa2e7a07-5358-4389-bc4d-88967a3d8bb3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walking"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1843be14-0f5f-47cb-b77a-be963ca24bce"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""19325787-a47b-46f5-9dbb-41109ce0f752"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a63a6901-eefb-4477-b6a0-aa069cc8d1eb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6f0f21ef-62e5-4ef0-8dcd-f3ebeae5a23f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""dfc2190f-0aae-4bba-b9f9-00d85178fcf6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38ecf0e5-41de-4584-a10e-21a7b22cc1fe"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8a6e7a2-775e-4bfb-9c38-b17d8d5aa337"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MouseMovement"",
            ""id"": ""9624cf4d-45f8-46ca-96cb-62c20b60c4be"",
            ""actions"": [
                {
                    ""name"": ""Mouse X"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c35c7ffc-af1e-4c11-abb1-8fbd99205955"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Y"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8f1626b5-b968-4182-bd0e-5f8c17f3695a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0ba7fc09-a1fb-4990-95b8-c79c6bc29a82"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b15773d-bd09-4614-bae6-4136836734bd"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GroundMovement
        m_GroundMovement = asset.FindActionMap("GroundMovement", throwIfNotFound: true);
        m_GroundMovement_Walking = m_GroundMovement.FindAction("Walking", throwIfNotFound: true);
        m_GroundMovement_Jump = m_GroundMovement.FindAction("Jump", throwIfNotFound: true);
        m_GroundMovement_Sprint = m_GroundMovement.FindAction("Sprint", throwIfNotFound: true);
        m_GroundMovement_Interact = m_GroundMovement.FindAction("Interact", throwIfNotFound: true);
        // MouseMovement
        m_MouseMovement = asset.FindActionMap("MouseMovement", throwIfNotFound: true);
        m_MouseMovement_MouseX = m_MouseMovement.FindAction("Mouse X", throwIfNotFound: true);
        m_MouseMovement_MouseY = m_MouseMovement.FindAction("Mouse Y", throwIfNotFound: true);
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

    // GroundMovement
    private readonly InputActionMap m_GroundMovement;
    private IGroundMovementActions m_GroundMovementActionsCallbackInterface;
    private readonly InputAction m_GroundMovement_Walking;
    private readonly InputAction m_GroundMovement_Jump;
    private readonly InputAction m_GroundMovement_Sprint;
    private readonly InputAction m_GroundMovement_Interact;
    public struct GroundMovementActions
    {
        private @PlayerControls m_Wrapper;
        public GroundMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Walking => m_Wrapper.m_GroundMovement_Walking;
        public InputAction @Jump => m_Wrapper.m_GroundMovement_Jump;
        public InputAction @Sprint => m_Wrapper.m_GroundMovement_Sprint;
        public InputAction @Interact => m_Wrapper.m_GroundMovement_Interact;
        public InputActionMap Get() { return m_Wrapper.m_GroundMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GroundMovementActions set) { return set.Get(); }
        public void SetCallbacks(IGroundMovementActions instance)
        {
            if (m_Wrapper.m_GroundMovementActionsCallbackInterface != null)
            {
                @Walking.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnWalking;
                @Walking.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnWalking;
                @Walking.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnWalking;
                @Jump.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnSprint;
                @Interact.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_GroundMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Walking.started += instance.OnWalking;
                @Walking.performed += instance.OnWalking;
                @Walking.canceled += instance.OnWalking;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public GroundMovementActions @GroundMovement => new GroundMovementActions(this);

    // MouseMovement
    private readonly InputActionMap m_MouseMovement;
    private IMouseMovementActions m_MouseMovementActionsCallbackInterface;
    private readonly InputAction m_MouseMovement_MouseX;
    private readonly InputAction m_MouseMovement_MouseY;
    public struct MouseMovementActions
    {
        private @PlayerControls m_Wrapper;
        public MouseMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseX => m_Wrapper.m_MouseMovement_MouseX;
        public InputAction @MouseY => m_Wrapper.m_MouseMovement_MouseY;
        public InputActionMap Get() { return m_Wrapper.m_MouseMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseMovementActions set) { return set.Get(); }
        public void SetCallbacks(IMouseMovementActions instance)
        {
            if (m_Wrapper.m_MouseMovementActionsCallbackInterface != null)
            {
                @MouseX.started -= m_Wrapper.m_MouseMovementActionsCallbackInterface.OnMouseX;
                @MouseX.performed -= m_Wrapper.m_MouseMovementActionsCallbackInterface.OnMouseX;
                @MouseX.canceled -= m_Wrapper.m_MouseMovementActionsCallbackInterface.OnMouseX;
                @MouseY.started -= m_Wrapper.m_MouseMovementActionsCallbackInterface.OnMouseY;
                @MouseY.performed -= m_Wrapper.m_MouseMovementActionsCallbackInterface.OnMouseY;
                @MouseY.canceled -= m_Wrapper.m_MouseMovementActionsCallbackInterface.OnMouseY;
            }
            m_Wrapper.m_MouseMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseX.started += instance.OnMouseX;
                @MouseX.performed += instance.OnMouseX;
                @MouseX.canceled += instance.OnMouseX;
                @MouseY.started += instance.OnMouseY;
                @MouseY.performed += instance.OnMouseY;
                @MouseY.canceled += instance.OnMouseY;
            }
        }
    }
    public MouseMovementActions @MouseMovement => new MouseMovementActions(this);
    public interface IGroundMovementActions
    {
        void OnWalking(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IMouseMovementActions
    {
        void OnMouseX(InputAction.CallbackContext context);
        void OnMouseY(InputAction.CallbackContext context);
    }
}
