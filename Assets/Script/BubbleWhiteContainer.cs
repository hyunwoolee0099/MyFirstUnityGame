using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWhiteContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(15f);
        Destroy(this.gameObject);
    }
}
