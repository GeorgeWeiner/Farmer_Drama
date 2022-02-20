using UnityEngine;

public class MM_BackgroundColorLoop : MonoBehaviour
{
    [SerializeField] 
    private float colorLoopSpeed;
    private float hsvValue;
    
    private void Update()
    {
        LoopThroughColors();
    }

    private void LoopThroughColors()
    {
        hsvValue = Mathf.MoveTowards(hsvValue, 1f, colorLoopSpeed * Time.deltaTime);
        
        if (hsvValue >= 0.99f) hsvValue = 0f;
        if (Camera.main != null) Camera.main.backgroundColor = Color.HSVToRGB(hsvValue, .7f, .7f);
    }
}
