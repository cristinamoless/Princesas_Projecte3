using UnityEngine;

public class TableManager : MonoBehaviour
{
    public Transform tableArea;

    public void RemoveLeavesFromTable()
    {
        Flower[] flowers = tableArea.GetComponentsInChildren<Flower>();

        foreach (Flower f in flowers)
        {
            f.RemoveLeaves();
        }
    }
}