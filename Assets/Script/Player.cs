using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    public UIManager uiManager;
    public ItemSpawnManager itemSpawnManager;
    float yPos = -4;
    public int score, combo, hitScore, killScore, comboScore, bonusScore;
    public int highestCombo;

    public GameObject playerIdle;
    public GameObject hitTOLeftClip;
    public GameObject hitTORightClip;
    public GameObject hitPNG;
    public GameObject bottomLine;
    public GameObject clamSupporterContainer;
    Animator hitToLeftAnim;
    Animator hitToRifgtAnim;
    public AudioSource bubblePopBig;
    public AudioSource bubblePopSmall;
    public AudioSource itemGet;

    bool isHitPlay = false;
    bool othersPlay;
    bool isCalculateYaxis = false;
    bool isItemSpawning = false;
    float playingTime = 0;

    float yTarget;

    // Start is called before the first frame update
    void Start()
    {
        hitTORightClip.SetActive(false);
        hitTOLeftClip.SetActive(false);
        hitPNG.SetActive(false);
        hitToLeftAnim = hitTOLeftClip.GetComponent<Animator>();
        hitToRifgtAnim = hitTORightClip.GetComponent<Animator>();
        itemSpawnManager = GameObject.Find("ItemSpawnManager").GetComponent<ItemSpawnManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.transform.position = new Vector3(0, yPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitPlay == true)
        {
            playingTime = playingTime + Time.deltaTime;
            if (playingTime > 0.3f)
            {
                playerIdle.SetActive(true);
                hitTORightClip.SetActive(false);
                hitTOLeftClip.SetActive(false);
                othersPlay = false;
                isHitPlay = false;
            }
            else if (playingTime > 0.15f)
                hitPNG.SetActive(false);
        }
        if (isCalculateYaxis)
        {
            CalculatePlayerYaxis();
            
        }
        PlayerMovement();
    }

    void PlayerMovement()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, yPos, 0);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if (mousePos.x < -2.6f)
            mousePos.x = -2.6f;
        else if (mousePos.x > 2.6f)
            mousePos.x = 2.6f;
        this.transform.position = new Vector3(mousePos.x, yPos, 0);
    }

    public void SetPlayerYaxis(int caseType)
    {

        switch (caseType)
        {
            case 0:
                isCalculateYaxis = true;
                yTarget = 0.8f;
                break;
            case 1:
                isCalculateYaxis = true;
                yTarget = 0.4f;
                break;
            case 2:
                if (this.transform.position.y >= -3.8f)
                {
                    itemGet.Play();
                    isCalculateYaxis = true;
                    yTarget = -0.4f;
                }
                break;
        }

    }

    void CalculatePlayerYaxis()
    {
        float temp;
        yPos += yTarget * 0.015f;
        temp = yTarget * 0.015f;
        yTarget -= temp;
        if (gameManager.isLoseGame != true)
            bottomLine.GetComponent<BottomDeadLine>().bottomLineSetPosition(yPos);
        if (yTarget > 0f)
        {
            if (yTarget <= 0.002f)
                isCalculateYaxis = false;
            else if (yTarget <= 0.35f)
            {
                bottomLine.GetComponent<BottomDeadLine>().effect01.SetActive(false);
                bottomLine.GetComponent<BottomDeadLine>().effect02.SetActive(false);
            }
        }
        else if (yTarget < 0f)
        {
            if (yTarget >= -0.002f)
                isCalculateYaxis = false;
            else if (yTarget >= -0.35f)
            {
                bottomLine.GetComponent<BottomDeadLine>().effect01.SetActive(false);
                bottomLine.GetComponent<BottomDeadLine>().effect02.SetActive(false);
            }
        }
    }

    public void addScore(int scoreType, int tempcombo)
    {
        switch(scoreType)
        {
            case 0:
                score += 10;
                hitScore += 10;
                break;
            case 1:
                bubblePopSmall.Play();
                score += 300;
                killScore += 300;
                break;
            case 2:
                bubblePopBig.Play();
                score += 500;
                killScore += 500;
                break;
            case 3:
                score += tempcombo;
                comboScore += tempcombo;
                break;
            case 4:
                score += tempcombo;
                bonusScore += tempcombo;
                break;
        }
        uiManager.updateScore(score, hitScore, killScore, comboScore, bonusScore);
    }

    public void addCombo(int comboType)
    {
        int temp = 0;
        switch (comboType)
        {
            case 0:
                combo += 1;
                uiManager.updateComboDetail(comboType);
                temp = 10;
                break;
            case 1:
                combo += 2;
                uiManager.updateComboDetail(comboType);
                temp = 20;
                break;
            case 2:
                combo += 3;
                uiManager.updateComboDetail(comboType);
                temp = 30;
                break;
            case 3:
                combo += 5;
                uiManager.updateComboDetail(comboType);
                temp = 50;
                break;
            case 4:
                if (combo > highestCombo)
                    highestCombo = combo;
                uiManager.updateComboDetail(comboType);
                combo = 0;
                break;
        }

        addScore(3, temp);
        uiManager.updateCombo(combo);

        if (combo >= 20 && (combo % 20) >= 0 && (combo % 20) <= 4)
        {
            if (combo >= 100 && (combo % 100) >= 0 && (combo % 100) <= 4)
            {
                if (isItemSpawning == false)
                {
                    uiManager.updateItemDetail(2);
                    itemSpawnManager.HundredComboItemSpawn();
                    temp = 1500;
                    addScore(4, temp);
                    isItemSpawning = true;
                }
            }
            else if (combo >= 50 && (combo % 50) >= 0 && (combo % 50) <= 4)
            {
                if (isItemSpawning == false)
                {
                    uiManager.updateItemDetail(1);
                    itemSpawnManager.FiftyComboItemSpawn();
                    isItemSpawning = true;
                }
            }
            else
            {
                if (isItemSpawning == false)
                {
                    uiManager.updateItemDetail(0);
                    itemSpawnManager.TwentyComboItemSpawn();
                    isItemSpawning = true;
                }
            }
        }
        if (combo >= 50 && (combo % 50) >= 0 && (combo % 50) <= 4)
        {
            if (combo >= 100 && (combo % 100) >= 0 && (combo % 100) <= 4)
            {
                if (isItemSpawning == false)
                {
                    uiManager.updateItemDetail(2);
                    itemSpawnManager.HundredComboItemSpawn();
                    temp = 1500;
                    addScore(4, temp);
                    isItemSpawning = true;
                }
            }
            else
            {
                if (isItemSpawning == false)
                {
                    uiManager.updateItemDetail(1);
                    itemSpawnManager.FiftyComboItemSpawn();
                    isItemSpawning = true;
                }
            }
        }

        if (combo % 10 >= 5)
        {
            isItemSpawning = false;
        }
    }

    public void playHitClip(float dirX, bool isMini)
    {
        hitPNG.transform.localPosition = new Vector3(Random.Range(-0.45f, 0.56f), Random.Range(0.4f, 0.7f), 0);
        if(isMini == true)
            hitPNG.transform.localScale = new Vector3(Random.Range(0.4f, 0.65f), Random.Range(0.4f, 0.65f), Random.Range(0.4f, 0.65f));
        else
            hitPNG.transform.localScale = new Vector3(Random.Range(0.8f, 1f), Random.Range(0.8f, 1f), Random.Range(0.8f, 1f));
        hitPNG.SetActive(true);

        if (dirX >= 0f)
        {
            if (othersPlay == false)
            {
                hitTOLeftClip.SetActive(true);
                hitToLeftAnim.Play("Player-HitToLeft", 0, 0);
                othersPlay = true;
            }
        }
        else if(dirX < 0f)
        {
            if (othersPlay == false)
            {
                hitTORightClip.SetActive(true);
                hitToRifgtAnim.Play("Player-HitToRight", 0, 0);
                othersPlay = true;
            }
        }
        playerIdle.SetActive(false);
        playingTime = 0f;
        isHitPlay = true;
    }

    public void ActivateGuard()
    {
        itemGet.Play();
        bottomLine.GetComponent<BottomDeadLine>().ActivateGuard();
    }

    public void ActivateCrab()
    {
        itemGet.Play();
        bottomLine.GetComponent<BottomDeadLine>().ActivateCrab();
    }

    public void ActivateClamSupporter()
    {
        itemGet.Play();
        clamSupporterContainer.GetComponent<ClamSupporterContainer>().ActivateClamSupporter();
    }
}
