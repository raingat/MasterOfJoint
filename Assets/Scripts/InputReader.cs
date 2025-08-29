using UnityEngine;

public class InputReader
{
    private const int NumberLeftMouseButton = 0;
    private const int NumberRightMouseButton = 1;
    private const KeyCode ReloadButton = KeyCode.Q;

    public bool IsLeftMouseButton()
    {
        return Input.GetMouseButtonDown(NumberLeftMouseButton);
    }

    public bool IsRightMouseButton()
    {
        return Input.GetMouseButtonDown(NumberRightMouseButton);
    }

    public bool IsReloadButton()
    {
        return Input.GetKeyDown(ReloadButton);
    }
}
