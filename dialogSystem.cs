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

    public bool Ebut = false;

    public GameObject EButGO;

    public bool wasPereclWhilePlayerHere = false;

    public GameObject[] soundsAll;
    // Start is called before the first frame update


    
    public void Start()
    {
        stageNow.activeMe(TextOfAsk,ButtonsOfAsk, saing);
        stageNow.sound.SetActive(false);
        saing.text = stageNow.textSaid;
        offAllsounds();


    }
    public void Update()
    {

        if (Input.GetKey(KeyCode.G))
        {
            stageNow.sound.SetActive(false);
        }


        if (playerHere)
        {

            skin.transform.LookAt(player.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ebut = !Ebut;
        }
        if (Ebut && playerHere)
        {
            Cursor.lockState = playerHere ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = playerHere;
            dialogWindow.SetActive(true);
            player.GetComponent<movePlayer>().CantMoveBecouseDialog = true;
            if (!wasPereclWhilePlayerHere)
            {
                stageNow.sound.SetActive(true);
            }
            wasPereclWhilePlayerHere = false;
        }
        if (!Ebut && playerHere)
        {
            Cursor.lockState = playerHere ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = playerHere;
            dialogWindow.SetActive(false);
            player.GetComponent<movePlayer>().CantMoveBecouseDialog = false;
        }
        if (player.GetComponent<movePlayer>().getDamageB)
        {
            player.GetComponent<movePlayer>().getDamageB = false;

            Cursor.lockState = playerHere ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = playerHere;
            dialogWindow.SetActive(false);
            Ebut = false;
            player.GetComponent<movePlayer>().CantMoveBecouseDialog = false;

        }
    }

    public void offAllsounds()
    {
        for (int i = 0; i < soundsAll.Length; i++)
        {
            soundsAll[i].SetActive(false);
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
        stageNow.sound.SetActive(false);
        wasPereclWhilePlayerHere = true;
        if (!stageNow.ResponseOptionsIsEnd[clickedBut])
        {
            stageNow = stageNow.nextStage[clickedBut];
            stageNow.activeMe(TextOfAsk, ButtonsOfAsk, saing);
        }
        
    }
    public void clickMeCheckEnd(int clickedBut)
    {
        if (stageNow.ResponseOptionsIsEnd[clickedBut])
        {
            Ebut = false;
            playerHere = false;
            Cursor.lockState = playerHere ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = playerHere;
            dialogWindow.SetActive(false);
            player.GetComponent<movePlayer>().CantMoveBecouseDialog = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
       
        //stageNow.sound.SetActive(false);

        if (other.gameObject == player.gameObject)
        {
            EButGO.SetActive(!Ebut);
            playerHere = true;
            
        }
    }
    
    


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            dialogWindow.SetActive(false);
            EButGO.SetActive(false);
            Ebut = false;
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
    public bool[] ResponseOptionsIsEnd;
   // public dialogStage[] stageAnswer;

    public void activeMe(Text[] textAsk, Button[] buttonsOfAsk , Text saying, int vali=0)
    {
        sound.SetActive(true);
        saying.text = textSaid;
        for (int i = 0; i < ResponseOptions.Length; i++)
        {
            buttonsOfAsk[i].gameObject.SetActive(true);
            textAsk[i].gameObject.SetActive(true);
            textAsk[i].text = ResponseOptions[i];
            Debug.Log(ResponseOptions[i]);
        }
        for (int i = ResponseOptions.Length; i < buttonsOfAsk.Length; i++)
        {
            buttonsOfAsk[i].gameObject.SetActive(false);
            
            Debug.Log(buttonsOfAsk[i].gameObject.name);
        }
    }
    
}
