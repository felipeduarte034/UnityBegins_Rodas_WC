using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VeiculoSimples : MonoBehaviour
{

    [SerializeField] Transform[] m_MeshRodas;
    [SerializeField] WheelCollider[] m_ColisorRodas;
    [SerializeField] float m_MaxAngle = 30f;
    [SerializeField] float m_MaxTorque = 1000f;
    [SerializeField] float m_BreakTorque = float.MaxValue;
    [SerializeField] float m_Peso = 2000f;

    float v;
    float h;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = m_Peso;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        ControlaCar(v, h, 0f);
    }

    private void ControlaCar(float v, float h, float ff)
    {
        m_ColisorRodas[0].steerAngle = h * m_MaxAngle; //Controla o angulo(eixo Y) das rodas dianteiras
        m_ColisorRodas[1].steerAngle = h * m_MaxAngle;

        m_ColisorRodas[2].motorTorque = v * m_MaxTorque; //Controla a força(eixo X) das rodas traseiras
        m_ColisorRodas[3].motorTorque = v * m_MaxTorque;

        if (Input.GetKey(KeyCode.Space)) // se apertar Spaço, é ativado o freio nas rodas traseiras
        {
            m_ColisorRodas[2].brakeTorque = m_BreakTorque;
            m_ColisorRodas[3].brakeTorque = m_BreakTorque;
        }
        else
        {
            m_ColisorRodas[2].brakeTorque = ff;
            m_ColisorRodas[3].brakeTorque = ff;
        }

		for (int i = 0; i < m_ColisorRodas.Length; i++) //Sincroniza o Mesh com o WheelCollider
		{
			Quaternion quat;
			Vector3 pos;
			m_ColisorRodas[i].GetWorldPose(out pos, out quat);
			m_MeshRodas[i].position = pos;
			m_MeshRodas[i].rotation = quat;
		}
    }
}
