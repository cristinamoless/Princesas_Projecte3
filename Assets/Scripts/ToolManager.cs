using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static bool scissorsActive = false;
    public static bool deleteActive = false;


    public GameObject scissorsCursor;
    public GameObject handCursor;
    public GameObject deleteCursor;

    void Start()
    {
        Cursor.visible = false;
        scissorsActive = false;
        handCursor.SetActive(true);
        scissorsCursor.SetActive(false);
        deleteCursor.SetActive(false);
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (scissorsActive)
        {
            scissorsCursor.SetActive(true);
            handCursor.SetActive(false);
            deleteCursor.SetActive(false);
            scissorsCursor.transform.position = mousePos;
        }
        else if (deleteActive)
        {
            scissorsCursor.SetActive(false);
            handCursor.SetActive(false);
            deleteCursor.SetActive(true);
            deleteCursor.transform.position = mousePos;
        }
        else
        {
            scissorsCursor.SetActive(false);
            handCursor.SetActive(true);
            deleteCursor.SetActive(false);

            handCursor.transform.position = mousePos;
        }
    }

    public void ActivateScissors()
    {
        scissorsActive = true;
        deleteActive = false;
    }

    public void ActivateHand()
    {
        scissorsActive = false;
        deleteActive = false;
    }
    public void ActivateDelete()
    {
        deleteActive = true;
        scissorsActive = false;
    }
}
