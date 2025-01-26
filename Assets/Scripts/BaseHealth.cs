using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseHealth : MonoBehaviour
{
    [Header("References")]
    public LevelManager LevelManager;

    [Header("Attributes")]
    [SerializeField] public int health;
    [SerializeField] public int maxhealth = 100;
    private bool isDead = false;

    public UnityEngine.UI.Image Soap_1;
    public UnityEngine.UI.Image Soap_2;
    public UnityEngine.UI.Image Soap_3;
    public UnityEngine.UI.Image Soap_4;
    public TextMeshProUGUI LivesText; 

    void Start()
    {
        health = maxhealth;
        Soap_1.gameObject.SetActive(true);
        Soap_2.gameObject.SetActive(false);
        Soap_3.gameObject.SetActive(false);
        Soap_4.gameObject.SetActive(false);
    }

    void Update()
    {

        LivesText.text = health.ToString();

        if (health >= 75 && health < 100)
        {
            Soap_1.gameObject.SetActive(false);
            Soap_2.gameObject.SetActive(true);
            Soap_3.gameObject.SetActive(false);
            Soap_4.gameObject.SetActive(false);
        }
        else if (health > 50 && health < 75)
        {
            Soap_2.gameObject.SetActive(false);
            Soap_3.gameObject.SetActive(true);
            Soap_1.gameObject.SetActive(false);
            Soap_4.gameObject.SetActive(false);
        }
        else if (health < 25)
        {
            Soap_3.gameObject.SetActive(false);
            Soap_4.gameObject.SetActive(true);
            Soap_1.gameObject.SetActive(false);
            Soap_2.gameObject.SetActive(false);
        }


        if (health <= 0 && !isDead)
        {
            isDead = true;
            //Put in gameover sequence here.
            LevelManager.gameOver();
            SceneManager.LoadScene("GameOver");

        }
    }
}
