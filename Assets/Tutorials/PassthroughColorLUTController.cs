using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughColorLUTController : MonoBehaviour
{
    [SerializeField]
    private Texture2D _2dColorLUT;
    
    public Texture2D ColorLut2D => _2dColorLUT;

    private OVRPassthroughColorLut _passthroughColorLut;
    private OVRPassthroughLayer _passthroughLayer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
        if (ovrCameraRig == null)
        {
            Debug.LogError("Scene does not contain an OVRCameraRig");
            return;
        }

        _passthroughLayer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
        if (_passthroughLayer == null)
        {
            Debug.LogError("OVRCameraRig does not contain an OVRPassthroughLayer component");
            return;
        }

        _passthroughColorLut = new OVRPassthroughColorLut(_2dColorLUT, false);
        _passthroughLayer.SetColorLut(_passthroughColorLut, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
