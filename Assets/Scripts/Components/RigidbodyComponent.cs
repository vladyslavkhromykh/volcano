using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

[BurstCompile]
public struct RigidbodyComponent : IComponentData
{
    public float3 Velocity;
    public float3 Torque;
}

