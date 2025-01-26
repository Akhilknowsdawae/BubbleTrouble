using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    int currency = 500;
    GameObject newTower;
    BaseTower selectedTower;
    public Camera mainCamera;

    public UnityEngine.UI.Button setPriorityClose;
    public UnityEngine.UI.Button setPriorityFar;
    public UnityEngine.UI.Button setPriorityHealth;
    public UnityEngine.UI.Button Upgrade;
    public TextMeshProUGUI DollarText;


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

        DollarText.text = currency.ToString();

        MoveNewTower();

        if (Input.GetMouseButtonDown(0))
        {
            if (newTower)
            {
                PlaceNewTower();
            }
            else
            {
                SelectTower();
            }
        }
    }

    void MoveNewTower()
    {
        if (newTower)
        {
            Vector3 pos = Input.mousePosition;
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                Vector3 euler = newTower.transform.rotation.eulerAngles;
                euler.z += 90.0f;

                newTower.transform.rotation = Quaternion.Euler(euler);
            }

            newTower.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0.1f));
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
            bool success = newTower.GetComponent<DragAndDrop>().TryPlacement();

            if (success)
            {
                newTower = null;
            }
            else
            {
                currency += newTower.GetComponent<BaseTower>().GetCostToBuy();
                Destroy(newTower);
            }
        }
    }

    void SelectTower()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 5.0f, LayerMask.GetMask("Towers"));

        if (hit.collider)
        {
            SpriteRenderer sprite = hit.collider.GetComponent<SpriteRenderer>();
            if (sprite)
            {
                if (PointInsideSprite(sprite, hit.point))
                {
                    BaseTower tower = hit.collider.GetComponent<BaseTower>();

                    if (tower)
                    {
                        if (tower == selectedTower)
                        {
                            selectedTower = null;

                            setPriorityClose.onClick.RemoveAllListeners();
                            setPriorityClose.gameObject.SetActive(false);

                            setPriorityFar.onClick.RemoveAllListeners();
                            setPriorityFar.gameObject.SetActive(false);

                            setPriorityHealth.onClick.RemoveAllListeners();
                            setPriorityHealth.gameObject.SetActive(false);

                            Upgrade.onClick.RemoveAllListeners();
                            Upgrade.gameObject.SetActive(false);
                        }
                        else
                        {
                            if (selectedTower)
                            {
                                setPriorityClose.onClick.RemoveAllListeners();
                                setPriorityFar.onClick.RemoveAllListeners();
                                setPriorityHealth.onClick.RemoveAllListeners();
                                Upgrade.onClick.RemoveAllListeners();
                            }

                            selectedTower = tower;

                            setPriorityClose.gameObject.SetActive(true);
                            setPriorityClose.onClick.AddListener(selectedTower.GetScanner().PriorityClose);

                            setPriorityFar.gameObject.SetActive(true);
                            setPriorityFar.onClick.AddListener(selectedTower.GetScanner().PriorityFar);

                            setPriorityHealth.gameObject.SetActive(true);
                            setPriorityHealth.onClick.AddListener(selectedTower.GetScanner().PriorityHealth);

                            Upgrade.gameObject.SetActive(true);
                            Upgrade.onClick.AddListener(selectedTower.UpgradeStats);
                        }
                    }
                }
            }
        }
    }

    bool PointInsideSprite(SpriteRenderer sprite, Vector2 point)
    {
        Vector2 localPoint = sprite.transform.InverseTransformPoint(point);

        return sprite.sprite.bounds.Contains(localPoint);
    }
}
