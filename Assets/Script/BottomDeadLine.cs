using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDeadLine : MonoBehaviour
{
    public float yPos;
    GameManager gameManager;
    public GameObject effect01;
    public GameObject effect02;
    public GameObject guard;
    public GameObject crab;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        crab.gameObject.SetActive(false);
    }

    public void bottomLineSetPosition(float yAxis)
    {
        effect01.SetActive(true);
        effect02.SetActive(true);
        yPos = yAxis - 0.155f;
        this.transform.position = new Vector3(-3.687f, yPos - 0.155f, 0);
        if (yPos >= 2.745f)
        {
            if(gameManager.isWinGame == false)
                gameManager.LoseGame();
        }
    }

    public void ActivateGuard()
    {
        guard.gameObject.SetActive(true);
    }

    public void ActivateCrab()
    {
        crab.gameObject.SetActive(true);
        StartCoroutine(crab.GetComponent<BubbleCrabContainer>().CrabActivateTimer());
    }
}
