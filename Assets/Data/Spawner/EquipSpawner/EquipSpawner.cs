using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSpawner : Spawner
{
    private static EquipSpawner _instance;
    public static EquipSpawner Instance => _instance;

    List<string> classType = new List<string> { "Warrior", "Magican", "Bowman", "Thief", "Pirate" };

    protected override void Awake()
    {
        base.Awake();
        if (EquipSpawner._instance != null) Debug.LogError("Only 1 EquipmentSpawner allow to exist");
        EquipSpawner._instance = this;
    }

    protected override void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        
        foreach(string classPlayer in classType)
        {
            string resPath = "Prefabs" + "/" + classPlayer;
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
        List<EquipmentDropList> equipments = monsterSO.equipmentDropList;

        foreach (EquipmentDropList equipment in equipments)
        {
            EquipmentName equipmentName = equipment.equipmentProfile.equipmentName;

            // Calculate a random position around the monster within a certain range
            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0f, 0f);
            Vector3 targetPosition = pos + randomOffset;

            // Spawn the equipment drop at the monster's position
            Transform equipmentDrop = Spawn(equipmentName.ToString(), pos, rot);

            if (equipmentDrop != null)
            {
                // Activate the game object associated with the equipment drop
                equipmentDrop.gameObject.SetActive(true);

                // Start a coroutine to move the equipment drop along a parabolic path to the target position
                StartCoroutine(FlyItem(equipmentDrop, targetPosition));
            }
        }
    }
}
