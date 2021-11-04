using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearl : MonoBehaviour
{
    Player player;
    GameManager gameManager;
    public GameObject triplePearl;
    public GameObject DeathPearl;

    public int damageAmount = 2;
    public float speed = 2f;
    public float posX;
    public float posY;

    public bool isMini;
    public int pearlHP;

    public AudioSource itemGet;
    public AudioSource tick;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (isMini == false)
        {
            gameManager.CountPearl("addPearl");
            posX = Random.Range(-1f, 1f);
            posY = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(posX, posY, 0) * Time.deltaTime * speed);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        addAngle();
        tick.Play();
        if (other.tag == "Bubble")
        {
            if (isMini)
            {
                pearlHP -= 1;
                if (pearlHP <= 0)
                    Destroy(this.gameObject);
            }
            player.addScore(0, 0);
            player.addCombo(0);
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

        if (other.tag == "Player")
        {
            player.addCombo(1);
            player.playHitClip(posX, false);
            passXflipY();
        }

        if (other.tag == "Guard")
        {
            passXflipY();
        }

        if (other.tag == "DeadLine")
        {
            if (isMini == false)
            {
                player.addCombo(4);
                gameManager.CountPearl("delPearl");
            }
            Instantiate(DeathPearl, this.transform.position, Quaternion.identity);
            DeathPearl.GetComponent<PearlDeath>().speed = speed;
            DeathPearl.GetComponent<PearlDeath>().posX = posX;
            DeathPearl.GetComponent<PearlDeath>().posY = posY;
            Destroy(this.gameObject);
        }

        if (other.tag == "FinalCheck")
        {
            flipXpassY();
        }


        if (other.tag == "LoseGameTrigger")
        {
            tick.Stop();
            speed = 0f;
            flipXpassY();
        }

    }

    void addAngle()
    {
        posX *= Random.Range(0.8f, 1.2f);
        posY *= Random.Range(0.8f, 1.2f);

        if (posX > 1f || posX < -1f)
        {
            posX *= Random.Range(0.7f, 0.9f);
        }
        else if (posX > -0.2f || posX < 0.2f)
        {
            posX *= Random.Range(1.1f, 1.3f);
        }

        if (posY > 1f || posY < -1f)
        {
            posY *= Random.Range(0.7f, 0.9f);
        }
        else if (posY > -0.2f || posY < 0.2f)
        {
            posY *= Random.Range(1.1f, 1.3f);
        }
    }

    public void passXflipY()
    {
        posY *= -1f;
    }

    public void flipXpassY()
    {
        posX *= -1f;
    }

    public void flipAll()
    {
        posX *= -1f;
        posY *= -1f;
    }

    public void SpeedUp()
    {
        itemGet.Play();
        speed += 0.5f;
        if (speed >= 4f)
            speed = 4f;
    }

    public void SpeedDown()
    {
        itemGet.Play();
        speed -= 0.5f;
        if (speed <= 1.5f)
            speed = 1.5f;
    }

    public void InstantiateTriplePearl()
    {
        itemGet.Play();
        Instantiate(triplePearl, this.transform.position, Quaternion.identity);
    }
}
