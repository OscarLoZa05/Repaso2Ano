using UnityEngine;

public class GroundSensor : MonoBehaviour
{

    public bool isGrounded;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }
}
