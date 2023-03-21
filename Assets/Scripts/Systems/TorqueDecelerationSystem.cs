using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct TorqueDecelerationSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, rigidbody) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<RigidbodyComponent>>())
        { 
            rigidbody.ValueRW.Torque *= 0.99f;
            transform.ValueRW.Rotation = quaternion.Euler(rigidbody.ValueRO.Torque);
        }
    }
}