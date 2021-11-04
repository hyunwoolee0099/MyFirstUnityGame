using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlDeath : MonoBehaviour
{

    public float speed;
    public float posX;
    public float posY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speeddecrease();
        this.transform.Translate(new Vector3(posX, posY, 0) * Time.deltaTime * speed);
    }

    void speeddecrease()
    {
        speed *= 0.98f;
    }
}
