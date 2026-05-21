using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public enum ToolType
    {
        Hand,
        Scissors,
        Rotate,
        Delete
    }

    public static ToolType activeTool = ToolType.Hand;

    public Texture2D handCursor;
    public Texture2D handClosedCursor;
    public Texture2D scissorsCursor;
    public Texture2D scissorsClosedCursor;
    public Texture2D rotateCursor;
    public Texture2D deleteCursor;

    public Vector2 hotspot = Vector2.zero;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SetCursor(ToolType.Hand);
    }

    void Update()
    {
        bool clicking = Input.GetMouseButton(0);

        switch (activeTool)
        {
            case ToolType.Hand:
                Cursor.SetCursor(clicking ? handClosedCursor : handCursor, hotspot, CursorMode.Auto);
                break;

            case ToolType.Scissors:
                Cursor.SetCursor(clicking ? scissorsClosedCursor : scissorsCursor, hotspot, CursorMode.Auto);
                break;

            case ToolType.Rotate:
                Cursor.SetCursor(rotateCursor, hotspot, CursorMode.Auto);
                break;

            case ToolType.Delete:
                Cursor.SetCursor(deleteCursor, hotspot, CursorMode.Auto);
                break;
        }
    }
    public void SetCursor(ToolType tool)
    {
        activeTool = tool;

        switch (tool)
        {
            case ToolType.Hand:
                Cursor.SetCursor(handCursor, hotspot, CursorMode.Auto);
                break;

            case ToolType.Scissors:
                Cursor.SetCursor(scissorsCursor, hotspot, CursorMode.Auto);
                break;

            case ToolType.Rotate:
                Cursor.SetCursor(rotateCursor, hotspot, CursorMode.Auto);
                break;

            case ToolType.Delete:
                Cursor.SetCursor(deleteCursor, hotspot, CursorMode.Auto);
                break;
        }
    }

    public void ActivateHand() {
        SetCursor(ToolType.Hand);
    }
    public void ActivateScissors() {
        SetCursor(ToolType.Scissors);
    }
    public void ActivateRotate() {
        SetCursor(ToolType.Rotate);
    }
    public void ActivateDelete() {
        SetCursor(ToolType.Delete);
    }

}
