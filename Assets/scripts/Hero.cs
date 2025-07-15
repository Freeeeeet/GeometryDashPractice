using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Hero : MonoBehaviour
{
    public float jumpForce = 15f;
    public float rotationSpeed = -500f;
    public float positionX = -10f;
    public Rigidbody2D rb;
    private bool isGrounded = true;
    private HeroInputActions inputActions;
    private int score = 0;
    public GameObject loseMenu;
    public GameObject winMenu;
    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrounded)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        }
        transform.position = new Vector2(positionX, transform.position.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            score++;
            Debug.Log("Score: " + score);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Level finished — stopping environment and hero.");
            // Обновляем текст с текущим счетом
            if (scoreText != null)
            {
                scoreText.text = "Your score " + score;
            }
            else
            {
                Debug.LogWarning("Score TextMeshPro component not found.");
            }

            // Находим объект с компонентом Environment и останавливаем его
            Environment env = FindFirstObjectByType<Environment>();
            if (env != null)
            {
                env.environment_speed = 0f;
                OnDisable(); // Отключаем действия ввода
            }
            Debug.Log("Your score: " + score);

            winMenu.SetActive(true); // Показываем меню победы
        }
        else if (collision.gameObject.CompareTag("Kill"))
        {
            Debug.Log("You lose — stopping environment.");

            // Находим объект с компонентом Environment и останавливаем его
            Environment env = FindFirstObjectByType<Environment>();
            if (env != null)
            {
                env.environment_speed = 0f;
                Destroy(gameObject); // Уничтожаем героя
            }
            loseMenu.SetActive(true); // Показываем меню проигрыша
        }
    }

    void Awake()
    {
        inputActions = new HeroInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Gameplay.Jump.performed += _ => OnJump();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void OnJump()
    {
        if (rb != null)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Debug.Log("Hero jumps!");
                isGrounded = false;
            }
            else
            {
                Debug.Log("Cannot jump, not grounded.");
            }

        }


        else
        {
            Debug.LogWarning("Rigidbody2D component not found on Hero object.");
        }
    }


}
