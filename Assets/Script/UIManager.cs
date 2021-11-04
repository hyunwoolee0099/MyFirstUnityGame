using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text scoreUI;
    public Text nscoreUI;
    public Text comboUI;
    public Text comboUI2;
    public Text remaining;
    public Text comboDetail;
    public Text ItemDetail;
    public Text EndGameText;
    public GameObject fadeBG;
    public Image winImage;
    public Image loseImage;
    public Text gameInfoText;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        scoreUI.text = "Score\n" + 0;
        nscoreUI.text = "Hit :   " + 0 + "\nKill :   " + 0 + "\nCombo :   " + 0 + "\nBonus :   " + 0;
        comboUI.text = "Combo\n" + 0;
        comboUI2.gameObject.SetActive(false);
    }

    public void updateScore(int score, int hitscore, int killscore, int comboscore, int bonusscore)
    {
        scoreUI.text = "Score\n" + score;
        nscoreUI.text = "Hit :   " + hitscore + "\nKill :   " + killscore + "\nCombo :   " + comboscore + "\nBonus :   " + bonusscore;
    }

    public void updateCombo(int combo)
    {
        comboUI.text = "Combo\n" + combo;
        comboUI2.text = "Combo\n" + combo;
        if (combo != 0)
            StartCoroutine(comboTextColor());
        else if (combo == 0)
            comboUI.color = Color.gray;
    }

    public void updateRemaining(int bubbles, int miniBubbles)
    {
        remaining.text = "< Remaining >\n" + "Bubbles : " + bubbles + "\nMini-Bubbles : " + miniBubbles;
    }

    public void updateComboDetail(int index)
    {
        switch (index)
        {
            case 0:
                comboDetail.text = "Hit Bubble +1";
                break;
            case 1:
                comboDetail.text = "Hit Player +2";
                break;
            case 2:
                comboDetail.text = "Kill MiniBubble +3";
                break;
            case 3:
                comboDetail.text = "Kill Bubble +5";
                break;
            case 4:
                comboDetail.text = "Miss Bubble . . .";
                break;
            default:
                break;
        }
    }

    public void updateItemDetail(int index)
    {
        ItemDetail.gameObject.SetActive(true);
        switch (index)
        {
            case 0:
                ItemDetail.color = Color.green;
                ItemDetail.text = "! 20 Combo Item !";
                break;
            case 1:
                ItemDetail.color = Color.cyan;
                ItemDetail.text = "!! 50 Combo Item !!";
                break;
            case 2:
                ItemDetail.color = Color.red;
                ItemDetail.text = "!!! 100 Combo Item !!!";
                break;
            default:
                break;
        }
        StartCoroutine(ItemDetailFlicker());
    }

    IEnumerator comboTextColor()
    {
        comboUI.color = Color.red;
        comboUI.fontSize = 33;
        comboUI2.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        comboUI2.gameObject.SetActive(false);
        comboUI.fontSize = 30;
        comboUI.color = Color.white;
    }

    IEnumerator ItemDetailFlicker()
    {
        yield return new WaitForSeconds(1f);
        ItemDetail.gameObject.SetActive(false);
    }

    public void EndGame(int winOrLose)
    {
        if (winOrLose == 0)
        {
            EndGameText.text = "- W I N -";
            EndGameText.color = Color.cyan;
        }
        else if (winOrLose == 1)
        {
            EndGameText.text = "- L O S E -";
            EndGameText.color = Color.red;
        }


        Instantiate(fadeBG, new Vector3(0, 0, 0), Quaternion.identity);
        StartCoroutine(EndGameTextFlickerCorutine(winOrLose));
    }

    IEnumerator EndGameTextFlickerCorutine(int winOrLose)
    {
        yield return new WaitForSeconds(0.5f);
        EndGameText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        EndGameText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        EndGameText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        EndGameText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        EndGameText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (winOrLose == 0)
        {
            winImage.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            gameInfoText.color = Color.cyan;
        }
        else if (winOrLose == 1)
        {
            loseImage.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            gameInfoText.color = Color.gray;
        }
        Player player = GameObject.Find("Player").GetComponent<Player>();
        updateGameInfoText(player.score, player.highestCombo);
    }

    public void updateGameInfoText(int score, int comboRecord)
    {
        gameInfoText.text = "Score\n" + score + "\nCombo Record\n" + comboRecord;
        gameInfoText.gameObject.SetActive(true);
    }
}
