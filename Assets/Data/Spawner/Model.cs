using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : SecondMonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
//    public Sprite sprite => _sprite;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpriteRenderer();
    }
    private void LoadSpriteRenderer()
    {
        if (this._spriteRenderer != null) return;
        this._spriteRenderer = transform.GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadSprite", gameObject);
    }

    public virtual void SetSprite(Sprite newSprite)
    {
        if (this._spriteRenderer.sprite.name == newSprite.name) return;
        this._spriteRenderer.sprite = newSprite;
    }
}
