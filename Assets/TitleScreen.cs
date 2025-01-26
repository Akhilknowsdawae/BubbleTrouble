using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class TitleScreen : MonoBehaviour
{

    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button CreditsButton;
    public UnityEngine.UI.Button BackButton;
    public UnityEngine.UI.Image Title;
    public TextMeshProUGUI CreditText;
    public AudioSource hoverButtonSound;
    public TextMeshProUGUI loading;
    bool isCredit = false; 


    // Start is called before the first frame update
    void Start()
    {
        startButton.gameObject.SetActive(true);
        CreditsButton.gameObject.SetActive(true);
        Title.gameObject.SetActive(true);
        CreditText.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        loading.gameObject.SetActive(false);

    }

    public void Credits()
    {
        hoverButtonSound.Play();
        CreditsButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        CreditText.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
        isCredit = true;
    }

    public void BackBut()
    {
        hoverButtonSound.Play();
        CreditsButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        CreditText.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        isCredit = true;
    }
    public void Startbut()
    {
      
        startButton.gameObject.SetActive(false);
        CreditText.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        Title.gameObject .SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(false);
        hoverButtonSound.Play();
        SceneManager.LoadScene("EnemyTest-Akhil");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
