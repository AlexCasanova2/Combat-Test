using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public Vector3 inicio;
    public Vector3 destino;
    public float speed;
    private float t;

    void Start()
    {
        //Vector3 vector3 = GetComponent<Vector3>();
        //inicio = vector3;
        inicio = transform.position;
        //Debug.Log(inicio);
    }

    private void Update()
    {
        MoverObjeto();
        //Debug.Log(inicio);
    }

    public void MoverObjeto()
    {
        t += Time.deltaTime * speed;
        // Moves the object to target position
        transform.position = Vector3.Lerp(inicio, destino, t);
        if (t >= 1)
        {
            var b = destino;
            var a = inicio;
            inicio = b;
            destino = a;
            t = 0;
        }
    }
}