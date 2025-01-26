using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class lostthesoap : MonoBehaviour
{

    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button CreditsButton;
   
    public AudioSource hoverButtonSound;
    bool isCredit = false; 


    // Start is called before the first frame update
    void Start()
    {
     
       

    }

    public void Credits()
    {
      
     
    }

    public void BackBut()
    {
      
   
    }
    public void Startbut()
    {
      
        SceneManager.LoadScene("EnemyTest-Akhil");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
