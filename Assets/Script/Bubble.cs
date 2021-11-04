using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float BubblePosX;
    public float BubblePosY;
    public float BubbleSpeed;

    public int BubbleHP = 5;
    public bool isMini;
    bool isSpeedDead = false;

    GameManager gameManager;
    public GameObject BubbleContainer;
    public GameObject Bubble_Mini;
    public GameObject DeadBubble;
    Player player;
    Pearl pearl;

    Animator anim;
    public AudioSource bubblePopTick;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        pearl = GameObject.Find("Pearl").GetComponent<Pearl>();
        anim = GetComponent<Animator>();
        if (isMini == false)
        {
            gameManager.CountBubble("addBubble");
            gameManager.CountBubble("addBubbleMini");
            anim.Play("Bubble", 0, Random.Range(0, 3f));
            int DirIndex = (int)Random.Range(0, 4);
            switch (DirIndex)
            {
                case 0:
                    BubblePosX = 1f;
                    BubblePosY = 1f;
                    break;
                case 1:
                    BubblePosX = 1f;
                    BubblePosY = -1f;
                    break;
                case 2:
                    BubblePosX = -1f;
                    BubblePosY = 1f;
                    break;
                case 3:
                    BubblePosX = -1f;
                    BubblePosY = -1f;
                    break;
            }
        }
        else
        {
            anim.Play("Bubble-Mini", 0, Random.Range(0, 3f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateSpeed();
        this.transform.Translate(new Vector3(BubblePosX, BubblePosY, 0) * Time.deltaTime * BubbleSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isMini == true)
            anim.Play("Bubble-Mini", 0, Random.Range(0, 3f));
        else if (isMini == false)
            anim.Play("Bubble", 0, Random.Range(0, 3f));

        if (other.tag == "Bubble")
        {
            if (other.GetComponent<Bubble>().BubbleSpeed > 0.6f)
                BubbleSpeed = other.GetComponent<Bubble>().BubbleSpeed * 0.9f;
            float targetAngle;
            targetAngle = Vector3.Angle((other.transform.position - this.transform.position), this.transform.right);
            if (targetAngle > 0 && targetAngle <= 22.5f)
            {
                flipXpassY();
            }
            else if (targetAngle > 22.5f && targetAngle <= 67.5f)
            {
                flipAll();
            }
            else if (targetAngle > 67.5f && targetAngle <= 112.5f)
            {
                passXflipY();
            }
            else if (targetAngle > 112.5f && targetAngle < 157.5f)
            {
                flipAll();
            }
            else
            {
                flipXpassY();
            }
        }

        if (other.tag == "BubbleWhite")
        {
            bubblePopTick.Play();
            BubbleSpeed = other.GetComponent<BubbleWhite>().speed * 0.9f;
            if (BubblePosY < 0)
                flipAll();
            else if (BubblePosY >= 0)
                flipXpassY();

            Destroy(other.gameObject);
        }

        if (other.tag == "Item")
        {
            if (other.GetComponent<ComboItem>().speed > 0.6f)
                BubbleSpeed = other.GetComponent<ComboItem>().speed * 0.9f;
            float targetAngle;
            targetAngle = Vector3.Angle((other.transform.position - this.transform.position), this.transform.right);
            if (targetAngle > 0 && targetAngle <= 22.5f)
            {
                flipXpassY();
            }
            else if (targetAngle > 22.5f && targetAngle <= 67.5f)
            {
                flipAll();
            }
            else if (targetAngle > 67.5f && targetAngle <= 112.5f)
            {
                passXflipY();
            }
            else if (targetAngle > 112.5f && targetAngle < 157.5f)
            {
                flipAll();
            }
            else
            {
                flipXpassY();
            }
        }

        if (other.tag == "Pearl")
        {
            bubblePopTick.Play();
            BubbleHP = BubbleHP - other.GetComponent<Pearl>().damageAmount;
            if (BubbleHP <= 0)
            {
                if (isMini == false)
                {
                    player.addScore(2, 0);
                    player.addCombo(3);
                    gameManager.CountBubble("delBubble");
                    Destroy(this.gameObject);
                    GameObject MiniContainer = Instantiate(Bubble_Mini, this.transform.position, Quaternion.identity);
                    MiniContainer.transform.parent = BubbleContainer.transform;
                }
                else if (isMini == true)
                {
                    player.addScore(1, 0);
                    player.addCombo(2);
                    gameManager.CountBubble("delBubbleMini");
                    Destroy(this.gameObject);
                }
            }
            else
            {
                BubbleSpeed = other.GetComponent<Pearl>().speed * 0.8f;
                if (other.GetComponent<Pearl>().posX < 0)
                {
                    if (BubblePosX > 0)
                        BubblePosX *= -1f;
                }
                else if (other.GetComponent<Pearl>().posX > 0)
                {
                    if (BubblePosX < 0)
                        BubblePosX *= -1f;
                }

                if (other.GetComponent<Pearl>().posY < 0)
                {
                    if (BubblePosY > 0)
                        BubblePosY *= -1f;
                }
                else if (other.GetComponent<Pearl>().posY > 0)
                {
                    if (BubblePosY < 0)
                        BubblePosY *= -1f;
                }

                float targetAngle;
                targetAngle = Vector3.Angle((other.transform.position - this.transform.position), this.transform.right);
                if (targetAngle > 0 && targetAngle <= 22.5f)
                {
                        other.GetComponent<Pearl>().flipXpassY();
                }
                else if (targetAngle > 22.5f && targetAngle <= 67.5f)
                {
                    other.GetComponent<Pearl>().flipAll();
                }
                else if (targetAngle > 67.5f && targetAngle <= 112.5f)
                {
                    other.GetComponent<Pearl>().passXflipY();
                }
                else if (targetAngle > 112.5f && targetAngle < 157.5f)
                {
                    other.GetComponent<Pearl>().flipAll();
                }
                else
                {
                    other.GetComponent<Pearl>().flipXpassY();
                }
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

        if (other.tag == "bgUpDown")
        {
            passXflipY();
        }

        if (other.tag == "bgUpDown_DoubleCheck")
        {
            passXflipY();
        }

        if (other.tag == "FinalCheck")
        {
            if (isMini == false)
            {
                gameManager.CountBubble("delBubble");
                gameManager.CountBubble("delBubbleMini");
                gameManager.CountBubble("delBubbleMini");
                gameManager.CountBubble("delBubbleMini");
                gameManager.CountBubble("delBubbleMini");
                Destroy(this.gameObject);
            }
            else if (isMini == true)
            {
                gameManager.CountBubble("delBubbleMini");
                Destroy(this.gameObject);
            }
        }

        if (other.tag == "DeadLine")
        {
            player.addCombo(4);
            if (isMini == false)
            {
                gameManager.CountBubble("delBubbleAndMini");
            }
            else if (isMini == true)
            {
                gameManager.CountBubble("delBubbleMini");
            }
            BubbleDeadth();
        }

        if (other.tag == "Player")
        {
            bubblePopTick.Play();
            player.addCombo(1);
            BubbleSpeed = 3f;
            player.playHitClip(BubblePosX, isMini);
            passXflipY();
        }

        if (other.tag == "Guard")
        {
            bubblePopTick.Play();
            BubbleSpeed = 5f;
            passXflipY();
        }

        if (other.tag == "LoseGameTrigger")
        {
            LoseGame();
        }
    }



    void passXflipY()
    {
        BubblePosY *= -1f;
    }

    void flipXpassY()
    {
        BubblePosX *= -1f;
    }

    void flipAll()
    {
        BubblePosX *= -1f;
        BubblePosY *= -1f;
    }

    void calculateSpeed()
    {
        if(BubbleSpeed > 0.5f)
            BubbleSpeed *= 0.985f;
        else if (BubbleSpeed < 0.1f)
            BubbleSpeed *= 1.008f;
    }

    void BubbleDeadth()
    {
        if (isMini == false)
        {
            player.SetPlayerYaxis(0);
        }
        else
        {
            player.SetPlayerYaxis(1);
        }
        GameObject newDeadBubbles = Instantiate(DeadBubble, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        newDeadBubbles.GetComponent<DeadBubble>().InheritBubbleVector(BubblePosX, BubblePosY, BubbleSpeed);
        Destroy(this.gameObject);
    }

    public void LoseGame()
    {
        GameObject newDeadBubbles = Instantiate(DeadBubble, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        newDeadBubbles.GetComponent<DeadBubble>().InheritBubbleVector(BubblePosX, BubblePosY, BubbleSpeed);
        Destroy(this.gameObject);
    }
}
