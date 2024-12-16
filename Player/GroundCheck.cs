using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Josh_Player_Move playerMovement;

    private void Awake()
    {
        playerMovement = GetComponentInParent<Josh_Player_Move>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            playerMovement.isGrounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerMovement.isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerMovement.isGrounded = false;
        }
    }

}
