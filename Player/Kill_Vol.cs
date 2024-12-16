using UnityEngine;
using UnityEngine.SceneManagement;
public class Kill_Vol : MonoBehaviour
{
    [SerializeField] private Josh_Player_Move plr;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            plr.Die();
        }
    }
}
