using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warriorRus : baseAI, IDamagable
{
    public GameObject target;
    public int damage = 10;

    public float distTarg = 0;

    public float timerAtt = 0;
    public float cdAtt = 2;

    

    public void getDamage(int damage)
    {
        hp -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = null;
    }

    // Update is called once per frame
    void Update()
    {

        if(hp < 0)
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
                animator.SetBool("run", false);
                animator.SetTrigger("attack");
                attack();

            }
            else if (distTarg > 6)
            {

                goToTarget();
            }


        }

    }


    public void goToTarget()
    {
        animator.SetBool("run", true);
        myAi.SetDestination(target.transform.position);
    }

    public void attack()
    {
        animator.SetTrigger("attack");
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
        if (other.gameObject.tag == "Tatar" &&   target == null)
        {
            target = other.gameObject;
        }
    }

    
}
