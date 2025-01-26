using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathVisibility : MonoBehaviour
{
    [Header("References")]
    private EnemySpawner endedWave;

    [Header("Water Levels")]
    public List<GameObject> waterLevels;

    // water states
    enum waterState
    {
        
        WL0,
        WL1,
        WL2
    }

    waterState currentWaterState;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < waterLevels.Count; i++)
        {
            if (i == 0)
            {
                waterLevels[i].GetComponent<Tilemap>().color = new Color(1.0f, 1.0f, 1.0f);
            }
            else
            {
                waterLevels[i].GetComponent<Tilemap>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }

        currentWaterState = waterState.WL0;

        endedWave = GameObject.FindObjectOfType<EnemySpawner>();

    }

    // Update is called once per frame
    void Update()
    {
        
        print(currentWaterState);

        switch (currentWaterState)
        {
            case (waterState.WL1):
                StartCoroutine(FadeIn(1, 0.1f, 1.0f));
                break;
            case (waterState.WL2):
                StartCoroutine(FadeOut(1, 0.1f, 0.0f));
                break;
            default:
                break;

        }

    }

    public void UpdateWaterLevel()
    {
        //if (countDownTime <= 0.0f)
        //{
        //    print("Water Level Changing");
        //    countDownTime = targetTime;
        //    currentWaterState++;
        //    //print(currentWaterState);
        //    if (((int)currentWaterState) > waterLevels.Count)
        //    {
        //        currentWaterState = waterState.WL1;
        //    }
        //}

        currentWaterState++;

        if (((int)currentWaterState) > waterLevels.Count)
        {
            currentWaterState = waterState.WL1;
        }

    }

    private IEnumerator FadeIn(int levelToFade, float fadeSpeed, float aEnd)
    {

        float aStart = waterLevels[levelToFade].GetComponent<Tilemap>().color.a;
        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / fadeSpeed)
        {
            Color newColor = new Color( 1f, 1f, 1f, Mathf.Lerp(aStart, aEnd, fadeSpeed));
            waterLevels[levelToFade].GetComponent<Tilemap>().color = newColor;


            yield return null;
        }
            //print("Water is at Level 2");

    }

    private IEnumerator FadeOut(int levelToFade, float fadeSpeed, float aEnd)
    {

        float aStart = waterLevels[levelToFade].GetComponent<Tilemap>().color.a;
        for (float t = 1.0f; t >= 0.0f; t -= Time.deltaTime / fadeSpeed)
        {
            Color newColor = new Color(1f, 1f, 1f, Mathf.Lerp(aStart, aEnd, fadeSpeed));
            waterLevels[levelToFade].GetComponent<Tilemap>().color = newColor;


            yield return null;
        }
            print("Water is at Level 1");

    }
}
