using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float damageRadius = 10;
    [SerializeField] private GameObject flyingTrail;
    [SerializeField] private GameObject buildingExplosion;
    [SerializeField] private GameObject hitTheGround;
    [SerializeField] private AudioSource flying;

    private bool isExplode;
    private GameObject controlButtons;

    private void Awake()
    {
        controlButtons = GameObject.FindGameObjectWithTag("ControlButtons");
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        if (Physics.Raycast(ray, out hit, 20))
        {
            if (hit.collider.CompareTag("Building"))
            {
                if (!isExplode)
                {
                    isExplode = true;
                    GameObject buf = Instantiate(buildingExplosion);
                    buf.transform.position = hit.transform.position;
                    buf.transform.rotation = transform.rotation;
                    Destroy(buf, 5);
                    Destroy(hit.collider.gameObject, 1f);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        flying.enabled = false;
        GameObject buf = Instantiate(hitTheGround, transform.position, hitTheGround.transform.rotation);
        Destroy(buf, 2);
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider hit in hitObjects)
        {
            if (hit.CompareTag("npc_ragdoll"))
            {
                hit.transform.root.GetComponent<NPC>().Death(false);
            }
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce: 1000 ,transform.position, damageRadius);
                //if (transform.position.z > hit.transform.position.z)
                //{
                //    hit.GetComponent<Rigidbody>().AddForce(-hit.transform.forward * 50, ForceMode.Impulse);
                //}
                //if (transform.position.z < hit.transform.position.z)
                //{
                //    hit.GetComponent<Rigidbody>().AddForce(hit.transform.forward * 50, ForceMode.Impulse);
                //}
                Time.timeScale = 0.5f;
                controlButtons.SetActive(false);
                FindObjectOfType<CarMovement>().enabled = false;
                FindObjectOfType<LevelController>().Invoke("GameLost", 2);
            }
        }
        flyingTrail.transform.parent = null;
        flyingTrail.transform.position = new Vector3(flyingTrail.transform.position.x, 0, flyingTrail.transform.position.z);
        //Destroy(gameObject);
    }
}
