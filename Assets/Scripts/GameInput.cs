using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private GameInputAction gameInputAction;
    public event EventHandler OnMenuButtonPressed;
    private void Awake()
    {
        Instance = this;
        gameInputAction = new GameInputAction();
        gameInputAction.Enable();
        gameInputAction.Lander.Menu.performed += Menu_performed;
    }

    private void Menu_performed(InputAction.CallbackContext context)
    {
        OnMenuButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public bool UpActionPressed()
    {
        return gameInputAction.Lander.Up.IsPressed();
    }
    public bool LeftActionPressed()
    {
        return gameInputAction.Lander.Left.IsPressed();
    }
    public bool RightActionPressed()
    {
        return gameInputAction.Lander.Right.IsPressed();
    }

}
