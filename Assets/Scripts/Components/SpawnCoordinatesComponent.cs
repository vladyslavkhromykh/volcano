using Unity.Entities;
using Unity.Mathematics;

public struct SpawnCoordinatesComponent : IComponentData
{
    public byte IsNeedToSpawnEntities;
    public float3 Coordinates;
}