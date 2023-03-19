using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct SpawnerSystem : ISystem {

    public void OnUpdate(ref SystemState state)
    {
        RefRW<SpawnCoordinatesComponent> spawnCoordinatesComponent = SystemAPI.GetSingletonRW<SpawnCoordinatesComponent>();
        
        if (spawnCoordinatesComponent.ValueRO.IsNeedToSpawnEntities == 1)
        {
            // Use ECB to spawn entities and get rid of iterating over all entities to find the one with SpawnerComponent
        }
    }
}