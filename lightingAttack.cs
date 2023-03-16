using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightingAttack : baseVarAttack
{
    public Vector3 freezPos;// Start is called before the first frame update
    public float speed = 2;
    public bool CanIGo = true;
    void Start()
    {
        freezPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

   
    void Update()
    {
        if (CanIGo)
        {
            transform.LookAt(freezPos);
            transform.Translate(new Vector3(0, 0, speed));
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Tatar" && other.gameObject.name != "lightingBall(Clone)")
        {
          
            CanIGo = false;
        }
        if (player == other.gameObject)
        {
         
           player.GetComponent<movePlayer>().slowerMove(10);
            Destroy(gameObject);
        }
    }
}
