using UnityEngine;
using System.Collections;

public class NPCManager : MonoBehaviour
{
    public GameObject[] clients;
    public GameObject[] day1Clients;
    public GameObject[] day2Clients;

    public Transform spawnPoint;
    public TimeManager timeManager;
    public int[] clientHours = { 10, 14, 17 };

    private int currentClientIndex = 0;
    private GameObject currentClient;
    public GameObject CurrentClient => currentClient;

    private Coroutine leaveRoutine;

    public void SetClients(GameObject[] newClients)
    {
        clients = newClients;
    }

    public void ResetToFirstClient()
    {
        currentClientIndex = 0;

        if (leaveRoutine != null)
            StopCoroutine(leaveRoutine);

        if (currentClient != null)
        {
            Destroy(currentClient);
            currentClient = null;
        }

        CancelInvoke();
    }

    public void StartFirstClient()
    {
        SpawnNextClient();
    }

    public void SpawnNextClient()
    {
        if (currentClient != null)
            Destroy(currentClient);

        if (currentClientIndex >= clients.Length)
            return;

        timeManager.SetTime(clientHours[currentClientIndex]);

        currentClient = Instantiate(
            clients[currentClientIndex],
            spawnPoint.position,
            Quaternion.identity
        );

        currentClient.transform.rotation = Quaternion.Euler(0, 180, 0);

        currentClientIndex++;
    }

    public void MakeCurrentClientLeave()
    {
        if (currentClient == null)
        {
            SpawnNextClient();
            return;
        }

        var rock = currentClient.GetComponent<RockNPC>();

        if (rock != null)
        {
            rock.LeaveShop();
            leaveRoutine = StartCoroutine(LeaveThenSpawn());
        }
        else
        {
            Destroy(currentClient);
            SpawnNextClient();
        }
    }

    private IEnumerator LeaveThenSpawn()
    {
        yield return new WaitForSeconds(2f);
        SpawnNextClient();
    }
}