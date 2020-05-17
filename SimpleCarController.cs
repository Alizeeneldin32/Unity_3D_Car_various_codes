using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
	void carSounds()
	{
		CarSound[0].Stop();
		CarSound[1].volume = 0.0f;
		CarSound[2].volume = 0.0f;
		CarSound[3].volume = 0.0f;
		CarSound[4].volume = 0.0f;
		CarSound[5].volume = 0.0f;
		CarSound[6].volume = 0.0f;
		CarSound[7].volume = 0.0f;
		CarSound[8].volume = 0.0f;
		if (currentSpeed < 10)
		{
			if (!CarSound[1].isPlaying)
			{
				CarSound[1].Play();
			}
			CarSound[1].volume = 0.2f;
		}
		if (currentSpeed > 10 && currentSpeed <30)
		{
			if (!CarSound[2].isPlaying)
			{
				CarSound[2].Play();
			}
			CarSound[2].volume = 1f;
		}
		if (currentSpeed > 30 && currentSpeed < 50)
		{
			if (!CarSound[3].isPlaying)
			{
				CarSound[3].Play();
			}
			CarSound[3].volume = 0.8f;
		}
		if (currentSpeed > 50 && currentSpeed < 70)
		{
			if (!CarSound[4].isPlaying)
			{
				CarSound[4].Play();
			}
			CarSound[4].volume = 1f;
		}
		if (currentSpeed > 70 && currentSpeed < 90)
		{
			if (!CarSound[5].isPlaying)
			{
				CarSound[5].Play();
			}
			CarSound[5].volume = 0.8f;
		}
		if (currentSpeed > 90 && currentSpeed < 110)
		{
			if (!CarSound[6].isPlaying)
			{
				CarSound[6].Play();
			}
			CarSound[6].volume = 1f;
		}
		if (currentSpeed > 110 && currentSpeed < 130)
		{
			if (!CarSound[7].isPlaying)
			{
				CarSound[7].Play();
			}
			CarSound[7].volume = 0.8f;
		}
		if (currentSpeed > 130 && currentSpeed < 150)
		{
			if (!CarSound[8].isPlaying)
			{
				CarSound[8].Play();
			}
			CarSound[8].volume = 0.8f;
		}
	}

	void enableSound(int i)
	{
		if (!CarSound[i].isPlaying)
		{
			CarSound[i].Play();
		}
		CarSound[i].volume = 1f;
	}

	void disableSound(int i)
	{
		if (CarSound[i].isPlaying)
		{
			CarSound[i].Stop();
		}
		CarSound[i].volume = 0f;
	}
	void changeCamera()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			if (!farCamera.enabled)
			{
				farCamera.enabled = true;
				inCarCamera.enabled = false;
			}
			else
			{
				farCamera.enabled = false;
				inCarCamera.enabled = true;
			}
		}
	}
	void Start()
	{
		COM = GameObject.Find("Col");
		GetComponent<Rigidbody>().centerOfMass = new Vector3(COM.transform.localPosition.x * transform.localScale.x, COM.transform.localPosition.y * transform.localScale.y, COM.transform.localPosition.z * transform.localScale.z);

		CarSound[0].Play();
		new WaitForSeconds(10);
		CarSound[0].volume = 1f;
		nearCamera.enabled = false;
		farCamera.enabled = true;
		inCarCamera.enabled = false;
	}
	public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical");
	}

	private void Steer()
	{
		if (currentSpeed > 80)
		{
			maxSteerAngle = 20 - (currentSpeed / 10);
		}
		else
		{
			maxSteerAngle = 20;
		}
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		FL.steerAngle = m_steeringAngle;
		FR.steerAngle = m_steeringAngle;
	}
	void resetPos()
	{

		if (Input.GetKeyDown(KeyCode.R))
		{

			gameObject.transform.position= new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
			gameObject.transform.rotation= Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);
		}
	}
private void Accelerate()
	{
		FL.brakeTorque = 0;
		FR.brakeTorque = 0;
		RL.brakeTorque = 0;
		RR.brakeTorque = 0;
		FL.motorTorque = m_verticalInput * motorForce;
		FR.motorTorque = m_verticalInput * motorForce;
		if (Input.GetKey(KeyCode.DownArrow)&&currentSpeed>10)
		{
			enableSound(10);
			FL.brakeTorque = brakeTorque;
			FR.brakeTorque = brakeTorque;
		}
		else
		{
			disableSound(10);
			FL.brakeTorque = 0;
			FR.brakeTorque = 0;
		}
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(FL, frontDriverT);
		UpdateWheelPose(FR, frontPassengerT);
		UpdateWheelPose(RL, rearDriverT);
		UpdateWheelPose(RR, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}
	void HandBrakes()
	{

		RL.brakeTorque = brakeTorque;
		RR.brakeTorque = brakeTorque;
		FL.brakeTorque = brakeTorque;
		FR.brakeTorque = brakeTorque;
	}

	private void Update()
	{
		currentSpeed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
		GetInput();
		Steer();
		Accelerate();
		carSounds();
		UpdateWheelPoses();
		if (Input.GetButton("Jump"))
		{
			HandBrakes();
			enableSound(9);
		}
		else disableSound(9);
		changeCamera();
		resetPos();
	}

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public Camera nearCamera, farCamera, inCarCamera;
	public WheelCollider FL, FR;
	public WheelCollider RL, RR;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle;
	public float motorForce;
	public int brakeTorque;
	public float currentSpeed;
	public int maxSpeed=300;
	private GameObject COM;
	public bool handBraked;
	public List<AudioSource> CarSound;
}
