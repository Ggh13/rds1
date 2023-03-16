using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactions : MonoBehaviour
{
    public GameObject pressE;
    public bool pressEbutFlag = false;
    
    public GameObject dialogMenu;
    public bool playerHere = false;
    public GameObject playerGO;

    public GameObject tradeMenu;

    public getItManager getItManagerR;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = pressEbutFlag ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = pressEbutFlag;
        tradeMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerHere && Input.GetKeyDown(KeyCode.E))
        {
            
            pressEbutFlag = !pressEbutFlag;
            tradeMenu.SetActive(pressEbutFlag);

            Cursor.lockState = pressEbutFlag? CursorLockMode.Confined: CursorLockMode.Locked;
            Cursor.visible = pressEbutFlag;

        }
 

        
    }

    public void GetMe()
    {
        if (getItManagerR)
        {
            getItManagerR.PlussMushroom();
            Destroy(gameObject);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject == playerGO)
        {
            playerHere = true;
            pressE.SetActive(playerHere);

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerGO)
        {
            playerHere = false;
            pressEbutFlag = false;
            pressE.SetActive(playerHere);
        }
    }
}
