using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        SpawnCoordinatesComponent spawnCoordinatesComponent =
            SystemAPI.GetSingletonRW<SpawnCoordinatesComponent>().ValueRO;

        RefRW<SpawnerComponent> spawner = SystemAPI.GetSingletonRW<SpawnerComponent>();
        bool isTimeValid = SystemAPI.Time.ElapsedTime >
                           spawner.ValueRO.PreviousSpawnTime + spawner.ValueRO.SpawnInterval;

        if (spawnCoordinatesComponent.IsNeedToSpawnEntities == 1 && isTimeValid)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            Entity entity = ecb.Instantiate(spawner.ValueRO.Prefab);

            Random rnd = new Random((uint)UnityEngine.Random.Range(int.MinValue, int.MaxValue));
            float3 initialVelocity = new float3(rnd.NextFloat(-3.0f, 3.0f), rnd.NextFloat(3.0f, 10.0f),
                rnd.NextFloat(-3.0f, 3.0f));

            ecb.AddComponent(entity, new RigidbodyComponent
            {
                Velocity = initialVelocity,
                Torque = initialVelocity
            });

            ecb.SetComponent(entity,
                LocalTransform.FromPositionRotation(spawnCoordinatesComponent.Coordinates,
                    quaternion.Euler(initialVelocity)));

            spawner.ValueRW.PreviousSpawnTime = SystemAPI.Time.ElapsedTime;

            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}