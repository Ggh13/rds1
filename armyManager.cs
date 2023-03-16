using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armyManager : MonoBehaviour
{
    public int countArmyBefore = 5;
    public int countDead;
    public qwestManager qwestManager;

    public bool killArmyApruved = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countDead >= countArmyBefore - 2 && !killArmyApruved)
        {
            killArmyApruved = true;
            qwestManager.NextStage(2);
        }
    }
}
