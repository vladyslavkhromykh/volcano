using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct SpawnerSystem : ISystem {

    public void OnUpdate(ref SystemState state)
    {
        RefRW<SpawnCoordinatesComponent> spawnCoordinatesComponent = SystemAPI.GetSingletonRW<SpawnCoordinatesComponent>();
        RefRW<SpawnerComponent> spawner = SystemAPI.GetSingletonRW<SpawnerComponent>();

        if (spawnCoordinatesComponent.ValueRO.IsNeedToSpawnEntities == 1)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            Entity entity = ecb.Instantiate(spawner.ValueRO.Prefab);
            ecb.AddComponent(entity, new RigidbodyComponent());
            ecb.SetComponent(entity, LocalTransform.FromPosition(spawnCoordinatesComponent.ValueRO.Coordinates));
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
        
    }
}