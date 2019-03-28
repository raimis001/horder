using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;

	Rigidbody rb;
	public float speed;
	public float jumpSpeed = 2;

	private void Awake()
	{
		instance = this;
	}

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate()
	{
        if (GameManager.instance.IsGameModeActive())
        {
            float mH = Input.GetAxis("Horizontal");
            float mV = Input.GetAxis("Vertical");
            Vector3 move = Camera.main.transform.rotation * new Vector3(mH, 0, mV) * speed;
            move.y = rb.velocity.y;
            rb.velocity = move;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(0, jumpSpeed, 0);
            }
        }
	}
	private void Update()
	{
		transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
	}
}


