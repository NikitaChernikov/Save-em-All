using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject particles;
    [SerializeField] AudioSource point;
    [SerializeField] AudioSource hit;

    private NavMeshAgent agent;
    private Rigidbody[] childrenRb;
    private CharacterController character;
    private LevelController level;
    private CarMovement player;
    private bool goToCar = false;
    private Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        childrenRb = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
        level = FindObjectOfType<LevelController>();
        player = FindObjectOfType<CarMovement>();
    }

    private void Start()
    {
        foreach (Rigidbody rb in childrenRb)
        {
            rb.isKinematic = true;
            rb.tag = "npc_ragdoll";
        }
    }

    private void Update()
    {
        if (goToCar)
        {
            agent.SetDestination(player.transform.position);
            if (Vector3.Distance(transform.position, player.transform.position) < 5f)
            {
                //if (player.GetSpeed() < 10)
                //{
                //    point.Play();
                //    level.ChangeScore();
                //    Destroy(gameObject);
                //}
                //else
                //{
                //    hit.Play();
                //    Death(true);
                //}
            }
        }
    }

    public void Death(bool isPlayer)
    {
        agent.enabled = false;
        character.enabled = false;
        //foreach (Rigidbody rb in childrenRb)
        //{
        //    rb.isKinematic = false;
        //    if (isPlayer)
        //    {
        //        rb.AddForce(-player.transform.forward * player.GetSpeed() * 4, ForceMode.Impulse);
        //    }
        //    else
        //    {
        //        rb.AddExplosionForce(100, transform.position, -30);
        //    }
        //}
        anim.enabled = false;
        Destroy(gameObject, 3);
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particles.SetActive(false);
            anim.SetBool("isRun", true);
            goToCar = true;
            agent.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particles.SetActive(true);
            anim.SetBool("isRun", false);
            goToCar = false;
            agent.enabled = false;
        }
    }
}
