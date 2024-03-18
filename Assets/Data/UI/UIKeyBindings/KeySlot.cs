using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeySlot : SecondMonoBehaviour, IDropHandler
{
    public void CreateKeyBinding(List<KeySlot> keySlots, string nameKey)
    {
        foreach (KeySlot keySlot in keySlots)
        {
            if (keySlot.name == this.transform.name)
            {
                Transform transform = UIKeyBindingsCtrl.Instance.Spawn(nameKey, new Vector3(0f, 0f, 0f), Quaternion.identity);
                if (transform == null) return;
                KeyDragDrop dragKey = transform.GetComponent<KeyDragDrop>();
                if (dragKey == null) return;

                dragKey.transform.parent = keySlot.transform;
                dragKey.transform.localScale = new Vector3(1f, 1f, 1f);
                dragKey.SetCanvasGroupTrue();
                dragKey.gameObject.SetActive(true);
            }
        }
    }
    public void CreateKeyBinding(List<KeySlot> keySlots, string nameKey, Transform Slot)
    {
        foreach (KeySlot keySlot in keySlots)
        {
            if (keySlot.name == Slot.name)
            {
                Transform transform = UIKeyBindingsCtrl.Instance.Spawn(nameKey, new Vector3(0f, 0f, 0f), Quaternion.identity);
                if (transform == null) return;
                KeyDragDrop dragKey = transform.GetComponent<KeyDragDrop>();
                if (dragKey == null) return;

                dragKey.transform.parent = keySlot.transform;
                dragKey.transform.localScale = new Vector3(1f, 1f, 1f);
                dragKey.SetCanvasGroupTrue();
                dragKey.gameObject.SetActive(true);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag == null) return;
        GameObject dropObj = eventData.pointerDrag;

        // For Use Item
        UseDragDrop useDragDrop = dropObj.GetComponent<UseDragDrop>();
        if (useDragDrop != null)
        {
            if (useDragDrop.oldParent.parent.name == "UseSlots")
            {
                UseCode useCode = useDragDrop.useInfo.useInformation.useProfile.useCode;

                UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(KeyBindingsCtrl.Instance.keySlots, useCode.ToString());
                UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(UIKeyOfKeyBindingsCtrl.Instance.keySlots, useCode.ToString());

                CreateKeyBinding(KeyBindingsCtrl.Instance.keySlots, useCode.ToString());
                CreateKeyBinding(UIKeyOfKeyBindingsCtrl.Instance.keySlots, useCode.ToString());

                UIKeyBindingsCtrl.Instance.FixUseAmount(useCode);

                UIKeyBindingsCtrl.Instance.SetKeyBindings();
                return;
            }
        }

        // For Skill 
        SkillDragDrop skillDragDrop = dropObj.GetComponent<SkillDragDrop>();
        if (skillDragDrop != null)
        {
            Debug.Log("skill Drag Drop");

            SkillCode skillCode = skillDragDrop.skillInfo.skillProfile.skillCode;

            UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(KeyBindingsCtrl.Instance.keySlots, skillCode.ToString());
            UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(UIKeyOfKeyBindingsCtrl.Instance.keySlots, skillCode.ToString());

            CreateKeyBinding(KeyBindingsCtrl.Instance.keySlots, skillCode.ToString());
            CreateKeyBinding(UIKeyOfKeyBindingsCtrl.Instance.keySlots, skillCode.ToString());

            UIKeyBindingsCtrl.Instance.SetKeyBindings();
            return;
        }

        // For Key drag drop
        KeyDragDrop keyDragDrop = dropObj.GetComponent<KeyDragDrop>();
        KeyDragDrop keyDragDropSwaped = this.transform.GetComponentInChildren<KeyDragDrop>();
        if (keyDragDrop != null)
        {
            if(keyDragDrop.oldParent.transform.parent.parent.name == "SlotsContainKeyBindings")
            {
                if(this.transform.parent.parent.name == "KeyBindings" || this.transform.parent.parent.name == "UIKeyOfKeyBindings")
                {
                    if(keyDragDropSwaped == null)
                    {
                        UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(KeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);
                        UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(UIKeyOfKeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);

                        CreateKeyBinding(KeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);
                        CreateKeyBinding(UIKeyOfKeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);

                        keyDragDrop.transform.SetParent(keyDragDrop.oldParent);
                        keyDragDrop.SetCanvasGroupTrue();
                        keyDragDrop.transform.gameObject.SetActive(false);
                    }
                }
            }

            if (keyDragDrop.oldParent.transform.parent.parent.name == "KeyBindings" || keyDragDrop.oldParent.transform.parent.parent.name == "UIKeyOfKeyBindings")
            {
                if(this.transform.parent.parent.name == "KeyBindings" || this.transform.parent.parent.name == "UIKeyOfKeyBindings")
                {
                    if (keyDragDropSwaped == null)
                    {
                        UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(KeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);
                        UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(UIKeyOfKeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);

                        CreateKeyBinding(KeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);
                        CreateKeyBinding(UIKeyOfKeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);

                        UIKeyBindingsCtrl.Instance.Despawn(keyDragDrop.transform);
                    }
                    if(keyDragDropSwaped != null)
                    {
                        UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(KeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);
                        UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(UIKeyOfKeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);

                        CreateKeyBinding(KeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);
                        CreateKeyBinding(UIKeyOfKeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);

                        UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(KeyBindingsCtrl.Instance.keySlots, keyDragDropSwaped.transform.name);
                        UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(UIKeyOfKeyBindingsCtrl.Instance.keySlots, keyDragDropSwaped.transform.name);

                        CreateKeyBinding(KeyBindingsCtrl.Instance.keySlots, keyDragDropSwaped.transform.name, keyDragDrop.oldParent);
                        CreateKeyBinding(UIKeyOfKeyBindingsCtrl.Instance.keySlots, keyDragDropSwaped.transform.name, keyDragDrop.oldParent);

                        UIKeyBindingsCtrl.Instance.Despawn(keyDragDrop.transform);
                    }
                }
                else
                {
                    UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(KeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);
                    UIKeyBindingsCtrl.Instance.DeleteDuplicateKey(UIKeyOfKeyBindingsCtrl.Instance.keySlots, keyDragDrop.transform.name);

                    UIKeyBindingsCtrl.Instance.Despawn(keyDragDrop.transform);
                    Transform transform = UIKeyBindingsCtrl.Instance.GetPrefabByName(keyDragDrop.name);
                    transform.gameObject.SetActive(true);
                }
            }
        }
        UIKeyBindingsCtrl.Instance.SetKeyBindings();
    }
}
