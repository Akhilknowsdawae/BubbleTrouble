using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathVisibility : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> waterLevels;

    private float secondsCount;
    bool fading;


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

        fading = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (TimeToFade())
        {

        }
    }
    
    // counting when to fade
    bool TimeToFade()
    {   
        secondsCount += Time.deltaTime;
        
        if (secondsCount % 5 == 0)
        {
            fading = true;
        }

        return fading;
    }

    
}
