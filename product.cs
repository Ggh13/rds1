using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class product : MonoBehaviour
{
    public sword swords;
    public player_stats playerStats;
    public Text descriptionText;
    public string description;

    public GameObject destroable;
    // Start is called before the first frame update
    void Start()
    {
        descriptionText.text = description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void destroyMyParent()
    {
        Destroy(destroable);
    }



    public void giveMponey( int givenMiney)
    {
        playerStats.gold += givenMiney;
        destroyYourself();


    }
    public void UnlockSword(int numUnlock)
    {
        if (playerStats.gold >= swords.NeedToUnlock[numUnlock])
        {
            playerStats.gold -= swords.NeedToUnlock[numUnlock];
            swords.swordsUnlock[numUnlock] = true;
            destroyYourself();
        }
        

    }
    public void giveFood()
    {
        playerStats.food += 1;
        destroyYourself();

    }

    public void destroyYourself()
    {
        //Destroy(gameObject);
    }
}
