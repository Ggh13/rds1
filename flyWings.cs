using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
public class flyWings : MonoBehaviour
{
    public GameObject wings;// Start is called before the first frame update
    public GameObject end;
    public Image wing1;
    public Image wing2;
    public Image wing3;
    public Text wing4;
    void Start()
    {
        StartCoroutine(goUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator goUp()
    {
        float t = 0f;
        float t2 = 0f;
        Transform start = transform;
        while (t < 1)
        {
            t += 0.001f;
            t2 += 0.007f;
            yield return new WaitForSeconds(0.05f);
            transform.position = Vector3.Lerp(transform.position, end.transform.position,t);
            wing1.color = Color.Lerp(new Color32(254, 254, 254, 254), new Color32(254, 254, 254, 0), t2);
            wing2.color = Color.Lerp(new Color32(254, 254, 254, 254), new Color32(254, 254, 254, 0), t2);
            wing3.color = Color.Lerp(new Color32(254, 254, 254, 254), new Color32(254, 254, 254, 0), t2);
            wing4.color = Color.Lerp(new Color32(254, 254, 254, 254), new Color32(254, 254, 254, 0), t2);

        }
        transform.position = start.position;
     
    }
}
