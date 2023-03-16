using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationOfZone : MonoBehaviour
{

    public SphereCollider zone;
    public GameObject zoneGO;
    public float r;
    public bool turn = false;
    public int i = 0;

    public float speedRotate = 1f;
    public float timeTogoUp = 0.1f;

    public int amplitude = 10;

    public int iStep = 1;

    // Start is called before the first frame update
    void Start()
    {
        r = zone.radius;
        StartCoroutine(upDown());
    }

    public void corut()
    {
        StartCoroutine(upDown());
    }
    // Update is called once per frame
    void Update()
    {
        zoneGO.transform.Rotate(0, 0, speedRotate);


    }
    public IEnumerator upDown()
    {
        yield return new WaitForSeconds(timeTogoUp);
        if (turn)
        {
            i += iStep;
        }
        else
        {
            i -= iStep;
        }
        if (i >= amplitude)
        {
            turn = false;
        }
        if (i <= -amplitude)
        {
            turn = true;
        }
        zoneGO.transform.position = new Vector3(zone.transform.position.x, zone.transform.position.y + i, zone.transform.position.z);
        corut();
    }


}
