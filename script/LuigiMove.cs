using UnityEngine;
using UnityEngine.InputSystem;

public class LuigiMove : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    public InputActionAsset actions;
    private InputAction xAxis;

    void Awake()
    {
        xAxis = actions.FindActionMap("LuigiWalk").FindAction("XAxis");
    }

    void OnEnable()
    {
        actions.FindActionMap("LuigiWalk").Enable();
    }

    void OnDisable()
    {
        actions.FindActionMap("LuigiWalk").Disable();
    }

    void Update()
    {
        MoveX();
    }

    private void MoveX()
    {
        float xMove = xAxis.ReadValue<float>();
        transform.position += speed * Time.deltaTime * xMove * transform.right;
    }
}