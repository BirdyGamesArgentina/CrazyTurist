using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticlePool : MonoBehaviour
{

    public static ParticlePool instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] Transform parent;
    Dictionary<string, ObjectPool<ParticleSystem>> registry = new Dictionary<string, ObjectPool<ParticleSystem>>();


    public static void AddRegistry(string _key, ParticleSystem model)
    {
        instance._addToRegistry(_key, model);
    }
    public static ParticleSystem Get(string _key)
    {
        return instance._get(_key);
    }
    public static void Release(string _key, ParticleSystem torelease)
    {
        instance._release(_key, torelease);
    }

    void _addToRegistry(string _key, ParticleSystem model)
    {
        if (!registry.ContainsKey(_key))
        {

            ObjectPool<ParticleSystem> pool = new ObjectPool<ParticleSystem>
                (
                 createFunc: () =>
                 {
                     ParticleSystem part = Instantiate(model, parent);
                     return part;
                 },
                 actionOnGet: x => x.gameObject.SetActive(true),
                 actionOnRelease: x => x.gameObject.SetActive(false),
                 actionOnDestroy: x => Destroy(x.gameObject)
                );


            registry.Add(_key, pool);
        }
    }
    ParticleSystem _get(string _key)
    {
        if (registry.ContainsKey(_key))
        {
            return registry[_key].Get();
        }

        return null;
    }
    void _release(string _key, ParticleSystem torelease)
    {
        if (registry.ContainsKey(_key))
        {
            registry[_key].Release(torelease);
        }
    }
}
