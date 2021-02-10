using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
public class TPRB : MonoBehaviour
{
	public float turnSpeed = 10, gravity = 2, JumpPower = 5, detectionOffset = 2;
	[HideInInspector] public bool ground;
	[HideInInspector] public float turn = 0, speed = 0, t = 0, crouch, ccheight;
	[HideInInspector] public Vector3 move, axis, ccenter;
	CapsuleCollider cc;
	Rigidbody rb;
	RaycastHit hit;
	Transform cam;
	void Start()
	{
		cam = Camera.main.transform;
		cc = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
		ccheight = cc.height;
		ccenter = cc.center;
	}
	void FixedUpdate()
	{
		Physics.gravity = Vector3.down * 9.8f * gravity;
		t = Mathf.Clamp(Input.GetKeyDown(KeyCode.Space) ? 1 : t - Time.deltaTime, 0, 1);
		MovementHandle();
		GroundHandle();
		CrouchSetup();
	}
	void GroundHandle()
	{
		ground = t < 0.5f ? Physics.Raycast(cc.bounds.center, Vector3.down, out hit) && hit.distance < detectionOffset : false;//Raycasting will only be enabled when t (airtime) is smaller than 0.5f;
		rb.useGravity = !ground;
		rb.velocity = new Vector3(rb.velocity.x, ground ? Mathf.Lerp(rb.velocity.y, (hit.point.y - transform.position.y) * 15, Time.deltaTime * 12) : (t > 0.75f) ? JumpPower + 5 : rb.velocity.y, rb.velocity.z);
	}//use IK support to hide performance.
	void MovementHandle()
	{
		axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		move = Quaternion.LookRotation(new Vector3(cam.forward.x, 0, cam.forward.z)) * axis;
		turn = (axis.magnitude != 0) ? Vector3.Cross(transform.forward, move).y : 0;
		speed = (axis.magnitude != 0) ? Input.GetKey(KeyCode.LeftShift) ? 1 : 0.5f : 0;
		if (axis.magnitude != 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * turnSpeed);
	}
	void CrouchSetup()
	{
		if (ground) crouch = Input.GetKey(KeyCode.C) ? 0f : (Input.GetKey(KeyCode.V) ? 0.5f : 1f);// 1 = crouching calculated procedural amount.
		cc.center = ccenter * (0.5f + crouch / 2f);
		cc.height = ccheight * (0.5f + crouch / 2f);
	}
}