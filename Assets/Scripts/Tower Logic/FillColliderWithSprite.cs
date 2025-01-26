using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillColliderWithSprite
{
    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;
    SpriteRenderer spriteRenderer;

    public void init(SpriteRenderer renderer, BoxCollider2D box = null, CircleCollider2D circle = null)
    {
        spriteRenderer = renderer;
        boxCollider = box;
        circleCollider = circle;

        if (boxCollider)
        {
            FillBoxWithSprite();
        }
        else if (circleCollider)
        {
            FillCircleWithSprite();
        }
        else
        {
            Debug.LogError("Where's my collider foo?");
        }
    }

    void FillBoxWithSprite()
    {
        Vector2 colliderSize = boxCollider.size;
        Vector2 spriteSizeInWorldUnits = spriteRenderer.sprite.bounds.size;

        float scaleX = colliderSize.x / spriteSizeInWorldUnits.x;
        float scaleY = colliderSize.y / spriteSizeInWorldUnits.y;

        spriteRenderer.transform.localScale = new Vector3(scaleX, scaleY, 1);

        Vector3 pos = spriteRenderer.transform.position;

        spriteRenderer.transform.position = new Vector3(pos.x + boxCollider.offset.x, pos.y + boxCollider.offset.y, pos.z);
    }

    void FillCircleWithSprite()
    {
        float colliderRadius = circleCollider.radius;

        Vector2 spriteSizeInWorldUnits = spriteRenderer.sprite.bounds.size;

        float scaleFactor = colliderRadius * 2 / spriteSizeInWorldUnits.x;

        spriteRenderer.transform.localScale = new Vector3(scaleFactor * 1.1f, scaleFactor * 1.1f, 1);
    }
}
