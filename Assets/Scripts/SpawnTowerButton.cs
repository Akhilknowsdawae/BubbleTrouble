using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTowerButton : MonoBehaviour
{
    Button spawnButton;
    public Camera mainCamera;
    public GameObject TowerPrefab;
    public PlayerController player;

    bool bAffordable = true;

    int cost;

    // Start is called before the first frame update
    void Start()
    {
        spawnButton = GetComponent<Button>();
        spawnButton.onClick.AddListener(OnButtonClicked);

        cost = TowerPrefab.GetComponent<BaseTower>().GetCostToBuy();
    }

    private void Update()
    {
        if (player.GetCurrency() >= cost)
        {
            if (!bAffordable)
            {
                ColorBlock color = new ColorBlock();
                color.normalColor = new Color(0, 0, 1, 0.5f);
                bAffordable = true;
            }
        }
        else
        {
            if (bAffordable)
            {
                ColorBlock color = new ColorBlock();
                color.normalColor = new Color(1, 0, 0, 0.5f);
                bAffordable = false;
            }
        }
    }

    void OnButtonClicked()
    {
        if (bAffordable)
        {
            GameObject newTower = Instantiate(TowerPrefab, mainCamera.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            player.GetNewTower(newTower);

            player.SetCurrency(player.GetCurrency() - cost);
        }
    }
}
