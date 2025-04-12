using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TipoRotacao
{
    Nenhuma,
    TopoY,
    MeioY,
    BaseY
}

public class manager : MonoBehaviour
{
    public GameObject tetrahedron; // prefab da câmera
    public GameObject[] vetGameObj = new GameObject[24];
    GameObject paiMeioY;
    GameObject paiBaseY;
    
    GameObject planoTopoY;
    GameObject planoMeioY;
    GameObject planoBaseY;
    private float rotacaoAcumulada = 0f;
    private float velocidadeRotacao = 90f;
    private bool girando = false;

    private TipoRotacao tipoRotacaoAtual = TipoRotacao.Nenhuma;
    private System.Action<float> acaoRotacaoAtual;

    void Start()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i == 0)
            {
                vetGameObj[i] = Instantiate(tetrahedron, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
                vetGameObj[i] = Instantiate(tetrahedron, new Vector3(vetGameObj[i - 1].transform.position.x + 1, 0, 0), vetGameObj[i - 1].transform.rotation);

            vetGameObj[i].name = "Tetraedro " + i;
        }

        // Posicionamentos específicos
        vetGameObj[3].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[3].transform.Rotate(143f, 180f, 0);

        vetGameObj[4].transform.position = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[5].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[5].transform.Rotate(143f, 180f, 0);

        vetGameObj[6].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[7].transform.position = new Vector3(2.0f, Mathf.Sqrt(0.75f) * 2, Mathf.Sqrt(0.75f) * 2 / 3);
        vetGameObj[7].transform.Rotate(143f, 180f, 0);

        vetGameObj[8].transform.position = new Vector3(1.0f, Mathf.Sqrt(0.75f) * 2, Mathf.Sqrt(0.75f) * 2 / 3);
        vetGameObj[9].transform.position = new Vector3(1.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[10].transform.position = new Vector3(1.0f, 0, Mathf.Sqrt(0.75f) * 2);
        vetGameObj[11].transform.position = new Vector3(1.0f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) * 4 / 3);

        vetGameObj[12].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f) * 2, Mathf.Sqrt(0.75f) * 5 / 3);
        vetGameObj[12].transform.Rotate(-17.5f, -125f, 213f);

        vetGameObj[13].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) * 7 / 3);
        vetGameObj[13].transform.Rotate(-17.5f, -125f, 213f);

        vetGameObj[14].transform.position = new Vector3(1.0f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) * 4 / 3);
        vetGameObj[14].transform.Rotate(-17.5f, -125f, 213f);

        vetGameObj[15].transform.position = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[16].transform.position = new Vector3(1.148f, Mathf.Sqrt(0.75f) / 2.5f, 1.233f);
        vetGameObj[16].transform.Rotate(342f, 125f, 147f);

        vetGameObj[17].transform.position = new Vector3(1.664f, Mathf.Sqrt(0.75f) / 2.5f, 0.367f);
        vetGameObj[17].transform.Rotate(342f, 125f, 147f);

        vetGameObj[18].transform.position = new Vector3(1.148f, 1.201f, 0.673f);
        vetGameObj[18].transform.Rotate(342f, 125f, 147f);

        vetGameObj[19].transform.position = new Vector3(2.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[19].transform.Rotate(0, 180f, 0);

        vetGameObj[20].transform.position = new Vector3(1.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[20].transform.Rotate(0, 180f, 0);

        vetGameObj[21].transform.position = new Vector3(2.0f, 0, Mathf.Sqrt(0.75f) * 2);
        vetGameObj[21].transform.Rotate(0, 180f, 0);

        paiMeioY = new GameObject("PaiMeioY");
        paiBaseY = new GameObject("PaiBaseY");



        this.planoMeioY = CriaPlano("PlanoMeioY", new Vector3(1.5f, 1.5f, 0.8f));
        this.planoBaseY = CriaPlano("PlanoBaseY", new Vector3(1.5f, 0.5f, 0.8f));
        

        Destroy(vetGameObj[19]);
        Destroy(vetGameObj[20]);
        Destroy(vetGameObj[21]);
        Destroy(vetGameObj[22]);
        Destroy(vetGameObj[23]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && !girando)
        {
            girando = true;
            rotacaoAcumulada = 0f;
            tipoRotacaoAtual = TipoRotacao.TopoY;
            acaoRotacaoAtual = GirarTopoY;
        }

        if (Input.GetKeyDown(KeyCode.F2) && !girando)
        {
            girando = true;
            rotacaoAcumulada = 0f;
            tipoRotacaoAtual = TipoRotacao.MeioY;
            acaoRotacaoAtual = GirarMeioY;
        }

        if (Input.GetKeyDown(KeyCode.F3) && !girando)
        {
            girando = true;
            rotacaoAcumulada = 0f;
            tipoRotacaoAtual = TipoRotacao.BaseY;
            acaoRotacaoAtual = GirarBaseY;
        }

        if (girando && acaoRotacaoAtual != null)
        {
            float rotacaoFrame = velocidadeRotacao * Time.deltaTime;

            if (rotacaoAcumulada + rotacaoFrame >= 120f)
            {
                rotacaoFrame = 120f - rotacaoAcumulada;

                // Aplica última parte da rotação
                acaoRotacaoAtual.Invoke(rotacaoFrame);
                rotacaoAcumulada += rotacaoFrame;

                // Agora desvincula
                switch (tipoRotacaoAtual)
                {
                    case TipoRotacao.MeioY:
                        DesvincularFilhos(paiMeioY);
                        break;
                    case TipoRotacao.BaseY:
                        DesvincularFilhos(paiBaseY);
                        break;
                }

                girando = false;
                tipoRotacaoAtual = TipoRotacao.Nenhuma;
            }
            else
            {
                acaoRotacaoAtual.Invoke(rotacaoFrame);
                rotacaoAcumulada += rotacaoFrame;
            }
        }
    }

    void GirarTopoY(float rotacao)
    {
        Vector3 pontoRotacao = new Vector3(1.5f, 0.4f, 0.86603f);
        vetGameObj[8].transform.RotateAround(pontoRotacao, Vector3.up, rotacao);
    }

    void GirarMeioY(float rotacao)
    {
        paiMeioY.transform.position = new Vector3(1.5f, 0.4f, 0.86603f);
        Vector3 pontoRotacao = paiMeioY.transform.position;

        // Vincula
        var filhos = GetTetraedrosDoMeio();
        foreach (GameObject obj in filhos)
            obj.transform.parent = paiMeioY.transform;

        paiMeioY.transform.RotateAround(pontoRotacao, Vector3.up, rotacao);
    }

    void GirarBaseY(float rotacao)
    {
        paiBaseY.transform.position = new Vector3(1.5f, 0.4f, 0.86603f);
        Vector3 pontoRotacao = paiBaseY.transform.position;

        int[] indices = { 0, 1, 2, 3, 5, 9, 10, 13, 14, 15, 16, 17 };
        foreach (int i in indices)
            vetGameObj[i].transform.parent = paiBaseY.transform;

        paiBaseY.transform.RotateAround(pontoRotacao, Vector3.up, rotacao);
    }

    void DesvincularFilhos(GameObject pai)
    {
        while (pai.transform.childCount > 0)
        {
            Transform filho = pai.transform.GetChild(0);
            filho.SetParent(null, true);
        }
    }
    
    List<GameObject> GetTetraedrosDoMeio()
    {
        List<GameObject> resultado = new List<GameObject>();
        Collider[] coliders = Physics.OverlapBox(planoMeioY.transform.position, planoMeioY.GetComponent<BoxCollider>().size / 2f);

        foreach (Collider col in coliders)
        {
            Debug.Log(col);
            if (col.gameObject.name.StartsWith("Tetraedro"))
            {
                Debug.Log(col.gameObject.name);
                resultado.Add(col.gameObject);
            }
        }

        return resultado;
    }

    GameObject CriaPlano(string nome, Vector3 pos){
        GameObject plano = new GameObject(nome);
        BoxCollider box = plano.AddComponent<BoxCollider>();
        box.isTrigger = true;
        box.size = new Vector3(5f, 0.1f, 5f); 
        plano.transform.position = pos;
        return plano;
    }
}
