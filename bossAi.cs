using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossAi : baseAI, IDamagable
{
    public int state = 0;
    public GameObject[] placeGoToVillage;

    public GameObject target;
    public int damage = 40;


    public bool zamah = false;

    public float timerAtt = 0;

    public float cdAtt = 0;
    public bool iWentInPlace = true;

    public int placeNumGo = -1;


    public float distTarg2 = 0;

    public bool playerHere = false;

    public GameObject hpBarGO;
    public Slider hpBar;

    public GameObject[] totems;
    public bool[] totemsIsDead;

    public GameObject afkWhileTotemLive;
    public bool invulnerable = false;
    public bool afk = false;


    

    public SphereCollider boomCol;
    public bool wasBoom = false;
    public int addForcePower = 20;

    public GameObject myNowTarget;


    public GameObject myWeop;
    public GameObject playerWeop;
    public GameObject playerRig;

    public GameObject totem1;
    public GameObject totem2;
    public GameObject totem3;
    public GameObject chasha;

    public bool dead = false;
    public bool deadOnlyOne = false;

    public qwestManager qwestManagerR;
    // Start is called before the first frame update
    void Start()
    {
        chasha.GetComponent<interactions>().enabled = false;
        totemsIsDead = new bool[3];
        hpBarGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (hp <= 0)
        {
            dead = true;
        }
        hpBar.value = hp;
        if (state == 0)
        {
            if (iWentInPlace)
            {
                placeNumGo += 1;
                Debug.Log("{++++++]");
                if (placeNumGo < placeGoToVillage.Length)
                {
                    Debug.Log("GOOOOOOOOOOOOOOOOOOO");
                    iWentInPlace = false;


                }
                else
                {
                    state = 1; 
                }
            }
            else
            {
                distTarg2 = (transform.position - placeGoToVillage[placeNumGo].transform.position).magnitude;
                if (distTarg2 > 14)
                {
                    if(placeNumGo != placeGoToVillage.Length - 1)
                    {
                        goToTarget(placeGoToVillage[placeNumGo]);
                    }
                    else
                    {
                        
                        transform.position = placeGoToVillage[placeNumGo].transform.position;
                        goToTarget(placeGoToVillage[placeNumGo]);
                        iWentInPlace = true;
                        animator.SetBool("run", false);

                    }
                    
                }
                else
                {
                    iWentInPlace = true;
                    animator.SetBool("run", false);
                }
            }
            
        }
        else
        {
            if (playerHere)
            {
                if (!dead)
                {
                    hpBarGO.SetActive(true);
                    fightPhase();
                }
                else
                {
                    if (!deadOnlyOne)
                    {
                        chasha.GetComponent<interactions>().enabled = true;
                        deadOnlyOne = true;
                        qwestManagerR.NextStage(3);
                        animator.SetBool("dead", true);
                    }
                    

                }
                
            }
            
        }
       
    }

    public void fightPhase()
    {
        if(hp < 600  && state == 1)
        {
            state = 2;
            totems[0].SetActive(true);
            totems[1].SetActive(true);
            totems[2].SetActive(true);
            afk = true;
        }
        if(state >= 2)
        {
            if (totemsIsDead[0] == true && totemsIsDead[1] == true && totemsIsDead[2] == true)
            {
                afk = false;
            }
        }
        if (afk)
        {
            goToTarget(afkWhileTotemLive);
        }
        else
        {
            float distTarg = 0;
            skin.transform.LookAt(target.transform);
            distTarg = (transform.position - target.transform.position).magnitude;
            if (distTarg <= 19 && timerAtt < Time.time)
            {
                timerAtt = Time.time + cdAtt;
                zamah = true;
                animator.SetBool("run", false);
                attack();


            }
            else if (distTarg > 18)
            {
                if (!zamah)
                {
                    goToTarget(target);
                }

            }
        }
        


    }

    public void attack()
    {
            animator.SetTrigger("attack");
          StartCoroutine(swapBom());
    }

    public void goToTarget( GameObject targttt)
    {
        myNowTarget = targttt;
        animator.SetBool("run", true);
        myAi.SetDestination(targttt.transform.position);
    }


    public IEnumerator swapBom()
    {

        yield return new WaitForSeconds(3.6f);
        attackOfSword = true;
        yield return new WaitForSeconds(1f);
        attackOfSword = false;
        zamah = false;
    }


    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject == target)
        {
            if (!wasBoom)
            {
                StartCoroutine(boom());
            }
            
            playerHere = true;
            
        }
        if (playerHere && other.GetComponent<Rigidbody>() != null && !wasBoom && playerWeop != other.gameObject && myWeop != other.gameObject && playerRig != other.gameObject && totem1 != other.gameObject && totem2 != other.gameObject && totem3 != other.gameObject && other.gameObject != chasha)
        {
            boomCol.radius = 88;
            other.GetComponent<Rigidbody>().AddForce((-transform.position + other.transform.position - new Vector3(0, 0, 100)) * addForcePower);

        }
    }

    public IEnumerator boom()
    {
        boomCol.radius = 200;
        yield return new WaitForSeconds(5);
        boomCol.radius = 15;
        wasBoom = true;
    }
    public void getDamage(int damage)
    {
        if (state == 2)
        {
            if(totemsIsDead[1] == false || totemsIsDead[2] == false || totemsIsDead[0] == false)
            {
                invulnerable = true;
                hp = hp;
            }
            else
            {
                invulnerable = false;
                afk = false;
                hp -= damage;
            }
        }
        else
        {
            hp -= damage;
        }
        
    }
}
