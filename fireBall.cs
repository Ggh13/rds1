using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : baseVarAttack
{
    public int damage = 10;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        transform.Translate(new Vector3(0, 0, speed));
    }

    public void OnTriggerEnter(Collider other)
    {
        var canBeat = other.gameObject.GetComponent<IDamagable>();
        if (canBeat != null && other.gameObject.tag != "Tatar")
        {
            player.GetComponent<movePlayer>().fireBall(1);
            canBeat.getDamage(damage);
            Destroy(gameObject);
        }
    }
}
