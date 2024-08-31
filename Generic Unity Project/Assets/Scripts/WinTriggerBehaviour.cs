using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerBehaviour : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private Sprite BeforeWinSprite;
    [SerializeField] private Sprite WinSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        // get necessary components
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        BeforeWinSprite = SpriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // update sprite 
    public void UpdateSprite()
    {
        if (WinSprite)
        {
            SpriteRenderer.sprite = WinSprite;
        }
    }
}
