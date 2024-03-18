using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopupCtrl : SecondMonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;

    private float despwanTimer;
    private Color textColor;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    private void LoadText()
    {
        /*if (this.OutLine != null || this.Text != null) return;
        this.OutLine = transform.GetComponent<TextMeshPro>();
        this.Text = transform.Find("pfDamagePopup_1").GetComponent<TextMeshPro>();*/

        if (this.textMesh != null) return;
        this.textMesh = transform.GetComponent<TextMeshPro>();
        Debug.LogWarning(transform.name + ": LoadText", gameObject);
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        //this.OutLine.SetText(damageAmount.ToString());
        this.textMesh.SetText(damageAmount.ToString());
        if(!isCriticalHit)
        {
            this.textColor = new Color32(0xFF, 0x8C, 0x11, 0xFF);
            this.textMesh.color = this.textColor;
            this.textMesh.fontSize = 25f;
        }
        else
        {
            this.textColor = new Color32(0xD4, 0x2B, 0x05, 0xFF);
            this.textMesh.color = this.textColor;
            this.textMesh.fontSize = 35f;
        }
        this.despwanTimer = 1.5f;
    }

    public void Text(string Text)
    {
        this.textMesh.SetText(Text);
        this.textColor = new Color32(0xFF, 0x8C, 0x11, 0xFF);
        this.textMesh.color = this.textColor;
        this.textMesh.fontSize = 5f;
        this.despwanTimer = 1f;
    }

    private void Update()
    {
        float moveYSpeed = 1f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        despwanTimer -= Time.deltaTime;
        if(despwanTimer < 0)
        {
            float despawnSpeed = 3f;
            textColor.a -= despawnSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                DamagePopupSpawner.Instance.Despawn(transform);
            }
        }
    }
}
