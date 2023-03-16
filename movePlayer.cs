using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class movePlayer : player_stats, IDamagable
{

    public bool CantMoveBecouseDialog = false;


    public int nowSkin;
    public GameObject[] skins;
    public GameObject[] skins2;
    public Animator[] animatorSkins;
    public bool attackOfSword = false;
    [Header("SPEEEEEDS")]
    public float _moveSpeed = 2.0f;
    public float _runSpeed = 5.0f;

    public float _moveSpeedMaximum = 2.0f;
    public float _runSpeedMaximum = 5.0f;
    [SerializeField] private float _jumpHeight = 1.0f;


    [Header("Additional parameters")]
    [SerializeField] private float _smoothRotate = 0.25f;
    [SerializeField] private float _checkGroundDistance;

    private Vector3 _playerVelocity;
    private bool _isGrounded;
    private bool _isStaminaRecovery;

    [Header("References")]
    [SerializeField] private Transform _thisTransform;
    [SerializeField] private CharacterController characterController;

    private float _currentSpeed;
    private float _currentStamina;

    public Animator blackDeathBackround;
    public Animator my_anim;

    public const float gravity = -9.81f;

    [Header("cameraControllerIntoPlayer")]
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    Vector2 rotation = Vector2.zero;

    public bool canMove = true;


    [Header("Attack")]
    public GameObject enemy;
    public float timer;
    public float cdAttack;
    public int damage;
    public int enemyHp;

    public Color32 panelHP;
    public float alpha = 0;
    public Image panel;
    public Slider hp_slider;
    public GameObject swapSword;
    public GameObject walkSound;
    public GameObject runSound;
    public bool dead = false;

    public GameObject spawnPoint;

    public GameObject[] swordSound;



    public float timerOfZamedlo = 0;
    public float delayZamedlo = 10;
    public float procZamedlo = 100;


    public Text foodText;



    public float rollSped = 10;
    public int staminaForRoll = 10;
    public float continueTimeRoll = 3;
    public bool rollContinue = false;

    public Vector3 moveRollDerec;


    public int[] stackCounter;
    public Sprite[] stackImages;


    public float timerOfFIre = 0;
    public float delayFire = 7;

    public float timerOfBurn = 0;
    public float cdBurn = 7;
    public int fireStacks = 0;



    public float timerOfPoision = 0;
    public float delayPoision = 7;

    public float timerOfPoisionAct = 0;
    public float cdPoision = 7;
    public int PoisionStacks = 0;


    public Text moneyText;

    public bool getDamageB = false;
    private void Start()
    {
        fireBall(1);
        dead = false;
        swapSword.SetActive(false);
        _currentSpeed = _moveSpeed;
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        _moveSpeedMaximum = _moveSpeed;
        _runSpeedMaximum = _runSpeed;
        procZamedlo = 100;

    }
    private void Update()
    {
        if(hp <= 0)
        {
            SceneManager.LoadScene("NTOwithPP");
        }
        for(int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
            skins2[i].SetActive(false);
        }

        skins[nowSkin].SetActive(true);
        skins2[nowSkin].SetActive(true);
        // my_anim = animatorSkins[nowSkin];

        moneyText.text = gold.ToString();
        stackCounter[0] = fireStacks;
        stackCounter[1] = PoisionStacks;
        stackCounter[2] = (int)((float)(100 - procZamedlo)/10);
        stacksAllUpdate();
        
        foodText.text = food.ToString();
        if (Input.GetKeyDown(KeyCode.R) && food >= 1)
        {
            food -= 1;
            hp += 40;
            stamina += 80;
        }
        if(hp > 100)
        {
            hp = 100;
        }
        if(stamina > 100)
        {
            stamina = 100;
        }
        if (timerOfZamedlo < Time.time)
        {
            slowerMoveUpdate();
        }

            alpha = ((float)hp / (float)maxHp);
        panelHP.a = (System.Byte)alpha;
        panel.color = new Color32(255, 0, 0, (System.Byte)(255 - alpha * 255));

    }
    private void FixedUpdate()
    {
        //*** Проверка на земле ли персонаж
        Vector3 moveDirection = Vector3.zero;


        hp_slider.value = hp;
        stamina_slider.value = stamina;

        _isGrounded = CheckGround();

        if (_isGrounded)
        {
            _playerVelocity.y = 0f;
        }

        //***

        //*** Блок с ускорение персонажа
        stamina_res();
        if (!CantMoveBecouseDialog)
        {
            if (Input.GetKey(KeyCode.LeftShift) && stamina >= exp_run)
            {
                stamina -= exp_run;
                _currentSpeed = _runSpeed;
                animAllInteger("goState", 2);
                walkSound.SetActive(false);
                runSound.SetActive(true);
            }
            else
            {
                //stamina += stamina_res_count;
                _currentSpeed = _moveSpeed;
                animAllInteger("goState", 1);
                walkSound.SetActive(true);
                runSound.SetActive(false);
            }
        }




        //***

        //*** Блок с передвижением персонажа



        moveDirection = Vector3.zero;
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);


        //ector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 forward = transform.forward;
        // Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 right = transform.right;
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        if (inputVertical + inputHorizontal == 0)
        {
            walkSound.SetActive(false);
            runSound.SetActive(false);
        }

        float curSpeedX = _moveSpeed * inputVertical;
        float curSpeedY = _moveSpeed * Input.GetAxis("Horizontal");
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);




        if (moveDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            Quaternion deltaRotate = Quaternion.Lerp(_thisTransform.rotation, desiredRotation, _smoothRotate);

            if (!CantMoveBecouseDialog)
            {
                _thisTransform.rotation = deltaRotate;
            }
        }
        else
        {
            animAllInteger("goState", 0);
        }
        if (!CantMoveBecouseDialog)
        {
            if (!rollContinue)
            {
                moveRollDerec = moveDirection;
                characterController.Move(moveDirection * _currentSpeed + new Vector3(0, gravity, 0));
            }
            else
            {
                characterController.Move(moveRollDerec * rollSped + new Vector3(0, gravity, 0));
            }
        }
        

        if (!rollContinue && Input.GetKey(KeyCode.Space))
        {
            roll();
        }

        //***

        //*** Блок с прыжком персонажа


        //Debug.Log(timer.ToString() + " " + Time.time.ToString() + "   " + (timer < Time.time).ToString() + "  " + (enemy != null).ToString());
        if (Input.GetMouseButtonDown(0) && timer < Time.time && stamina >= punchStamina)
        {
            swapSword.SetActive(false);
            swapSword.SetActive(true);
            timer = Time.time + cdAttack;
            attackOfSword = true;
            animAllTrigger("attack");
            StartCoroutine(swapBom());
          
        }


        //***
    }

    private bool CheckGround()
    {
        var ray = new Ray(_thisTransform.position, -_thisTransform.up);

        return Physics.Raycast(ray, _checkGroundDistance);
    }



    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "   " + other.gameObject.tag);
        if (other.gameObject.tag == "totem")
        {
           // if (other.gameObject.GetComponent<base_totem>())
           // {
          //      enemy = other.gameObject;
          //  }
          //  else
          //  {
        //        enemy = other.gameObject.transform.parent.gameObject;
          //  }

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "totem")
        {
            //if (other.gameObject.GetComponent<base_totem>())
           // {
           //     enemy = null;
          //  }
           // else
          //  {
              //  if (other.gameObject.transform.parent.gameObject.GetComponent<base_totem>())
             //   {
              //      enemy = null;
              //  }

           // }

        }
    }
    public void stamina_res()
    {
        if (stamina < stamina_res_count)
            stamina = stamina_res_count;
        else if (stamina > max_stamina)
        {
            stamina = max_stamina;
        }

        if (last_stamina > stamina)
        {
            res = false;
            timer_stamina = Time.time + timer_res_stamina;
            last_stamina = stamina;
        }
        else if (timer_stamina <= Time.time)
        {
            last_stamina = stamina;
            res = true;
            stamina += stamina_res_count;

        }
    }
    public void allOffSounds()
    {
        for (int i = 0; i < swordSound.Length; i++)
        {
            swordSound[i].SetActive(false);
        }

    }


    public IEnumerator swapBom()
    {
        soundsMech();
        attackOfSword = true;
        allOffSounds();
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1.3f);
        attackOfSword = false;
    }

    public IEnumerator swapBomSounds()
    {
        allOffSounds();
        yield return new WaitForSeconds(0.7f);
        swordSound[Random.Range(0, swordSound.Length)].SetActive(true);
        yield return new WaitForSeconds(1.3f);
    }

    public void soundsMech()
    {
        StartCoroutine(swapBomSounds());
    }

    public void getDamage(int damage)
    {
        getDamageB = true;
        hp -= damage;
    }

    public void slowerMove(int proc)
    {
        timerOfFIre = Time.time + delayFire;
        slowerMoveUpdate();


        procZamedlo = 100 - proc;
        if (procZamedlo < 50)
        {
            procZamedlo = 50;
        }
    }


    public void fireBall(int stack)
    {
        timerOfFIre = Time.time + delayFire;
        fireStacks += stack;
    }

    public void fireUpdate()
    {
        if(timerOfBurn < Time.time)
        {
            timerOfBurn += cdBurn;
            hp -= 3 * fireStacks;
        }
        if(timerOfFIre < Time.time)
        {
            fireStacks = 0;
        }
    }





    public void PoisionBall(int stack)
    {
        timerOfPoision = Time.time + delayPoision;
        PoisionStacks += stack;
    }

    public void PoisionUpdate()
    {
        if (timerOfPoisionAct < Time.time)
        {
            timerOfPoisionAct += cdPoision;
            hp -= 5 * PoisionStacks;
        }
        if (timerOfPoision < Time.time)
        {
            PoisionStacks = 0;
        }
    }








    public void slowerMoveUpdate()
    {
        
        if(timerOfZamedlo < Time.time)
        {
            procZamedlo = 100;
        }
        Debug.Log(procZamedlo / 100);
        _moveSpeed = _moveSpeedMaximum * (procZamedlo / 100);
        _runSpeed = _runSpeedMaximum * (procZamedlo / 100);

    }

    public void roll()
    {
        rollContinue = true;
        characterController.Move(moveRollDerec * rollSped + new Vector3(0, gravity, 0));
        animAllTrigger("roll");
        StartCoroutine(rollGo());
    }

    public IEnumerator rollGo()
    {
        characterController.Move(moveRollDerec * rollSped + new Vector3(0, gravity, 0));
        yield return new WaitForSeconds(continueTimeRoll);
        characterController.Move(new Vector3(0, 0, 0));
        rollContinue = false;
    }

    public void stacksAllUpdate()
    {
        fireUpdate();
        slowerMoveUpdate();
        PoisionUpdate();
    }
    public void animAllBool(string name, bool sett)
    {
        for(int i = 0; i < skins.Length; i++)
        {
            animatorSkins[i].SetBool(name, sett);
        }
    }
    public void animAllTrigger(string name)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            animatorSkins[i].SetTrigger(name);
        }
    }

    public void animAllInteger(string name, int valiu)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            animatorSkins[i].SetInteger(name, valiu);
        }
    }

    public void changeSkin()
    {
        nowSkin = 1;
    }

    
}