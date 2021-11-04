using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    int bubbleNum, bubbleMiniNum, pearlNum;
    UIManager uiManager;
    public bool isPlayingGame = true;
    public bool isWinGame = false;
    public bool isLoseGame = false;
    public AudioSource bgm;

    public GameObject losegameTrigger;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void CountBubble(string bubbleCase)
    {
        switch(bubbleCase)
        {
            case "addBubble":
                bubbleNum += 1;
                break;
            case "addBubbleMini":
                bubbleMiniNum += 4;
                break;
            case "delBubble":
                bubbleNum -= 1;
                break;
            case "delBubbleAndMini":
                bubbleNum -= 1;
                bubbleMiniNum -= 4;
                break;
            case "delBubbleMini":
                bubbleMiniNum -= 1;
                break;
        }

        uiManager.updateRemaining(bubbleNum, bubbleMiniNum);

        if(bubbleNum == 0 && bubbleMiniNum == 0)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.addCombo(4);
                if(isLoseGame == false) 
                    WinGame();
        }
    }

    public void CountPearl(string pearlCase)
    {
        switch (pearlCase)
        {
            case "addPearl":
                pearlNum += 1;
                break;
            case "delPearl":
                pearlNum -= 1;
                break;
        }

        print("Pearl : " + pearlNum);

        if (pearlNum == 0)
        {
            if (isWinGame == false)
                LoseGame();
        }
    }

    public void WinGame()
    {
        bgm.Stop();
        isWinGame = true;
        uiManager.EndGame(0);
    }

    public void LoseGame()
    {
        bgm.Stop();
        isLoseGame = true;
        losegameTrigger.SetActive(true);
        uiManager.EndGame(1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
