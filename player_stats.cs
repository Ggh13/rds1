using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class player_stats : MonoBehaviour
{

    public float graviry = 10f;

    public int maxHp = 100;
    public int hp = 100;
    public bool res = false;

    [Header("Stamina")]
    public Slider stamina_slider;
    public float max_stamina = 100;
    public float stamina = 100;
    public float punchStamina = 2;

    public float stamina_res_count = 0.1f;
    public float last_stamina = 100;
    public float exp_run = 0.1f;
    public float timer_res_stamina = 5f;
    public float timer_stamina = 0f;


    public int gold = 0;
    public int food = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
