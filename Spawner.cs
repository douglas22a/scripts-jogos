using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public PlayerMoviment player;
    private Pooling pooling;
    public float TempoDeEspera = 3;

    void Start()
    {
        pooling = FindObjectOfType<Pooling>(); // Encontra o script Pooling na cena
        StartCoroutine(SpawnObjects());
    }

    private void Update()
    {
        if(player.Pontos > 10 && player.Pontos < 25)
        {
            TempoDeEspera = 2;
        }

        if(player.Pontos > 25 && player.Pontos < 50)
        {
            TempoDeEspera = 1.5f;
        }

        if(player.Pontos > 50)
        {
            TempoDeEspera = 1.2f;
        }

    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            GameObject obj = pooling.PegaUmObjeto();
            obj.transform.position = new Vector3(6,Random.Range(2.5f,6.4f), -1); // Reposiciona o objeto
            obj.SetActive(true); // Ativa o objeto (embora isso já seja feito no script Pooling)
            yield return new WaitForSeconds(TempoDeEspera); // Espera um segundo antes de spawnar o próximo objeto
        }
    }
}