using UnityEngine;
using UnityEngine.Pool;

public class VFXHitPool : MonoBehaviour
{
    public static VFXHitPool Instance {get; private set;}

    [SerializeField] private Transform hitVFXContainer;
    [SerializeField] private HitboxVFX playerPrefab;
    [SerializeField] private HitboxVFX projectilePrefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 50;

    private ObjectPool<HitboxVFX> _pool;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
        
        _pool = new ObjectPool<HitboxVFX>(
            createFunc: () => Instantiate(projectilePrefab),
            actionOnGet: vfx => vfx.gameObject.SetActive(true),
            actionOnRelease: vfx => vfx.gameObject.SetActive(false),
            actionOnDestroy: vfx => Destroy(vfx.gameObject),
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }
    
    public HitboxVFX SpawnHitbox(Vector3 position, Quaternion rotation, float damage)
    {
        HitboxVFX vfx = _pool.Get();
        vfx.transform.SetPositionAndRotation(position, rotation);
        vfx.Init(damage, Release);
        return vfx;
    }
    
    public HitboxVFX SpawnProjectile(Vector3 startPosition, Vector3 targetPosition, float damage, CharacterType characterType)
    {
        HitboxVFX vfx = _pool.Get();
        vfx.transform.position = startPosition;
        vfx.transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPosition - startPosition); // 2D-friendly facing
        vfx.InitProjectile(characterType ,damage, targetPosition, Release);
        return vfx;
    }
    
    private void Release(HitboxVFX vfx)
    {
        _pool.Release(vfx);
    }
}

