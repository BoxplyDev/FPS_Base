using UnityEngine;
using System.Collections;

/// <summary>
/// Utility class for working with planes relative to a camera.
/// </summary>
public static class CameraPlane
{
  /// <summary>
	/// Returns world space position at a given viewport coordinate for a given depth.
	/// </summary>
	public static Vector3 ViewportToWorldPlanePoint (Camera theCamera, float zDepth, Vector2 viewportCord)
	{
		Vector2 angles = ViewportPointToAngle (theCamera, viewportCord);
		float xOffset = Mathf.Tan (angles.x) * zDepth;
		float yOffset = Mathf.Tan (angles.y) * zDepth;
		Vector3 cameraPlanePosition = new Vector3 (xOffset, yOffset, zDepth);
		cameraPlanePosition = theCamera.transform.TransformPoint (cameraPlanePosition);
		return cameraPlanePosition;
	}
	
	public static Vector3 ScreenToWorldPlanePoint (Camera camera, float zDepth, Vector3 screenCoord)
	{
		var point = Camera.main.ScreenToViewportPoint (screenCoord);
		return ViewportToWorldPlanePoint (camera, zDepth, point);
	}
	
	/// <summary>
	/// Returns X and Y frustum angle for the given camera representing the given viewport space coordinate.
	/// </summary>
	public static Vector2 ViewportPointToAngle (Camera cam, Vector2 ViewportCord)
	{
		float adjustedAngle = AngleProportion(cam.fieldOfView/2, cam.aspect) * 2;
		float xProportion = ((ViewportCord.x - .5f)/.5f);
		float yProportion = ((ViewportCord.y - .5f)/.5f);
		float xAngle = AngleProportion(adjustedAngle/2, xProportion) * Mathf.Deg2Rad;
		float yAngle = AngleProportion(cam.fieldOfView/2, yProportion) * Mathf.Deg2Rad;
		return new UnityEngine.Vector2 (xAngle, yAngle);
	}
	
	/// <summary>
	/// Distance between the camera and a plane parallel to the viewport that passes through a given point.
	/// </summary>
	public static float CameraToPointDepth (Camera cam, Vector3 point)
	{
		Vector3 localPosition = cam.transform.InverseTransformPoint (point);
		return localPosition.z;
	}
	
	public static float AngleProportion (float angle, float proportion)
	{
		float oppisite = Mathf.Tan (angle * Mathf.Deg2Rad);
		float oppisiteProportion = oppisite * proportion;
		return Mathf.Atan (oppisiteProportion) * Mathf.Rad2Deg;
	}
}
