using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighingWayAI : MonoBehaviour
{
    public GameObject player;
    public GameObject endPos;
    public GameObject StartPos;
    public GameObject lighting;

    public Light lightingL;
    public bool imFirst = false;
    public bool start = false;

    public lighingWayAI next;

    public bool zapusNext = false;
    // Start is called before the first frame update
    void Start()
    {
        UpDownStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (start && !zapusNext)
        {
            zapusNext = true;
            StartCoroutine(delayStart());
        }
        lightingL.intensity = (transform.position - player.transform.position).magnitude/20;
    }
    public void UpDownStart()
    {
        if (start || imFirst)
        {
            StartCoroutine(UpDown());
        }

        
    }
    public IEnumerator UpDown()
    {
        float t = 0f;
        while (t < 1)
        {
            lighting.transform.position = Vector3.Lerp(StartPos.transform.position, endPos.transform.position, t);
            yield return new WaitForSeconds(0.02f);
            t += 0.01f;

        }
        t = 0f;
        while (t < 1)
        {
            lighting.transform.position = Vector3.Lerp( endPos.transform.position, StartPos.transform.position, t);
            yield return new WaitForSeconds(0.02f);
            t += 0.01f;

        }
        UpDownStart();

    }
    public IEnumerator delayStart()
    {
            yield return new WaitForSeconds(1.8f);
            next.start = true;
            UpDownStart();

    }
}
