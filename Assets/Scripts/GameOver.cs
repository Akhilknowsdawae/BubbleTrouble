using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{

    public UnityEngine.UI.Button BackButton;
    public UnityEngine.UI.Button RestartButton;

    // Start is called before the first frame update
    void Start()
    {

    } // D29EF1 8445E7

    public void restart()
    {
        SceneManager.LoadScene("EnemyTest-Akhil");
    }

    public void backtomain()
    {
        SceneManager.LoadScene("Title_Screen");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
