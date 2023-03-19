using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public partial struct SpawnCoordinatesUserInputSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.EntityManager.CreateSingleton<SpawnCoordinatesComponent>();
    }

    public void OnUpdate(ref SystemState state)
    {
        byte isNeedToSpawnEntities = 0;
        float3 spawnPosition = float3.zero;

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            spawnPosition.z = 0;
            isNeedToSpawnEntities = 1;
        }

        RefRW<SpawnCoordinatesComponent> spawnCoordinatesComponent =
            SystemAPI.GetSingletonRW<SpawnCoordinatesComponent>();
        spawnCoordinatesComponent.ValueRW.IsNeedToSpawnEntities = isNeedToSpawnEntities;
        spawnCoordinatesComponent.ValueRW.Coordinates = spawnPosition;
    }
}