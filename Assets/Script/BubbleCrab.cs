using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCrab : MonoBehaviour
{

    public GameObject bubbleWhiteContainer;

    private void Start()
    {
//        StartCoroutine(SpawnBubbles());
    }

    public void StartSpawnBubbleCoroutine()
    {
        StartCoroutine(SpawnBubbles());
    }
    
    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            Instantiate(bubbleWhiteContainer, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.6f);
        }
    }
}
