using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBubble : MonoBehaviour
{
    float BubblePosX;
    float BubblePosY;
    float BubbleSpeed;

    public bool isMini;
    bool isSpeedDead = true;
    bool deadBubbleDeathOn = false;

    public GameObject DeadBubbleEffect;

    Player player;
    Pearl pearl;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        calculateSpeed();
        this.transform.Translate(new Vector3(BubblePosX, BubblePosY, 0) * Time.deltaTime * BubbleSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "DeadBubbleDeath")
        {
            if (deadBubbleDeathOn)
            {
                print("collison");
                BubbleDeadth();
            }
        }
        

        if (other.tag == "bgSide")
        {
            flipXpassY();
        }

        if (other.tag == "bgSide_DoubleCheck")
        {
            flipXpassY();
        }
    }

    public void InheritBubbleVector(float _BubblePosX, float _BubblePosY, float _BubbleSpeed)
    {
        BubblePosX = _BubblePosX;
        BubblePosY = _BubblePosY;
        BubbleSpeed = _BubbleSpeed;
    }

    void calculateSpeed()
    {
        if (isSpeedDead)
        {
            BubbleSpeed *= 0.95f;
            if (BubbleSpeed <= 0.01f)
            {
                BubbleSpeed = 0f;
                isSpeedDead = false;
                deadBubbleDeathOn = true;
            }
        }
    }

    void flipXpassY()
    {
        BubblePosX *= -1f;
    }

    void BubbleDeadth()
    {
        Instantiate(DeadBubbleEffect, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }

}
