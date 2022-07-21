using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
/// </summary>


[ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu

public class CinemachineRotateYAxis : CinemachineExtension
{
    [Tooltip("Lock the camera's Z position to this value")]
    public float yAxisMin = -190;
    public float yAxisMax = -170;
    public float xRot = 0;
    public float yRot = 0;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Aim)
        {
            Vector3 rot = state.RawOrientation.eulerAngles;
            //rot.y = Mathf.Clamp(rot.y, yAxisMin, yAxisMax);

            rot.x = xRot;
            rot.z = yRot;

            if (rot.y > yAxisMax)
            {
                Debug.Log("over bounds" + rot);

            }

            state.RawOrientation = Quaternion.Euler(rot);
        }
    }
}
