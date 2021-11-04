using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCrabContainer : MonoBehaviour
{

    public GameObject[] crab = new GameObject[6];
    public AudioSource crabActivateAudio;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public IEnumerator CrabActivateTimer()
    {
        crabActivateAudio.Play();
        crab[0].GetComponent<BubbleCrab>().StartSpawnBubbleCoroutine();
        crab[1].GetComponent<BubbleCrab>().StartSpawnBubbleCoroutine();
        crab[2].GetComponent<BubbleCrab>().StartSpawnBubbleCoroutine();
        crab[3].GetComponent<BubbleCrab>().StartSpawnBubbleCoroutine();
        crab[4].GetComponent<BubbleCrab>().StartSpawnBubbleCoroutine();
        crab[5].GetComponent<BubbleCrab>().StartSpawnBubbleCoroutine();
        yield return new WaitForSeconds(8f);
        this.gameObject.SetActive(false);
    }
}
