using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct TorqueSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        float G = -9.8f;
        
        foreach (var (transform, rigidbody) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<RigidbodyComponent>>())
        {
            transform.ValueRW.Rotation = quaternion.Euler(rigidbody.ValueRO.Velocity);
        }
    }
}