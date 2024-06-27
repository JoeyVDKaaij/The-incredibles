using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Bow))]
public class DespawnBow : MonoBehaviour
{
    public static event System.Action OnBowDespawned;

    [SerializeField] private float despawnTime = 1f;
    

    Bow grabInteractable;
    bool hasBeenSelectedFirstTime = false;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<Bow>();
    }

    void DespawnOperation()
    {
        if(hasBeenSelectedFirstTime && !grabInteractable.isSelected)
        {
            OnBowDespawned?.Invoke();
            Destroy(gameObject, despawnTime);
        }
    }

    void CheckSelectedFirstTime()
    {
        if(!hasBeenSelectedFirstTime && grabInteractable.isSelected)
        {
            hasBeenSelectedFirstTime = true;
        }
    }

    //bool CanDespawn()
    //{
    //    if (despawnTime > 0)
    //    {
    //        despawnTime -= Time.deltaTime;
    //        return false;
    //    }
    //    return true;
    //}

    // Update is called once per frame
    void Update()
    {
        CheckSelectedFirstTime();
        DespawnOperation();
    }
}
