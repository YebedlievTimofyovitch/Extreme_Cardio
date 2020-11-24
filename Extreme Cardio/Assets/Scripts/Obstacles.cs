using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleRow
{
    public bool HasCoins = false;
    public bool IsActive = false;
    public Vector3[] RowPositions = new Vector3[3] { Vector3.zero, Vector3.zero, Vector3.zero };
}

public class Obstacles : MonoBehaviour
{
    [SerializeField] private List<ObstacleRow> Rows = null;

    [SerializeField] private string[] obstacleTags = new string[4] { "", "", "" , ""};

    private List<GameObject> calledObstacles;

    void Awake()
    {
        
    }

    private void OnEnable()
    {
        calledObstacles = new List<GameObject>();

        foreach(ObstacleRow row in Rows)
        {

            row.IsActive = RandBool(0.6f);


            if(row.IsActive)
            {
                row.HasCoins = RandBool(0.50f);
                string[] obstacales = PrepareObstArray(row.HasCoins);

                for(int i = 0 ; i<3 ; i++)
                {
                    GameObject objobs = ObjectPoolManager.PoolerInstance.Get(obstacales[Random.Range(0,3)]);
                    if (objobs != null)
                    {
                        calledObstacles.Add(objobs);

                        objobs.transform.position = transform.position + row.RowPositions[i];
                        objobs.transform.rotation = transform.rotation;
                        objobs.SetActive(true);
                    }
                }
            }
        }
    }

    private bool RandBool(float chance)
    {
        float boolRange = Random.Range(0.0f, 1.0f);

        if(boolRange <= chance)
        {
            return true;
        }
        return false;
    }

    private string[] PrepareObstArray(bool hc)
    {  
        string[] obsts = new string[4];

        if(!hc)
        {
            for(int i = 0 ; i<2 ; i++)
            {
                obsts[i] = obstacleTags[i];
            }
        }

        else if(hc)
        {
            for (int i = 0; i < 3; i++)
            {
                obsts[i] = obstacleTags[i];
            }
        }

        return obsts;
    }

    void OnDisable()
    {
            foreach(GameObject obj in calledObstacles)
            {
            if (obj != null)
            {
                obj.SetActive(false);

                ObjectPoolManager.PoolerInstance._PooledObjects.Add(obj);
            }
            }
        
    }
}
