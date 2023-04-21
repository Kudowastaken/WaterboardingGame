using UnityEngine;

public class Spike : MonoBehaviour
{
    [Header("Spike Settings")]
    [SerializeField] float force = 200;
    [SerializeField] float damage = 10;

    [Header("Cached References")]
    PlayerController player;
    Rigidbody playerRB;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerRB = player.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!player.IsDashing)
            {
                SpikeCollision();
            }
            else
            {
                player.IsDashing  = false;
                playerRB.velocity = Vector3.zero;
                SpikeCollision();
            }
        }

        void SpikeCollision()
        {
            Debug.Assert(damage > 0, "Damage is 0! Please set a positive value.");
            Debug.Assert(force > 0, "Force is 0! Please set a positive value.");

            player.CurrentHealth -= damage;
            playerRB.AddForce(Vector3.up * force, ForceMode.Impulse);
            Debug.Log($"Player took damage from a spike! \n New Health: {player.CurrentHealth}");
        }
    }
}