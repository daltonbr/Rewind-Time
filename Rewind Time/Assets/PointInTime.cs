using UnityEngine;

public struct PointInTime
{
	public readonly Vector3 Position;
	public readonly Quaternion Rotation;
    public readonly Vector3 Velocity;
    public readonly Vector3 AngularVelocity;
    
	public PointInTime (Transform transform, Rigidbody rigidbody)
	{
	    Position = transform.position;
	    Rotation = transform.rotation;
	    Velocity = rigidbody.velocity;
	    AngularVelocity = rigidbody.angularVelocity;
	}
    
    public PointInTime(Transform transform)
    {
        Position = transform.position;
        Rotation = transform.rotation;
        Velocity = Vector3.zero;
        AngularVelocity = Vector3.zero;
    }
    
}
