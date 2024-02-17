using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughPracticeMain : MonoBehaviour
{
    private OVRPassthroughLayer _passthroughLayer;

    private void Awake()
    {
        _passthroughLayer = gameObject.GetComponent<OVRPassthroughLayer>();
    }

    private void Start()
    {
        bool isPassthroughRecommended = OVRManager.IsPassthroughRecommended();
        Debug.Log($"Passthrough is {(isPassthroughRecommended ? string.Empty : "not")} recommended");
        
#if UNITY_EDITOR
        // 에디터에서는 OVRManager.IsPassthroughRecommended()이 항상 false를 반환하는 것 같다.
        // 따라서 테스트를 위해 강제로 true로 설정한다.
        isPassthroughRecommended = true;
#endif
        if (isPassthroughRecommended)

        {
            _passthroughLayer.enabled = true;

            // 카메라의 배경을 투명하게 합니다
            OVRCameraRig ovrCameraRig = GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>();
            var centerCamera = ovrCameraRig.centerEyeAnchor.GetComponent<Camera>();
            centerCamera.clearFlags = CameraClearFlags.SolidColor;
            centerCamera.backgroundColor = Color.clear;

            // VR 배경 요소가 비활성화되어 있는지 확인하세요
        }
        else
        {
            _passthroughLayer.enabled = false;

            // VR 배경 요소가 활성화되어 있는지 확인하세요
        }
    }

    void Update()
    {
    }
}
