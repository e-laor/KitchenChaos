using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;


    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private const float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private const int platesSpawnMax = 4;
    

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if(platesSpawnAmount < spawnPlateTimerMax)
            {
                platesSpawnAmount++;

                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player is empty handed
            if (platesSpawnAmount > 0)
            {
                // There is at least 1 plate here
                platesSpawnAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
