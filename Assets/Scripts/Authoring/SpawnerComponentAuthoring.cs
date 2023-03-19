using Unity.Entities;
using UnityEngine;

public class SpawnerComponentAuthoring : MonoBehaviour
{
    public GameObject Prefab;

    public class SpawnerComponentBaker : Baker<SpawnerComponentAuthoring>
    {
        public override void Bake(SpawnerComponentAuthoring authoring)
        {
            AddComponent(new SpawnerComponent()
            {
                Prefab = GetEntity(authoring.Prefab)
            });
        }
    }
}