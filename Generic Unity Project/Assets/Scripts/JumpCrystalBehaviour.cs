using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class JumpCrystalBehaviour : MonoBehaviour
{
    [SerializeField] private float CrystalRefreshFrequency = 2;
    private SpriteRenderer SpriteRenderer;
    public bool bIsAvailable = true;
    private Sprite AvailableSprite;
    [SerializeField] private Sprite UnavailableSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        // get necessary components
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        AvailableSprite = SpriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // responsible for setting the crystal to not available
    public void DestroyThenRefresh()
    {
        if (bIsAvailable)
        {
            bIsAvailable = false;
            
            // change sprite
            if (UnavailableSprite)
            {
                SpriteRenderer.sprite = UnavailableSprite;
            }
            
            // after a set delay, refresh the availability
            StartCoroutine(Refresh());
        }
    }

    // responsible for setting the crystal to available
    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(CrystalRefreshFrequency);
        bIsAvailable = true;
        SpriteRenderer.sprite = AvailableSprite;
    }
}
