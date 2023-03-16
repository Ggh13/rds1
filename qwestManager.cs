using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class qwestManager : MonoBehaviour
{
    public int state = 1;
    public bool activeQw = false;


    public Text QwTextName;
    public Text QwText;
    public Text QwTextActive;

    [Header("MainQwest")]
    public string[] qw1StatesTextName;
    public string[] qw1StatesText;
    public string[] qw1StatesTextActive;


    [Header("SideQwest")]
    public bool[] simpleQwestsActive;
    public string[] simpleQwestsStatesTextName;
    public string[] simpleQwestsStatesText;
    public string[] simpleQwestsStatesTextNeedToDo;
    public bool[] simpleQwestsIhaveDo;
    public Text[] NameInFirstMen;
    public getItManager[] simpleQwestsStatesManager;
    public GameObject[] hideAfterDone;
    public Image[] backGround;
    public GameObject[] buttons;


    [Header("ListOfQwest")]
    public bool listOfQWBool = false;
    public GameObject QwestList;
    public GameObject QwestListAdd;
    public Text TextName;
    public Text TextOptions;
    public Text TextNeedToDo;

    public GameObject army;

    public armyManager armyManagerR;

    public GameObject swordForMiniMap;

    public moneyGet moneyGetScr;


    public GameObject thisButt;

    public int numberOfPicked = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = listOfQWBool ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = listOfQWBool;
        QwestList.SetActive(listOfQWBool);
    }

    // Update is called once per frame
    void Update()
    {

       


        if (Input.GetKeyDown(KeyCode.K))
        {

            activateMenu();
        }
        if (numberOfPicked == 45)
        {
            if (activeQw)
            {
                QwTextName.text = qw1StatesTextName[state - 1];
                QwText.text = qw1StatesText[state - 1];
                if (state == 1)
                {
                    swordForMiniMap.SetActive(true);
                    QwTextActive.text = "Убейте часть вражеских солдат: " + armyManagerR.countDead.ToString() + "/" + armyManagerR.countArmyBefore.ToString();
                }
                else if (state == 2)
                {
                    QwTextActive.text = "Убейте главного татара на Севере и заберите чашу";
                }
                else if(state == 3)
                {
                    QwTextActive.text = "Возьмите и отнесите чашу в деревню";
                }
                else
                {
                    swordForMiniMap.SetActive(false);
                }
                Debug.Log(qw1StatesText[state - 1]);
            }
            army.SetActive(state > 0);

        }
        else
        {
            QwTextName.text = simpleQwestsStatesTextName[numberOfPicked];
            QwText.text = simpleQwestsStatesText[numberOfPicked];
            QwTextActive.text = simpleQwestsStatesTextNeedToDo[numberOfPicked] + simpleQwestsStatesManager[numberOfPicked].nowGetHave.ToString() + "/" + simpleQwestsStatesManager[numberOfPicked].needGetCount.ToString();
        }


    }
    public void NextStage(int neededStage)
    {
        if(neededStage > state)
        {
            state = neededStage;

            moneyGetScr.StartCorutGiveMoney(100);
        }
        
    }

    public void activateMenu()
    {
        listOfQWBool = !listOfQWBool;
        Cursor.lockState = listOfQWBool ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = listOfQWBool;
        QwestList.SetActive(listOfQWBool);
        UpdateFirstName();

    }
    public void ActiveQW()
   {

       activeQw = true;
        //UpdateFirstName();

    }

    public void qwestReady(int numOfQwest)
    {
        simpleQwestsIhaveDo[numOfQwest] = true;
        moneyGetScr.StartCorutGiveMoney(100);
        hideAfterDone[numOfQwest].SetActive(false);
        backGround[numOfQwest].color = new Color(0, 250, 0);
        

    }
    public void pickMainQwest(int numOfQwest)
    {
        numberOfPicked = numOfQwest;
    }




    public void butThis(GameObject thisButoon)
    {
        thisButt = thisButoon;
    }

    public void Otvergnut(int numOfQwest)
    {
        if (numOfQwest == 45)
        {

        }
        else
        {
            simpleQwestsActive[numOfQwest] = false;
            thisButt.SetActive(false);
        }
        
    }

    public void UpdateFirstName()
    {
        for(int i = 0; i < NameInFirstMen.Length; i++)
        {
            NameInFirstMen[i].text = simpleQwestsStatesTextName[i];
            Debug.Log(simpleQwestsStatesTextName[i]);
            if (!simpleQwestsActive[i])
            {
                buttons[i].SetActive(false);
            }
            else
            {
                buttons[i].SetActive(true);
            }
        }
       
        
    }

    public void OnClickYourQwest(int numOfQwest)
    {
        if (numOfQwest != 45 && !simpleQwestsActive[numOfQwest])
        {
            thisButt.SetActive(false);
        }
        else
        {
            thisButt.SetActive(true);
            if (numOfQwest == 45)
            {
                TextName.text = qw1StatesTextName[state - 1];
                TextOptions.text = qw1StatesText[state - 1];
                TextNeedToDo.text = "Убейте часть вражеских солдат: " + armyManagerR.countDead.ToString() + "/" + armyManagerR.countArmyBefore.ToString();
            }
            else
            {

                TextName.text = simpleQwestsStatesTextName[numOfQwest];
                TextOptions.text = simpleQwestsStatesText[numOfQwest];
                TextNeedToDo.text = simpleQwestsStatesTextNeedToDo[numOfQwest];
            }

            QwestListAdd.SetActive(true);
        }
        
    }



}
