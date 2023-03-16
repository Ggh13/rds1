using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moneyGet : MonoBehaviour
{
    public GameObject TextGo;
    public Text TextMon;

    public GameObject endPos;
    public float t = 0f;    // Start is called before the first frame update
    public player_stats player_StatsG;
    void Start()
    {
       // StartCoroutine(moneyGo(100));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCorutGiveMoney(int moneyGive)
    {
        StartCoroutine(moneyGo(moneyGive));
    }    

    public IEnumerator moneyGo(int money)
    {
        player_StatsG.gold += money;
        TextMon.text = "+" + money.ToString();
        t = 0f;
        Transform startPos = TextMon.transform;
        while (t < 1)
        {
            TextGo.transform.position = Vector3.Lerp(TextGo.transform.position, endPos.transform.position, t);
            TextMon.color = Color.Lerp(new Color32(0, 0, 0, 254), new Color32(0, 0, 0, 0), t);
            yield return new WaitForSeconds(0.8f);
            t += 0.08f;
            
        }
        TextGo.transform.position = startPos.position;
    }
}
