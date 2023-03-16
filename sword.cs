using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public movePlayer movePlayer;
    public Collider col;

    public swordGO[] swords;
    public bool[] swordsUnlock;
    public int[] NeedToUnlock;
    public int pickedSword = 0;
    public float scroll = 0;


    public float timerScroll = 0;
    public float cdScroll = 0.5f;
    public int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < swords.Length; i++)
        {
            swords[i].gameObject.SetActive(false);
        }
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        col.enabled = movePlayer.attackOfSword;
        scroll = Input.mouseScrollDelta.y;
        if (scroll > 0 && timerScroll < Time.time)
        {
            timerScroll = Time.time + cdScroll;
            counter++;
            FindNextUnlock();
        }

    }


    public void offAllSwords()
    {
        for (int i = 0; i < swords.Length; i++)
        {
            swords[i].gameObject.SetActive(false);
        }
    }

    public void FindNextUnlock()
    {
        offAllSwords();
        for (int i = pickedSword; i < swords.Length; i++)
        {
            if (i == swords.Length - 1 && pickedSword == i)
            {
                pickedSword = 0;
                break;
            }
            
            if (pickedSword == i)
            {
                continue;
            }
            if (swordsUnlock[i])
            {
                pickedSword = i;
                break;
            }
            if (i == swords.Length - 1)
            {
                pickedSword = 0;
            }
            
        }
        swords[pickedSword].gameObject.SetActive(true);
    }


    public void OnTriggerEnter(Collider other)
    {
        var targ = other.GetComponent<IDamagable>();
        if (targ != null && other.gameObject.name != "ARTEM")
        {
            Debug.Log("HIIIITTTT    " + other.gameObject.name);
            targ.getDamage(swords[pickedSword].damage); 
        }
    }
}
