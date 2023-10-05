using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySendingEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject lineConnection;

    public static EnemySendingEffect instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createALine(GameObject objA, GameObject objB)
    {
        /*spawn a prefab image "lineConnection" as angleBar*/
        GameObject angleBar = Instantiate(lineConnection, objB.transform.position, Quaternion.identity);
        /**/
        /*calculate angle*/
        Vector2 diference = objA.transform.position - objB.transform.position;
        float sign = (objA.transform.position.y < objB.transform.position.y) ? -1.0f : 1.0f;
        float angle = Vector2.Angle(Vector2.right, diference) * sign;
        angleBar.transform.Rotate(0, 0, angle);
        /**/
        /*calculate length of bar*/
        float height = 5;
        float width = Vector2.Distance(objB.transform.position, objA.transform.position);
        angleBar.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        /**/
        /*calculate midpoint position*/
        float newposX = objB.transform.position.x + (objA.transform.position.x - objB.transform.position.x) / 2;
        float newposY = objB.transform.position.y + (objA.transform.position.y - objB.transform.position.y) / 2;
        angleBar.transform.position = new Vector3(newposX, newposY, 0);
        /***/
        /*set parent to objB*/
        angleBar.transform.SetParent(objB.transform, true);
    }
}
