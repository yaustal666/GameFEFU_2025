using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : ScriptableObject, InputSystemActions.IPlayerActions, InputSystemActions.IUIActions  {

    private InputSystemActions _inputActions;

    private void OnEnable() {
        if (_inputActions == null) { 
            _inputActions = new InputSystemActions();

            _inputActions.Player.SetCallbacks(this);
            _inputActions.UI.SetCallbacks(this);

            SetPlayerActions();
        }
    }

    public event Action<Vector2> MoveEvent;
    public event Action AttackEvent;
    public Action JumpEvent;

    public void SetPlayerActions() {
        _inputActions.Player.Enable();
        _inputActions.UI.Disable();
    }

    public void SetUIActions() {
        _inputActions.UI.Enable();
        _inputActions.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context) {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnCancel(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnMiddleClick(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnNavigate(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnPoint(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }
}