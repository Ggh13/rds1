using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class owlAttack : baseVarAttack
{
    public GameObject attackCollider;
    public float step_size_cd = 0.2f;
    public Vector3 startSize;
    public GameObject ParticalSystem;


    public bool attackContinue = false;
    // Start is called before the first frame update
    void Start()
    {
        ParticalSystem.SetActive(false);
        startSize = transform.localScale;
        attack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void attack()
    {
        Debug.Log("ATTCK&%^8474");
        ParticalSystem.SetActive(true);
        if (!attackContinue)
        {
            attackContinue = true;
            StartCoroutine(widthUp());
        }
        
    }
    public IEnumerator widthUp()
    {
        Debug.Log("Ae1");
        //yield return new WaitForSeconds(5);

        attackCollider.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        float t = 0;
        while (t < 1)
        {
            attackCollider.transform.localScale = Vector3.Lerp(attackCollider.transform.localScale, startSize, t);
            t += 0.01f;
            
            if(t > 0.22f)
            {
                ParticalSystem.SetActive(false);
                break;
            }
            yield return new WaitForSeconds(step_size_cd);
        }
        attackCollider.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        
        attackContinue = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<movePlayer>().PoisionBall(2);
        }
    }
}
