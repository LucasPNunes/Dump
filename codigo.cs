using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject tetrahedron; // prefab do tetraedro
    public GameObject[] vetGameObj = new GameObject[19]; // Ajustado o tamanho do array para 19 elementos
    GameObject pai;
    Vector3 m_Center;

    // Use this for initialization
    void Start()
    {
        vetGameObj[0] = Instantiate(tetrahedron, new Vector3(0, 0, 0), Quaternion.identity);
        vetGameObj[1] = Instantiate(tetrahedron, new Vector3(1, 0, 0), Quaternion.identity);
        vetGameObj[2] = Instantiate(tetrahedron, new Vector3(2, 0, 0), Quaternion.identity);
        vetGameObj[3] = Instantiate(tetrahedron, new Vector3(0.5f, 0, 0.86603f), Quaternion.identity);
        vetGameObj[4] = Instantiate(tetrahedron, new Vector3(1.5f, 0, 0.86603f), Quaternion.identity);
        vetGameObj[5] = Instantiate(tetrahedron, new Vector3(1, 0, 1.73206f), Quaternion.identity);
        vetGameObj[6] = Instantiate(tetrahedron, new Vector3(0.5f, 0.86603f, 0.28868f), Quaternion.identity);
        vetGameObj[7] = Instantiate(tetrahedron, new Vector3(1.5f, 0.86603f, 0.28868f), Quaternion.identity);
        vetGameObj[8] = Instantiate(tetrahedron, new Vector3(1, 0.86603f, 1.15471f), Quaternion.identity);
        vetGameObj[9] = Instantiate(tetrahedron, new Vector3(1, 1.73206f, 0.578f), Quaternion.identity);
        vetGameObj[10] = Instantiate(tetrahedron, new Vector3(1.5f, 0.86603f, 0.287f), Quaternion.Euler(new Vector3(108, 0, 0)));
        vetGameObj[11] = Instantiate(tetrahedron, new Vector3(0.5f, 0.86603f, 0.287f), Quaternion.Euler(new Vector3(108, 0, 0)));
        vetGameObj[12] = Instantiate(tetrahedron, new Vector3(1, 0.86603f, 1.15471f), Quaternion.Euler(new Vector3(108, 120, 0)));
        vetGameObj[13] = Instantiate(tetrahedron, new Vector3(1.5f, 0.86603f, 2), Quaternion.Euler(new Vector3(108, 120, 0)));
        vetGameObj[14] = Instantiate(tetrahedron, new Vector3(2.5f, 0.86603f, 0.287f), Quaternion.Euler(new Vector3(108, 240, 0)));
        vetGameObj[15] = Instantiate(tetrahedron, new Vector3(2, 0.86603f, 1.15471f), Quaternion.Euler(new Vector3(108, 240, 0)));
        vetGameObj[16] = Instantiate(tetrahedron, new Vector3(1, 1.73206f, 0.578f), Quaternion.Euler(new Vector3(108, 0, 0)));
        vetGameObj[17] = Instantiate(tetrahedron, new Vector3(1.5f, 1.73206f, 1.445f), Quaternion.Euler(new Vector3(108, 120, 0)));
        vetGameObj[18] = Instantiate(tetrahedron, new Vector3(2, 1.73206f, 0.578f), Quaternion.Euler(new Vector3(108, 240, 0)));
        Debug.Log(GetCentro(vetGameObj[9]));
        //Criando o objeto pai para os tetraedros

        //Tornando o tetraedro 3 filho do objeto pai
    }

    // Update is called once per frame
    void Update()
    {
        // Nenhuma ação necessária para a rotação adicional neste momento
        vetGameObj[9].transform.RotateAround(new Vector3(1.5f, 2.02f, 0.78f), Vector3.up, 8 * Time.deltaTime);
    }

    Vector3 GetCentro(GameObject obj)
    {
        Vector3 vertice1 = obj.transform.position;
 
        Vector3 vertice2 = vertice1 + new Vector3(1f, 0f, 0f);
        Vector3 vertice3 = vertice1 + new Vector3(0.5f, Mathf.Sqrt(3f) / 2f, 0f);
        Vector3 vertice4 = vertice1 + new Vector3(0.5f, Mathf.Sqrt(3f) / 6f, Mathf.Sqrt(6f) / 3f);

        Vector3 centroide = (vertice1 + vertice2 + vertice3 + vertice4) / 4f;

        return centroide;
    }

}
