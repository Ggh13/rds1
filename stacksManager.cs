using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class stacksManager : MonoBehaviour
{
    public Image[] imagesFosStacks;
    public Sprite[] spritesFosStacks;
    public Text[] textFosStacks;
    public movePlayer movePlayer;

    public Sprite defoultSprite;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < imagesFosStacks.Length; i++)
        {
            imagesFosStacks[i].sprite = defoultSprite;
            spritesFosStacks[i] = defoultSprite;
            textFosStacks[imagesFosStacks.Length - 1].text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUiStacks();
    }
    public void UpdateUiStacks()
    {
        for (int i = 0; i < imagesFosStacks.Length; i++)
        {
 
                spritesFosStacks[i] = movePlayer.stackImages[i];
                imagesFosStacks[i].sprite = spritesFosStacks[i];
                textFosStacks[i].text = movePlayer.stackCounter[i].ToString();

        }
    }
}
