using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpCrystalBehaviour : MonoBehaviour
{
    [SerializeField] private float crystalRefreshFrequency = 2;
    private SpriteRenderer spriteRenderer;
    public bool bIsAvailable = true;
    private Sprite availableSprite;
    [SerializeField] private Sprite unavailableSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        availableSprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyThenRefresh()
    {
        if (bIsAvailable)
        {
            bIsAvailable = false;
            
            if (unavailableSprite)
            {
                spriteRenderer.sprite = unavailableSprite;
            }
            
            StartCoroutine(Refresh());
        }
    }

    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(crystalRefreshFrequency);
        bIsAvailable = true;
        spriteRenderer.sprite = availableSprite;
    }
}
