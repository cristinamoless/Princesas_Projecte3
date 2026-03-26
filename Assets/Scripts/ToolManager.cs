using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static bool handActive = true;
    public static bool scissorsActive = false;
    public static bool rotateActive = false;
    public static bool deleteActive = false;

    public GameObject handCursor;
    public GameObject handClosedCursor;
    public GameObject scissorsCursor;
    public GameObject rotateCursor;
    public GameObject deleteCursor;

    void Start()
    {
        Cursor.visible = false;
        handCursor.SetActive(true);
        scissorsCursor.SetActive(false);
        rotateCursor.SetActive(false);
        deleteCursor.SetActive(false);
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (handActive)
        {
            bool isClicking = Input.GetMouseButton(0);

            handCursor.SetActive(!isClicking);
            handClosedCursor.SetActive(isClicking);

            if (isClicking)
                handClosedCursor.transform.position = mousePos;
            else
                handCursor.transform.position = mousePos;

            scissorsCursor.SetActive(false);
            rotateCursor.SetActive(false);
            deleteCursor.SetActive(false);
            handCursor.transform.position = mousePos;
        }
        else if (scissorsActive)
        {
            handCursor.SetActive(false);
            scissorsCursor.SetActive(true);
            rotateCursor.SetActive(false);
            deleteCursor.SetActive(false);
            scissorsCursor.transform.position = mousePos;
        }
        else if (rotateActive)
        {
            handCursor.SetActive(false);
            scissorsCursor.SetActive(false);
            rotateCursor.SetActive(true);
            deleteCursor.SetActive(false);
            rotateCursor.transform.position = mousePos;
        }
        else if (deleteActive)
        {
            handCursor.SetActive(false);
            scissorsCursor.SetActive(false);
            rotateCursor.SetActive(false);
            deleteCursor.SetActive(true);
            deleteCursor.transform.position = mousePos;
        }
    }

    public void ActivateHand()
    {
        handActive = true;
        scissorsActive = false;
        rotateActive = false;
        deleteActive = false;
    }
    public void ActivateScissors()
    {
        handActive = false;
        scissorsActive = true;
        rotateActive = false;
        deleteActive = false;
    }
    public void ActivateRotate()
    {
        handActive = false;
        scissorsActive = false;
        rotateActive = true;
        deleteActive = false;
    }
    public void ActivateDelete()
    {
        handActive = false;
        scissorsActive = false;
        rotateActive = false;
        deleteActive = true;
    }
}
