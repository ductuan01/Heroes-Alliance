using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : SecondMonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] protected Transform holder;

    [SerializeField] protected int spawnedCount = 0;
    public int SpawnedCount => spawnedCount;

    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjs;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
        this.LoadHolder();
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.Log(transform.name + ": LoadHolder", gameObject);
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabsObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabsObj)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefabs();
    }

    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab not found: " + prefabName);
            return null;
        }

        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawnPos, rotation);
        newPrefab.parent = this.transform;
        newPrefab.parent = this.holder;
        //newPrefab.gameObject.SetActive(true);
        this.spawnedCount++;
        return newPrefab;
    }

    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach(Transform poolObj in this.poolObjs)
        {
            if (poolObj.name == prefab.name)  {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    public virtual void Despawn(Transform obj)
    {
        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        this.spawnedCount--;
    }

    public virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }
        return null;
    }

    public IEnumerator FlyItem(Transform itemTransform, Vector3 targetPosition)
    {
        // Define the duration of the flying animation
        float flyDuration = 0.75f;
        float flyHeight = 1f;
        float elapsedTime = 0f;
        Vector3 initialPosition = itemTransform.position;

        // Perform the flying animation
        while (elapsedTime < flyDuration)
        {
            float t = elapsedTime / flyDuration;
            itemTransform.position = Vector3.Lerp(initialPosition, targetPosition, t) + Vector3.up * Mathf.Sin(t * Mathf.PI) * flyHeight; // Parabolic path
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the item lands exactly at the target position
        itemTransform.position = targetPosition;
    }

    public IEnumerator FlyItemDrop(Transform itemTransform, Vector3 targetPosition)
    {
        // Define the duration of the flying animation
        float flyDuration = 0.35f;
        float holdDuration = 0.05f; // Duration to hold in space
        float flyHeight = 0.35f; // Adjust the height to fly up
        float dropHeight = 1f; // Height from where it starts to fall
        float elapsedTime = 0f;
        Vector3 initialPosition = itemTransform.position;

        // Perform the flying animation
        while (elapsedTime < flyDuration)
        {
            float t = elapsedTime / flyDuration;
            // Move the item up
            itemTransform.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * flyHeight, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(holdDuration);

        // Let the item fall down
        while (elapsedTime < flyDuration * 2) // Assuming the total duration includes both up and down motion
        {
            float t = (elapsedTime - flyDuration) / flyDuration; // Use elapsed time relative to the down motion
                                                                 // Move the item down
            itemTransform.position = Vector3.Lerp(initialPosition + Vector3.up * flyHeight, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the item lands exactly at the target position
        itemTransform.position = targetPosition;
    }
}
