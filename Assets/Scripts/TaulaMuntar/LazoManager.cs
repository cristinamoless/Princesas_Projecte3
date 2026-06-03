using UnityEngine;

public class LazoManager : MonoBehaviour
{
    public RectTransform tableArea;
    public GameObject colorPanel;

    private GameObject currentBow;

    public void CreateBow(GameObject bowPrefab)
    {
        if (currentBow != null)
            Destroy(currentBow);

        currentBow = Instantiate(bowPrefab, tableArea);

        currentBow.AddComponent<EditBow>();
        currentBow.AddComponent<DragDrop>();

        colorPanel.SetActive(false);
        TutorialManager.Instance.OnChooseBow();
    }
}
