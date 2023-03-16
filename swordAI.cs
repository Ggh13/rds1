using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordAI : MonoBehaviour
{
    public baseAI movePlayer;
    public Collider col;
    public int damage = 10;

    public GameObject ItsMe;

    public GameObject totem1;
    public GameObject totem2;
    public GameObject totem3;

    // Start is called before the first frame update
    void Start()
    {
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        col.enabled = movePlayer.attackOfSword;

    }





    public void OnTriggerEnter(Collider other)
    {
        var targ = other.GetComponent<IDamagable>();
        if (targ != null && other.gameObject != ItsMe && (other.gameObject != totem1 && other.gameObject != totem2 && other.gameObject != totem3))
        {
            targ.getDamage(damage);
            movePlayer.attackOfSword = false;
        }
    }
}
