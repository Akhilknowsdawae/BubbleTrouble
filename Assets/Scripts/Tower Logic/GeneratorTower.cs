using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTower : BaseTower
{
    [SerializeField] int currencyGeneration = 10;
    PlayerController player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Action()
    {

    }

    public void GenerateCurrency()
    {
        if (GetComponent<DragAndDrop>().enabled == false)
        {
            if (player)
            {
                player.SetCurrency(player.GetCurrency() + currencyGeneration);
                Debug.Log("Currency awarded");
            }
        }
    }
}
