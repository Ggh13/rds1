using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totemCert : totemBase, IDamagable
{
    public bool playerHere = false;

    public float timerAttack = 0;
    public float cdAttack = 4;

    public SphereCollider sphereCollider;
    public Vector3 newPos;

    public GameObject fireBall;
    public GameObject spawnPoint;

    public bool teleportContinue = false;

    public bossAi bosdsAi;
    public int myNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        teleport();
        attack();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            bosdsAi.totemsIsDead[myNumber] = true;
            Destroy(gameObject);
        }
    }

    public void attack()
    {
        Debug.Log("ATTACK123");
        if (playerHere)
        {
            if (fireBall.GetComponent<baseVarAttack>().needClone)
            {
                Debug.Log("ATT66666666663");
                GameObject newFireBall = Instantiate(fireBall, spawnPoint.transform.position, Quaternion.identity);
                Debug.Log("ATT   " + newFireBall.gameObject.name.ToString());
                newFireBall.GetComponent<baseVarAttack>().player = player;
            }
            else
            {
                fireBall.GetComponent<baseVarAttack>().player = player;
                fireBall.GetComponent<owlAttack>().attack();
            }
            
        }
        
        //Действие атаки
        StartCoroutine(attackTimer());
    }

    public void teleport()
    {
        teleportContinue = true;
        Debug.Log("f1");
        if (!playerHere)
        {
            Debug.Log("Fd1");
            Vector2 rad_rand = Random.insideUnitCircle * sphereCollider.radius;
            newPos = sphereCollider.transform.position + new Vector3(rad_rand.x, 0, rad_rand.y );
            StartCoroutine(tpAnimator(newPos));
        }
        
       


    }

    public IEnumerator tpAnimator(Vector3 newPos)
    {
        Debug.Log("Ae1");
        yield return new WaitForSeconds(5);


        if (!playerHere)
        {
            StartCoroutine(teleportTimer());
        }

        Vector3 start_size = new Vector3(skin.transform.localScale.x, skin.transform.localScale.y, skin.transform.localScale.z);
        float t = 0;
        while (t < 1)
        {
            
            skin.transform.localScale = Vector3.Lerp(skin.transform.localScale, start_size / 50, t);
            t += 0.01f;
            yield return new WaitForSeconds(step_size_cd);
            if(t > 0.2)
            {
                break;
            }
        }
        //Destroy(spawnedHealth);

        transform.position = newPos;
        t = 0;
        while (t < 1)
        {
            skin.transform.localScale = Vector3.Lerp(skin.transform.localScale, start_size, t);
            t += 0.01f;
            yield return new WaitForSeconds(step_size_cd);
            if (t > 0.2)
            {
                break;
            }
        }

        teleport();
        teleportContinue = false;

    }

    public IEnumerator teleportTimer()
    {
        
        yield return new WaitForSeconds(1);
    }

    public IEnumerator attackTimer()
    {
        yield return new WaitForSeconds(cdAttack);
        attack();

    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            playerHere = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Exid GO :  " + other.gameObject.name.ToString());
        if (other.gameObject == player)
        {
            playerHere = false;
            if (!teleportContinue)
            {
                teleport();
            }
           
        }
        
    }


    public void getDamage(int damage)
    {
        hp -= damage;
    }
}
