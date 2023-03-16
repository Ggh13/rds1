using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dialogSystem : MonoBehaviour
{
    public dialogStage stageNow;
    public Text saing;
    public Button[] ButtonsOfAsk;
    public Text[] TextOfAsk;
    public GameObject player;
    public bool playerHere = false;

    public GameObject dialogWindow;

    public int clickedButton;

    public GameObject skin;
    // Start is called before the first frame update


    
    public void Start()
    {
        saing.text = stageNow.textSaid;
        stageNow.activeMe(TextOfAsk, saing);
    }
    public void Update()
    {
        if (playerHere)
        {

            skin.transform.LookAt(player.transform.position);
        }
    }
    public void clickButton(int clickedBut = 1)
    {
        if (playerHere)
        {
            nextStage(clickedBut);
        }

    }

    public void nextStage(int clickedBut)
    {
        stageNow = stageNow.nextStage[clickedBut];
        stageNow.activeMe(TextOfAsk, saing);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {

            playerHere = true;
            Cursor.lockState = playerHere ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = playerHere;
            dialogWindow.SetActive(true);
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            
            playerHere = false;
            Cursor.lockState = playerHere ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = playerHere;
            dialogWindow.SetActive(false);
        }
    }

}
[System.Serializable]
public struct dialogStage
{
    public dialogStage[] nextStage;
    public string textSaid;
    public GameObject sound;
    public string[] ResponseOptions;
    public dialogStage[] stageAnswer;

    public void activeMe(Text[] textAsk, Text saying, int vali=0)
    {

        saying.text = textSaid;
        for (int i = 0; i < ResponseOptions.Length; i++)
        {
            textAsk[i].gameObject.SetActive(true);
            textAsk[i].text = ResponseOptions[i];
            Debug.Log(ResponseOptions[i]);
        }
    }
}
