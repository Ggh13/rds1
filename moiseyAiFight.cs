using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moiseyAiFight : baseAI, IDamagable
{
    public Animator[] anim;
    public GameObject[] skins;
    public int pickedSkin;

    public GameObject target;
    public int damage = 10;

    public float distTarg = 0;

    public float timerAtt = 0;
    public float cdAtt = 2;

    public bool startFight;


    public float timerFree = 0;
    public float cdFree = 0;
    public void getDamage(int damage)
    {
        hp -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        pickedSkin = 0;
        target = null;
        timerFree = Time.time + 3;
    }

    public void startFightF()
    {
        startFight = true;
    }
    public void setBoolAll(string name, bool state)
    {
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetBool(name, state);
        }
    }

    public void setTriggerAll(string name)
    {
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetTrigger(name);
        }
    }

    public void updPickSkin()
    {
        for(int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
            Debug.Log("KVA " + i.ToString() + " " + skins.Length.ToString());
        }
        Debug.Log("KVRRRRRRRRRRA ");
        skins[pickedSkin].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        updPickSkin();
        if (timerFree < (Time.time - cdAtt) && startFight)
        {
            pickedSkin = 1;
        }
        if (startFight)
        {
            pickedSkin = 1;

            if (hp < 0)
            {
                Destroy(gameObject);
            }
            if (target != null)
            {
                skin.transform.LookAt(target.transform);
                distTarg = (transform.position - target.transform.position).magnitude;
                if (distTarg <= 6 && timerAtt < Time.time)
                {
                    timerAtt = Time.time + cdAtt;
                    setBoolAll("run", false);
                    setTriggerAll("attack");
                    attack();
                    timerFree = Time.time;

                }
                else if (distTarg > 6)
                {

                    goToTarget();
                }


            }


        }
        

    }


    public void goToTarget()
    {
        setBoolAll("run", true);
        myAi.SetDestination(target.transform.position);
    }

    public void attack()
    {
        setTriggerAll("attack");
        target.GetComponent<IDamagable>().getDamage(damage);


    }


    public IEnumerator swapBom()
    {

        yield return new WaitForSeconds(0.7f);
        attackOfSword = true;
        //allOffSounds();
        //swordSound[Random.Range(0, swordSound.Length)].SetActive(true);
        yield return new WaitForSeconds(1.3f);
        attackOfSword = false;
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tatar" && target == null)
        {
            target = other.gameObject;
        }
    }


}
