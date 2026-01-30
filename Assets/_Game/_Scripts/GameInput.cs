using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction m_playerInputAction;

    public event EventHandler OnSwapClicked;

    private void Awake()
    {
        m_playerInputAction = new PlayerInputAction();
        m_playerInputAction.Enable();
        m_playerInputAction.Player.Swap.performed += Swap_performed;

    }

    private void Swap_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSwapClicked?.Invoke(this, EventArgs.Empty);
    }
}
