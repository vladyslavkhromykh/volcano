using Unity.Entities;
using UnityEngine;

public class SpawnerComponentAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    [Range(0.001f, 1.0f)]
    public double SpawnInterval;

    public class SpawnerComponentBaker : Baker<SpawnerComponentAuthoring>
    {
        public override void Bake(SpawnerComponentAuthoring authoring)
        {
            AddComponent(new SpawnerComponent()
            {
                Prefab = GetEntity(authoring.Prefab),
                SpawnInterval = authoring.SpawnInterval
            });
        }
    }
}