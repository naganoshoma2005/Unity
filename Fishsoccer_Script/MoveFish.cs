
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveFish : MonoBehaviour
{
   
    private bool isControllable = true;

       private Camera mainCamera;
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    private Rigidbody rb;
    public float rotationSpeed = 5f; 
    
  
    [Header("操作設定")]
   
    public float mouseYSpeedFactor = 10f; 
    public float mouseInputDeadZone = 0.05f; 
   

    public float maxStamina = 100f;
    private float currentStamina;
    public float dashCost = 25f;
    public float staminaRegenRate = 15f;
    public Slider staminaSlider;

   
    public float shootForce = 15f;
    public float shootCooldown = 1.0f;
    public Collider shootTrigger;
    private bool canShoot = true;
    private Rigidbody targetBallRb;

  
    private bool isGuarding = false;
    public float guardRotationSpeed = 10f;
    private Quaternion targetGuardRotation;
    
  
    [Header("衝突による停止設定")]
    public float freezeDuration = 2f;


    public void SetControllable(bool controllable)
    {
        this.isControllable = controllable;

        if (!controllable && rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentStamina = maxStamina;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isControllable) return;

        HandleCursorLock();
        HandleGuardInput();

        if (!isGuarding && Input.GetMouseButtonDown(0) && canShoot && targetBallRb != null)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (!isControllable) return;

        if (isGuarding)
        {
            rb.linearVelocity = Vector3.zero;
            rb.rotation = Quaternion.Lerp(rb.rotation, targetGuardRotation, guardRotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            HandleMovement();
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }
    
    void HandleCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void HandleGuardInput()
    {
        if (Input.GetMouseButtonDown(1) && !isGuarding)
        {
            isGuarding = true;
            targetGuardRotation = rb.rotation * Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetMouseButtonUp(1) && isGuarding)
        {
            isGuarding = false;
        }
    }

    void HandleMovement()
    {
       
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0f;

        if (cameraForward.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward.normalized);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
       
        float mouseMoveX = Input.GetAxis("Mouse X");
        
        
        if (Mathf.Abs(mouseMoveX) < mouseInputDeadZone)
        {
            mouseMoveX = 0f;
        }
        
        
        float movementIntensity = Mathf.Abs(mouseMoveX);
        
        
        bool isDashing = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && movementIntensity > 0;

        float targetSpeed;
        if (isDashing)
        {
            targetSpeed = dashSpeed + movementIntensity * mouseYSpeedFactor; 
            currentStamina = Mathf.Max(0, currentStamina - dashCost * Time.fixedDeltaTime);
        }
        else
        {
            
            targetSpeed = moveSpeed * movementIntensity + (movementIntensity > 0 ? moveSpeed : 0f);

           
            if (currentStamina < maxStamina)
            {
                currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.fixedDeltaTime);
            }
        }
        
       
        Vector3 targetVelocity;
        
        if (movementIntensity > 0)
        {
           
            Vector3 movement = transform.forward;
            movement.y = 0f;
            targetVelocity = movement.normalized * targetSpeed;
        }
        else
        {
            
            targetVelocity = Vector3.zero;
        }
        
        
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * 10f);
    }

    void Shoot()
    {
        Vector3 forceDirection = transform.forward;
        targetBallRb.AddForce(forceDirection * shootForce, ForceMode.Impulse);
        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (other.bounds.Intersects(shootTrigger.bounds))
            {
                targetBallRb = other.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (targetBallRb != null && targetBallRb.gameObject == other.gameObject)
            {
                targetBallRb = null;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isControllable)
        {
            Destroy(collision.gameObject);
            StartCoroutine(FreezePlayerTemporarily());
        }
    }

    private IEnumerator FreezePlayerTemporarily()
    {
        Debug.Log("障害物に衝突！ " + freezeDuration + "秒間停止します。");
        SetControllable(false);
        yield return new WaitForSeconds(freezeDuration);
        Debug.Log("操作を再開します。");
        SetControllable(true);
    }
}