using Unity.Entities;
using UnityEngine;

public class RigidbodyComponentAuthoring : MonoBehaviour
{
    
}

public class GravityComponentBaker : Baker<RigidbodyComponentAuthoring>
{
    public override void Bake(RigidbodyComponentAuthoring authoring)
    {
        AddComponent(new RigidbodyComponent());
    }
}