using UnityEngine;
using Cinemachine;

public class CinemaMachinePOVExtension : CinemachineExtension
{
    [SerializeField]
    private float _horizontalSpeed = 10f;

    [SerializeField]
    private float _verticalSpeed = 10f;

    [SerializeField]
    private float clampAngle = 80f;

    private InputManager _inputManager;
    private Vector3 _startRotating;

    protected override void Awake()
    {
        _inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime) 
    { 
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (_startRotating == null) _startRotating = transform.localRotation.eulerAngles;
                Vector2 deltaInput = _inputManager.GetMouseDelta();
                _startRotating.x += deltaInput.x * _verticalSpeed * Time.deltaTime;
                _startRotating.y += deltaInput.y * _horizontalSpeed * Time.deltaTime;
                _startRotating.y = Mathf.Clamp(_startRotating.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-_startRotating.y, _startRotating.x, 0f);
            }
        }
    }
}
