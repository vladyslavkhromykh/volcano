using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

[RequireMatchingQueriesForUpdate]
[BurstCompile]
public partial struct GravitySystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        float G = -9.8f;
        
        foreach (var (transform, rigidbody) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<RigidbodyComponent>>())
        {
            float3 velocity = rigidbody.ValueRO.Velocity;
            velocity += new float3(0.0f, G, 0.0f) * Time.deltaTime;
            rigidbody.ValueRW.Velocity = velocity;
            
            
            float3 position = transform.ValueRO.Position;
            position += rigidbody.ValueRO.Velocity * deltaTime;
            transform.ValueRW.Position = position;
        }
    }
}