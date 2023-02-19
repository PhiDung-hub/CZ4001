using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI targetRemainingText;
    public TextMeshProUGUI timeElapsed;
    public static uint ammoCount;

    private Rigidbody rb;
    [SerializeField]
    private int targetRemaining;
    const int TOTAL_TARGET = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRemaining = TOTAL_TARGET;
        SetCountText();
    }

    void SetCountText()
    {
        targetRemainingText.text = "Count: " + targetRemaining.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);
            targetRemaining += 1;
            SetCountText();
        }

    }
}
