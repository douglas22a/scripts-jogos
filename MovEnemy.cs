using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MovEnemy : MonoBehaviour
{
    
    public Transform[] pontosDaRota;
    int pontoAtual;
    public float distanciaDoPonto = 2;

    // Componente NavMeshAgent para controlar a movimentação do inimigo
    NavMeshAgent agente;
  
    public Transform jogador;

    [Header("Visão")]
    public float distancia = 5;
    public float angulo = 60;
    public LayerMask layerDoPlayer;

    Animator anim;

    
    void Start()
    {
        jogador = null;
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        // Obtém a velocidade atual do inimigo
        float velocidade = agente.velocity.magnitude;
        // Define a velocidade no Animator para controle de animações
        anim.SetFloat("velocidade", velocidade);

        // Se o jogador for detectado, define o destino do inimigo como a posição do jogador
        if (jogador != null)
        {
            agente.SetDestination(jogador.position);
            return;
        }

        // Define o destino do inimigo como o próximo ponto na rota de patrulha
        agente.SetDestination(pontosDaRota[pontoAtual].position);

        // Verifica se o inimigo chegou ao ponto atual da patrulha
        if (Vector3.Distance(transform.position, pontosDaRota[pontoAtual].position) < agente.stoppingDistance)
        {
            // Avança para o próximo ponto na rota
            pontoAtual++;
            
            // Se o ponto atual for o último da rota, volta ao primeiro ponto
            if (pontoAtual >= pontosDaRota.Length)
            {
                pontoAtual = 0;
            }
        }

        // Verifica se o jogador está dentro do alcance de visão
        Collider[] estaPerto = Physics.OverlapSphere(transform.position, distancia, layerDoPlayer);

        if (estaPerto.Length > 0)
        {
            // Calcula a direção entre o inimigo e o jogador
            Vector3 direcaoEntreInimigoEPlayer = estaPerto[0].transform.position - transform.position;

            // Calcula o ângulo entre o inimigo e o jogador
            float anguloEntreInimigoEPlayer = Vector3.Angle(transform.forward, direcaoEntreInimigoEPlayer);

            // Se o jogador estiver dentro do campo de visão, define jogador como o Transform do jogador detectado
            if (anguloEntreInimigoEPlayer < angulo / 2)
            {
                jogador = estaPerto[0].transform;
            }
        }
    }

#if UNITY_EDITOR
    // Desenha um arco visual representando o campo de visão do inimigo no editor do Unity
    private void OnDrawGizmosSelected()
    {
        // Define a cor do arco como vermelho
        Handles.color = Color.red;
        // Desenha o arco a partir da posição do inimigo com o ângulo e distância especificados
        Handles.DrawSolidArc(
            transform.position,
            transform.up,
            Quaternion.AngleAxis(-angulo / 2, transform.up) * transform.forward,
            angulo,
            distancia
        );
    }
#endif
}
