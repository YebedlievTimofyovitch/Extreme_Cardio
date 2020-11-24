using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    [SerializeField] private bool IsPooled = true;
    [SerializeField] float ZDistance = 30.0f;

    public void AddSection()
    {
        GameObject section = ObjectPoolManager.PoolerInstance.Get("Section");

        if (section != null)
        {
            section.transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z + ZDistance);
            section.SetActive(true);
        }
        else
            print("no section available");
    }


    public void RemoveSection()
    {
        if (!IsPooled)
        {
            Destroy(gameObject);
        }

        else if (IsPooled)
        {
            gameObject.SetActive(false);
        }
    }

    
}
