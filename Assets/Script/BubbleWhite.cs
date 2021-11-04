using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWhite : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bubbleWhiteContainer;
    public float speed = 1f;
    float posX = -1f;
    bool posXdir = true;


    // Update is called once per frame
    void Update()
    {
        if (posXdir)
            posX += 0.1f;
        else
            posX -= 0.1f;

        if (posX > 1f)
            posXdir = false;

        if (posX < -1f)
            posXdir = true;


        this.transform.Translate(new Vector3(posX, 1f, 0f) * Time.deltaTime * speed);

        if (this.transform.position.y > 5f)
        {
            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
