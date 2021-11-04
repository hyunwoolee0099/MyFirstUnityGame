using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboItem : MonoBehaviour
{

    public int itemID;
    float posX, posY, scale = 0f;
    public float speed = 0f;
    float scaleTemp = 1.2f;
    float speedTemp = 1.2f;
    bool isInitialScale = true;
    bool isInitialSpeed = true;
    Pearl pearl;
    Player player;

    void Start()
    {
        pearl = GameObject.Find("Pearl").GetComponent<Pearl>();
        if (pearl == null)
            Debug.LogError("ComboItem.pearl == Null");

        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
            Debug.LogError("ComboItem.player == null");

        this.transform.localScale = new Vector3(scale, scale, scale);
        posX = Random.Range(-1f, 1f);
        posY = -1f;
    }

    void Update()
    {
        if (isInitialScale)
            initialScale();
        if (isInitialSpeed)
            initialSpeed();
        this.transform.Translate(new Vector3(posX, posY, 0) * Time.deltaTime * speed);   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch(itemID)
            {
                case 0:
                    pearl.SpeedUp();
                    break;
                case 1:
                    pearl.SpeedDown();
                    break;
                case 2:
                    pearl.InstantiateTriplePearl();
                    break;
                case 3:
                    player.ActivateGuard();
                    break;
                case 4:
                    player.SetPlayerYaxis(2);
                    break;
                case 5:
                    player.ActivateCrab();
                    break;
                case 6:
                    player.ActivateClamSupporter();
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }

        if(other.tag == "Bubble")
        {
            flipXpassY();
        }

        if (other.tag == "bgSide")
        {
            flipXpassY();
        }

        if (other.tag == "bgSide_DoubleCheck")
        {
            flipXpassY();
        }

        if (other.tag == "DeadLine")
        {
            Destroy(this.gameObject);
        }
    }

    void initialScale()
    {
        scale = scale + (scaleTemp - (scaleTemp * 0.85f));
        scaleTemp *= 0.85f;
        this.transform.localScale = new Vector3(scale, scale, scale);
        if (scale >= 1f)
        {
            scale = 1f;
            this.transform.localScale = new Vector3(scale, scale, scale);
            isInitialScale = false;
        }
    }

    void initialSpeed()
    {
        speed = speed + (speedTemp - (speedTemp * 0.85f));
        speedTemp *= 0.85f;
        if (speed >= 1.2f)
        {
            isInitialSpeed = false;
            print(false);
        }
    }


    void flipXpassY()
    {
        posX *= -1f;
    }
}

