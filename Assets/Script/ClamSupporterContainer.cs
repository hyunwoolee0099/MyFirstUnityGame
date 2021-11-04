using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClamSupporterContainer : MonoBehaviour
{

    public GameObject clamPearlContainer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ActivateClamSupporter()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(SpawnDoublePearl());
        StartCoroutine(DeactivateClamSupporter());
    }

    public IEnumerator SpawnDoublePearl()
    {
        while (true)
        {
            Instantiate(clamPearlContainer, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.7f);
        }
    }

    public IEnumerator DeactivateClamSupporter()
    {
        yield return new WaitForSeconds(4f);
        this.gameObject.SetActive(false);
    }
}
