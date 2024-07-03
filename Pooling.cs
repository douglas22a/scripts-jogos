using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject prefabUsar; // O prefab que ser� instanciado
    public List<GameObject> objetosEmJogo; // Lista para armazenar os objetos instanciados
    public int comecaCriado = 10; // Quantidade inicial de objetos a serem criados

   
    void Start()
    {
        // Instancia os objetos iniciais no in�cio do jogo
        for (int i = 0; i < comecaCriado; i++)
        {
            GameObject novoObjeto = Instantiate(prefabUsar); // Instancia um novo objeto a partir do prefab
            objetosEmJogo.Add(novoObjeto); // Adiciona o objeto � lista de objetos em jogo
            novoObjeto.SetActive(false); // Desativa o objeto rec�m-criado
        }
    }

    // M�todo para obter um objeto da pool
    public GameObject PegaUmObjeto()
    {
        // Verifica todos os objetos na lista de objetos em jogo
        for (int i = 0; i < objetosEmJogo.Count; i++)
        {
            // Se encontrar um objeto que n�o est� ativo na hierarquia
            if (!objetosEmJogo[i].activeInHierarchy)
            {
                return objetosEmJogo[i]; // Retorna o objeto desativado encontrado
            }
        }

        // Se n�o encontrar nenhum objeto desativado, cria um novo
        GameObject novoObjeto = Instantiate(prefabUsar); // Instancia um novo objeto a partir do prefab
        objetosEmJogo.Add(novoObjeto); // Adiciona o objeto � lista de objetos em jogo
        novoObjeto.SetActive(false); // Desativa o objeto rec�m-criado

        return novoObjeto; // Retorna o objeto rec�m-criado ou reutilizado
    }
}