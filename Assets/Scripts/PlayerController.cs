using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int currency = 100;
    GameObject newTower;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        MoveNewTower();

        if (Input.GetMouseButtonDown(0))
        {
            PlaceNewTower();
        }
    }

    void MoveNewTower()
    {
        if (newTower)
        {
            Vector3 pos = Input.mousePosition;
            newTower.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0.0f));
        }
    }

    public void GetNewTower(GameObject inTower)
    {
        newTower = inTower;
    }
    
    public int GetCurrency()
    {
        return currency;
    }

    public void SetCurrency(int inCurrency)
    {
        currency = inCurrency;
    }

    private void PlaceNewTower()
    {
        if (newTower)
        {
            newTower.GetComponent<DragAndDrop>().TryPlacement();
            newTower = null;
        }
    }
}
