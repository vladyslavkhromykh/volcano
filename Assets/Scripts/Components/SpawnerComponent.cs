using Unity.Entities;

public struct SpawnerComponent : IComponentData
{
    public Entity Prefab;
    public double SpawnInterval;
    public double PreviousSpawnTime;
}