using UnityEngine;

public class BasicMove : MonoBehaviour {
    public Animator animator;
    public Rigidbody2D rb;

    [SerializeField]
    private float speed = 0.7f;


    // Update is called once per frame
    void Update() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);

    }
}
