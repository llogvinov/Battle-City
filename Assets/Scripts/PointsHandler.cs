using System.Collections;
using UnityEngine;

public class PointsHandler : MonoBehaviour
{
    [SerializeField] private Sprite[] _pointSprites;
    [SerializeField] private int[] _pointValues;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = null;
    }

    public void ChangeSprite(int pointValue, Vector3 position)
    {
        StartCoroutine(ChangeSpriteCoroutine(pointValue, position));
        Debug.Log("Sprite Changed");
    }

    private IEnumerator ChangeSpriteCoroutine(int pointValue, Vector3 position)
    {
        transform.position = position;
        _spriteRenderer.sprite = GetSpriteByValue(pointValue);

        yield return new WaitForSeconds(0.5f);

        _spriteRenderer.sprite = null;
    }
    
    private Sprite GetSpriteByValue(int pointValue)
    {
        int index = IndexOf(_pointValues, pointValue);

        return _pointSprites[index];
    }

    private static int IndexOf(int[] pointValues, int pointValue) 
    {
        for (int i = 0; i < pointValues.Length; i++)
        {
            if (pointValue == pointValues[i])
                return i;
        }

        return 0;
    }

}