using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public Rigidbody rigid;

    //����
    private bool isJump;
    public float jumpPower;
    //���ӸŴ���
    public GameManager gameManager;
    //�ִϸ�����
    private Animator playerAnimator;
    //�Ѿ�
    public GameObject bullet;
    public Transform weaponTransform;
    public float rotateSpeed;
    public float bulletCoolTime;
    public Weapon weapon;

    //ī�޶�
    private Camera mainCamera;
    private Vector2 input;
    private Vector3 targetDirection;
    private float turnSpeedMultiplier = 1.0f;



    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        StartCoroutine(Fire(bulletCoolTime));
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            playerAnimator.SetBool("isJumping", isJump);
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);

        }
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        //Vector3 dir = new Vector3(h, 0, v);
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        UpdateTargetDirection();

        if (input == Vector2.zero)
        {
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isRunning", false);

        }
        else
        {
            if (targetDirection.magnitude > 0.1f)
            {
                Vector3 lookDirection = targetDirection.normalized;
                Quaternion freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
                
                var differenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y; //ī�޶� ���� ������ �÷��̾ ���� ���� ����
                var eulerY = transform.eulerAngles.y; //���� Transform�� ���Ϸ� ���� ����

                if (differenceRotation < 0 || differenceRotation >0)
                {
                    eulerY = freeRotation.eulerAngles.y;
                }

                var euler = new Vector3(0, eulerY,0);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), rotateSpeed * turnSpeedMultiplier);
            }
            playerAnimator.SetBool("isWalking", true);
            rigid.AddForce(targetDirection * speed, ForceMode.Impulse);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("isRunning", true);
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isJumping", false);
                rigid.AddForce(targetDirection * speed *2, ForceMode.Impulse);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("isRunning", false);
                playerAnimator.SetBool("isWalking", true);
                rigid.AddForce(targetDirection * speed, ForceMode.Impulse);
            }

        }
        //Vector3 dir = new Vector3(input.x, 0, input.y);
        //transform.Translate(new Vector3(h,0,v) * speed); �긦 ���� x�� y���� ������. rigid.addforce�� �Ἥ ���� ������ �̴� �������� ����.
        //if (input.x==0 && input.y==0)
        //{
        //    playerAnimator.SetBool("isWalking", false);
        //}
        //else
        //{
        //    playerAnimator.SetBool("isWalking", true);

        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(t), rotateSpeed);

        //}
    }
    public void UpdateTargetDirection()
    {
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        forward.y= 0;
        
        var right = mainCamera.transform.TransformDirection(Vector3.right);

        targetDirection = (input.x * right) + (input.y * forward);
       
    }

    private IEnumerator Fire(float coolTime)
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Instantiate(bullet, weaponTransform.position, weaponTransform.rotation);
                weapon.PlayWeaponSound();
                yield return new WaitForSeconds(coolTime);
            }
            yield return null; //update���� �����ִ� ��
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag =="Floor") 
        {
            isJump = false;

            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isJumping", isJump);
            playerAnimator.SetBool("isRunning", false);
        }

        //if (other.gameObject.tag == "Item")
        //{
        //    gameManager.itemCount++;
        //    Destroy(other.gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            gameManager.itemCount++;
            gameManager.GetItem(gameManager.itemCount); //UI�� ��Ÿ���� ����.
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Finish")
        {
            gameManager.NextStage(); //UI�� ��Ÿ���� ����.
        }

        if (other.gameObject.tag == "Fall")
        {
            SceneManager.LoadScene(gameManager.stage);
        }
    }
}
