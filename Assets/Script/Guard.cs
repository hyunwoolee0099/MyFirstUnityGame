using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bubble")
        {
            this.gameObject.SetActive(false);
        }

        if (other.tag == "Pearl")
        {
            this.gameObject.SetActive(false);
        }
    }
}
