using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    public Camera mainCamera;
    public OVRHand leftHand;
    public OVRHand rightHand;
    public OVRSkeleton skeleton;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private float _step;
    
    // 검지손가락 핀칭 여부
    private bool _isIndexFingerPinching;

    private LineRenderer _lineRenderer;
    private Transform _p0;
    private Transform _p1;
    private Transform _p2;

    private Transform _handIndexTipTransform;
    
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 1.0f;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _step = 5.0f * Time.deltaTime;

        if (leftHand.IsTracked)
        {
            // 사용자가 검지를 사용하여 집고 있는지 확인
            _isIndexFingerPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            if (_isIndexFingerPinching)
            {
                _lineRenderer.enabled = true;
                // Animate cube smoothly next to left hand
                PinchCube();

                foreach (var bone in skeleton.Bones)
                {
                    if (bone.Id == OVRSkeleton.BoneId.Hand_IndexTip)
                    {
                        _handIndexTipTransform = bone.Transform;
                        break;
                    }
                }

                _p0 = transform; // 큐브의 트랜스폼
                _p2 = _handIndexTipTransform; // 손가락의 트랜스폼
                _p1 = mainCamera.transform; // 카메라의 트랜스폼
                _p1.position += mainCamera.transform.forward * 0.8f;

                DrawCurve(_p0.position, _p1.position, _p2.position);
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
    }
    
    private void PinchCube()
    {
        // 튜토리얼: 이 코드는 약 0.4m 거리에서 사용자의 왼손 옆에 큐브를 부드럽게 배치하고 회전시킵니다. 손의 실제 위치는 손목에 있습니다. 
        _targetPosition = leftHand.transform.position - leftHand.transform.forward * 0.4f;
        _targetRotation = Quaternion.LookRotation(transform.position - leftHand.transform.position);

        transform.position = Vector3.Lerp(transform.position, _targetPosition, _step);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _step);
    }
    
    private void DrawCurve(Vector3 startPoint, Vector3 inBetweenPoint, Vector3 endPoint)
    {
        _lineRenderer.positionCount = 200;
        float t = 0f;

        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            t += 0.005f;
            var interpolatedPoint = (1 - t) * (1 - t) * startPoint + 2 * (1 - t) * t * inBetweenPoint + t * t * endPoint;
            _lineRenderer.SetPosition(i, interpolatedPoint);
        }
    }
}
