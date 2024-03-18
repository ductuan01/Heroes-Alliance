using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSpawner : Spawner
{
    private static UseSpawner _instance;
    public static UseSpawner Instance => _instance;

    List<string> useTypes = new List<string> { "Recovery", "Scroll" };

    protected override void Awake()
    {
        base.Awake();
        if (UseSpawner._instance != null) Debug.LogError("Only 1 UseSpawner allow to exist");
        UseSpawner._instance = this;
    }

    protected override void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        foreach (string useType in useTypes)
        {
            string resPath = "Prefabs" + "/" + useType;
            Debug.Log(resPath);
            Transform prefabsObj = transform.Find(resPath);
            if (prefabsObj == null) continue;
            foreach (Transform prefab in prefabsObj)
            {
                this.prefabs.Add(prefab);
            }
        }
        this.HidePrefabs();
    }

    public virtual void MonsterDrop(MonsterSO monsterSO, Vector3 pos, Quaternion rot)
    {
        List<UseDropList> uses = monsterSO.useDropList;
        foreach (UseDropList use in uses)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0f, 0f);
            Vector3 targetPosition = pos + randomOffset;

/*            if (use.useProfile.useType == UseType.RecoveryItem)
            {
                UseCode recoveryName = use.useProfile.recoveryName;
                Transform recoveryDrop = this.Spawn(recoveryName.ToString(), pos, rot);
                if (recoveryDrop == null) return;
                //Rate Drop here
                recoveryDrop.GetComponent<UseCtrl>().UseInformation.Amount = 1;
                recoveryDrop.gameObject.SetActive(true);
                StartCoroutine(FlyItem(recoveryDrop, targetPosition));
            }
            if (use.useProfile.useType == UseType.Scroll)
            {
                ScrollName scrollName = use.useProfile.scrollName;
                Transform scrollDrop = this.Spawn(scrollName.ToString(), pos, rot);
                if (scrollDrop == null) return;
                //Rate Drop here
                scrollDrop.GetComponent<UseCtrl>().UseInformation.Amount = 1;
                scrollDrop.gameObject.SetActive(true);
                StartCoroutine(FlyItem(scrollDrop, targetPosition));
            }*/
        }
    }
}
