using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{

    public GameObject[] TwentyComboItems = new GameObject[2];
    public GameObject[] FiftyComboItems = new GameObject[3];
    public GameObject[] HundredComboItems = new GameObject[2];

    Player player;
    public AudioSource itemSpawn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
            Debug.LogError("ItemSpawnManager.Player Null");
    }

    public void TwentyComboItemSpawn()
    {
        Vector3 posToSpawn = new Vector3(Random.Range(-2.8f, 2.8f), Random.Range(0f, 4.5f), 0);
        int RandomID = (int)Random.Range(0, 2);
        GameObject twentyComnoItem = Instantiate(TwentyComboItems[RandomID], posToSpawn, Quaternion.identity);
        itemSpawn.Play();
    }

    public void FiftyComboItemSpawn()
    {
        Vector3 posToSpawn = new Vector3(Random.Range(-2.8f, 2.8f), Random.Range(0f, 4.5f), 0);
        int RandomID = (int)Random.Range(0, 3);
        GameObject fiftyComnoItem = Instantiate(FiftyComboItems[RandomID], posToSpawn, Quaternion.identity);
        itemSpawn.Play();
    }

    public void HundredComboItemSpawn()
    {
        Vector3 posToSpawn = new Vector3(Random.Range(-2.8f, 2.8f), Random.Range(0f, 4.5f), 0);
        int RandomID = (int)Random.Range(0, 2);
        GameObject fiftyComnoItem = Instantiate(HundredComboItems[RandomID], posToSpawn, Quaternion.identity);
        itemSpawn.Play();
    }
}
