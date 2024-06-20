using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class exam06_player : MonoBehaviour
{
    public float speed = 1.0f; // 이동속도
    Vector2 velocity;
    new Rigidbody2D rigidbody;
    Animator animator;
    public float jumpForce = 5.0f; // 점프력
    private bool isGrounded = true; // 점프를 위해서는 Ground에 닿아있어야 함
    public float jumpDelay = 0.5f; // 점프 딜레이 시간
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float _hozInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        velocity = new Vector2(_hozInput * speed, rigidbody.velocity.y);

        if(_hozInput > 0) {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if(_hozInput < 0) {
            transform.rotation = Quaternion.Euler(0,180,0);
        }

        if (velocity.x != 0) {
            animator.SetBool("isWalk",true);
        }
        else {
            animator.SetBool("isWalk",false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) {
            StartCoroutine(JumpWithDelay());
        }

        // y 좌표에 따른 애니메이션 설정
        if (rigidbody.velocity.y > 0) {
            animator.SetBool("isJump", true);
            animator.SetBool("isFalling", false);
        } else if (rigidbody.velocity.y < 0) {
            animator.SetBool("isJump", false);
            animator.SetBool("isFalling", true);
        }
        else {
            animator.SetBool("isJump", false);
            animator.SetBool("isFalling", false);
        }

    }

    void FixedUpdate() {
        rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
            animator.SetBool("isJump", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrounded", false);
        }
    }

     IEnumerator JumpWithDelay()
    {
        // 도약 모션 취한 뒤 점프 모션이 나가도록 딜레이 설정
        animator.SetBool("isGrounded", true);

        // 점프 딜레이
        yield return new WaitForSeconds(jumpDelay);

        // 점프 시작
        isGrounded = false;
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
