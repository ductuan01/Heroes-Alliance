using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDrop : MonsterAbstract
{
    private bool isDrop = false;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.isDrop = false;
    }
    public void ItemsDrop()
    {
        if (this.isDrop) return;
        List<(string, string)> DropList = new List<(string, string)>();

        foreach (EquipmentDropList equipmentDrop in MonsterCtrl.MonsterSO.equipmentDropList)
        {
            if (Random.value < equipmentDrop.dropRate)
            {
                string name = equipmentDrop.equipmentProfile.ToString().Split(' ')[0];
                DropList.Add((name, equipmentDrop.equipmentProfile.GetType().ToString()));
            }
        }

        foreach (UseDropList useDrop in MonsterCtrl.MonsterSO.useDropList)
        {
            if (Random.value < useDrop.dropRate)
            {
                string name = useDrop.useProfile.ToString().Split(' ')[0];
                DropList.Add((name, useDrop.useProfile.GetType().ToString()));
            }
        }

        foreach (EtcDropList etcDrop in MonsterCtrl.MonsterSO.etcDropList)
        {
            if (Random.value < etcDrop.dropRate)
            {
                string name = etcDrop.etcProfile.ToString().Split(' ')[0];
                DropList.Add((name, etcDrop.etcProfile.GetType().ToString()));
            }
        }

        foreach (NTDDropList ntdDrop in MonsterCtrl.MonsterSO.ntdDropList)
        {
            if (Random.value < ntdDrop.dropRate)
            {
                string name = ntdDrop.ntdProfile.ToString().Split(' ')[0];
                DropList.Add((name, ntdDrop.ntdProfile.GetType().ToString()));
            }
        }

        Vector3 initialPosition = this.transform.position; 
        float spacing = 0.5f;

        for (int i = 0; i < DropList.Count; i++)
        {
            Transform transform = null;
            (string itemName, string itemType) = DropList[i];

            switch (itemType)
            {
                case "EquipProfileSO":
                    transform = EquipSpawner.Instance.Spawn(itemName, initialPosition, Quaternion.identity);
                    break;
                case "UseProfileSO":
                    transform = UseSpawner.Instance.Spawn(itemName, initialPosition, Quaternion.identity);
                    break;
                case "EtcProfileSO":
                    transform = EtcSpawner.Instance.Spawn(itemName, initialPosition, Quaternion.identity);
                    break;
                case "NTDProfileSO":
                    transform = NTDSpawner.Instance.Spawn(itemName, initialPosition, Quaternion.identity);
                    break;
            }
            transform.gameObject.SetActive(true);
            Vector3 targetPosition = initialPosition + new Vector3((i - (DropList.Count - 1) / 2f) * spacing, 0f, 0f);
            StartCoroutine(FlyItem(transform, targetPosition));
        }
        this.isDrop = true;
    }

    public IEnumerator FlyItem(Transform itemTransform, Vector3 targetPosition)
    {
        float flyDuration = 0.75f;
        float flyHeight = 1f;
        float elapsedTime = 0f;
        Vector3 initialPosition = itemTransform.position;

        while (elapsedTime < flyDuration)
        {
            float t = elapsedTime / flyDuration;
            itemTransform.position = Vector3.Lerp(initialPosition, targetPosition, t) + Vector3.up * Mathf.Sin(t * Mathf.PI) * flyHeight;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        itemTransform.position = targetPosition;
    }
}
