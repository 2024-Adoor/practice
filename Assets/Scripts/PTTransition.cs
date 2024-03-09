using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTTransition : MonoBehaviour
{
    [SerializeField]
    private OVRPassthroughLayer passthroughLayer;
    
    [SerializeField]
    private OVRManager ovrManager;
    
    private bool _onDarkenPassthrough = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePassthroughVisibility()
    {
        passthroughLayer.textureOpacity = passthroughLayer.textureOpacity == 0 ? 1 : 0;
        if (_onDarkenPassthrough) passthroughLayer.edgeRenderingEnabled = passthroughLayer.textureOpacity != 0;
    }
    
    public void TogglePassthroughLayerEnabled()
    {
        passthroughLayer.enabled = !passthroughLayer.enabled;
    }
    
    public void TogglePassthroughEnabled()
    {
        ovrManager.isInsightPassthroughEnabled = !ovrManager.isInsightPassthroughEnabled;
    }
    
    public void DarkenPassthrough()
    {
        _onDarkenPassthrough = true;
        passthroughLayer.edgeRenderingEnabled = true;
        passthroughLayer.SetColorMapControls(contrast: 0, brightness: -1f);
    }
}
