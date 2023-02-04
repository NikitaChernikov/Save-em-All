using UnityEngine;

public class NPCDuplicateChecker : MonoBehaviour
{
    public bool isPlaceTaken()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("NPC"))  return true;
        }
        return false;
    }
}
