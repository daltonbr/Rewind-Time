using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TimeBody : MonoBehaviour
{
	public float RecordTime = 5f;

	private bool _isRewinding = false;
	private List<PointInTime> _pointsInTime;
	private Rigidbody _rigidbody;
    private int _framesToRecord;

    void Awake ()
	{
		_pointsInTime = new List<PointInTime>();
		_rigidbody = GetComponent<Rigidbody>();
        _framesToRecord = Mathf.RoundToInt(RecordTime / Time.fixedDeltaTime);
    }
		
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Return))
			StartRewind();
		if (Input.GetKeyUp(KeyCode.Return))
			StopRewind();
	}

	void FixedUpdate ()
	{
		if (_isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind ()
	{
		if (_pointsInTime.Count > 0)
		{
			PointInTime pointInTime = _pointsInTime[0];
            this.transform.position = pointInTime.Position;
		    this.transform.rotation = pointInTime.Rotation;			
			_pointsInTime.RemoveAt(0);
		} else
		{
			StopRewind();
		}
	}

	void Record ()
	{
		if (_pointsInTime.Count > _framesToRecord)
		{
			_pointsInTime.RemoveAt(_pointsInTime.Count - 1);
		}

		_pointsInTime.Insert(0, new PointInTime(this.transform, _rigidbody));
	}

	public void StartRewind ()
	{
		_isRewinding = true;
		_rigidbody.isKinematic = true;
	}

	public void StopRewind ()
	{
		_isRewinding = false;
		_rigidbody.isKinematic = false;
        ReapplyForces();
	}

    public void ReapplyForces()
     {
        _rigidbody.position = _pointsInTime[0].Position;
        _rigidbody.rotation = _pointsInTime[0].Rotation;
        _rigidbody.velocity = _pointsInTime[0].Velocity;
        _rigidbody.angularVelocity = _pointsInTime[0].AngularVelocity;
    }﻿
}
