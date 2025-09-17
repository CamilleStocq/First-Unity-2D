using UnityEngine;

public class SpeenyMove : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float sens = 1f; // vers la droite

    private void Update()
    {
        // transform.position += Time.deltaTime * speed * transform.right;

        Vector3 move = speed * sens * Time.deltaTime * transform.right;
        transform.Translate(move);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TurnBlock"))
        {
            sens *= -1;

            Vector3 flip = transform.localScale;
            flip.x *= -1;
            transform.localScale = flip; 
        }
    }
}
