using UnityEngine;

public class getItManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int needGetCount = 5;
    public int nowGetHave = 0;

    public qwestManager qwestManagerR;

    public bool allEnd = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (qwestManagerR.simpleQwestsActive[0])
        {

        }
        if (needGetCount <= nowGetHave && !allEnd)
        {
            allEnd = true;
            qwestManagerR.qwestReady(0);
        }
    }
    public void PlussMushroom()
    {
        if (qwestManagerR.simpleQwestsActive[0])
        {

            nowGetHave++;
        }
    }

}
