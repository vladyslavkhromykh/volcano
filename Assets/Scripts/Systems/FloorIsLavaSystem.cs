using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial struct FloorIsLavaSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        NativeList<Entity> entitiesToDestroy = new NativeList<Entity>(Allocator.TempJob);
        
        foreach (var (transform, entity) in SystemAPI.Query<LocalTransform>().WithAll<RigidbodyComponent>().WithEntityAccess())
        {
            if (transform.Position.y > 0.0f)
            {
                continue;
            }
            
            entitiesToDestroy.Add(entity);
        }
        
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);
        entityCommandBuffer.DestroyEntity(entitiesToDestroy.ToArray(Allocator.TempJob));
            
        state.Dependency.Complete();
        entityCommandBuffer.Playback(state.EntityManager);
        entityCommandBuffer.Dispose();
    }
}