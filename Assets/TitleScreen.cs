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
    bool isCredit = false; 


    // Start is called before the first frame update
    void Start()
    {
        startButton.gameObject.SetActive(true);
        CreditsButton.gameObject.SetActive(true);
        Title.gameObject.SetActive(true);
        CreditText.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);

    }

    public void Credits()
    {
        CreditsButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        CreditText.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
        isCredit = true;
    }

    public void BackBut()
    {
        CreditsButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        CreditText.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        isCredit = true;
    }
    public void Startbut()
    {
        SceneManager.LoadScene("Scene Name");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
