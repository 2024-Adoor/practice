using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    /// <summary>
    /// 현재 씬의 카메라
    /// </summary>
    public Camera sceneCamera;

    /// <summary>
    /// 카메라의 위치
    /// </summary>
    private Vector3 _targetPosition;
    
    /// <summary>
    /// 카메라의 회전
    /// </summary>
    private Quaternion _targetRotation;

    // Cube 게임오브젝트의 애니메이션에 도움을 준다고 한다.
    private float _step;
    
    // Start is called before the first frame update
    void Start()
    {
        // 카메라 앞 3m 위치에 게임오브젝트(이 경우에는 큐브)를 배치
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _step = 5.0f * Time.deltaTime;

        // 값이 true이면 사용자가 누른 것(이 경우 RIndexTrigger, 검지로 누르는 버튼)
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            CenterCube();
        }

        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft))
        {
            transform.Rotate(0, 5.0f * _step, 0);
        }

        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight))
        {
            transform.Rotate(0, -5.0f * _step, 0);
        }
        
        // OVRInput.GetUp()은 사용자가 버튼을 뗄 때 true.
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            // SetControllerVibration()은 컨트롤러 진동을 설정하는 함수.
            // 알아두어야 할 것:
            // amplitude 매개변수의 범위는 (0.0~1.0f)
            // 햅틱을 활성화하려면 frequency를 1로 설정한다.
            // controllerMask는 진동을 설정할 컨트롤러를 나타낸다.(왼쪽인지 오른쪽인지)
            // 진동을 종료하려면 frequency와 amplitude를 모두 0으로 설정.
            // 마지막 입력 2초 후에는 진동이 자동으로 종료된다. (0으로 안 바꿔도 그렇다는 얘기겠죠?)
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
        
        // 왼쪽 컨트롤러 핸드 트리거(엄지 스틱)에 입력이 있는 경우에
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.0f)
        {
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
    }
    
    private void CenterCube()
    {
        // 튜토리얼 曰 "이 함수는 선택 사항이지만 시각적으로 매력적인 큐브의 재배치 및 방향 조정에 애니메이션을 적용하는 접근 방식입니다."
        // "centerCube() 함수는 사용자 앞, 뷰포트 중앙에 큐브 GameObject를 부드럽게 배치하고 사용자의 머리 자세(카메라)에 따라 큐브를 회전시킵니다."
        
        _targetPosition = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
        _targetRotation = Quaternion.LookRotation(transform.position - sceneCamera.transform.position);
        
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _step);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _step);
    }
}
