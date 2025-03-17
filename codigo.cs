using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject tetrahedron; // prefab do tetraedro
    public GameObject[] vetGameObj = new GameObject[7]; // Ajuste o tamanho do array para 7 elementos
    GameObject pai;
    Vector3 m_Center;

    // Use this for initialization
    void Start()
    {
        // Instanciando os tetraedros
        for (int i = 0; i <= 8; i++)
        {
            if (i == 0)
            {
                vetGameObj[i] = Instantiate(tetrahedron, new Vector3(0, 0, 0), Quaternion.identity); // tetraedro base
            }
            else
            {
                if (i >= 3)
                {
                    vetGameObj[i] = Instantiate(tetrahedron, new Vector3(vetGameObj[i - 1].transform.position.x - 0.5f, 0, vetGameObj[i - 1].transform.position.z + 0.86603f), vetGameObj[i - 1].transform.rotation);
                }
                else
                {
                    vetGameObj[i] = Instantiate(tetrahedron, new Vector3(vetGameObj[i - 1].transform.position.x + 1, 0, 0), vetGameObj[i - 1].transform.rotation);
                }
                if(i == 5)
                {
                    vetGameObj[i] = Instantiate(tetrahedron, new Vector3(0.5f, 0, 0.86603f), vetGameObj[i - 1].transform.rotation);
                }
                else
                {
                    if(i >= 6)
                    {
                        vetGameObj[i] = Instantiate(tetrahedron, new Vector3(0.5f, 0.86603f, 0.28868f), vetGameObj[i - 1].transform.rotation);
                    }
                    if(i == 7)
                    {
                        vetGameObj[i] = Instantiate(tetrahedron, new Vector3(1.5f, 0.86603f, 0.28868f), vetGameObj[i - 1].transform.rotation);
                    }
                }
                
            }
        }

        // Criando o objeto pai para os tetraedros
        pai = new GameObject();
        pai.transform.position = new Vector3(0, 1, 0); // Pivô central da pirâmide

        // Tornando o tetraedro 3 filho do objeto pai
        vetGameObj[3].transform.parent = pai.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Nenhuma ação necessária para a rotação adicional neste momento
    }
}
