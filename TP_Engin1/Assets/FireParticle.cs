using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour
{
    [SerializeField] private List<Gradient> m_listOfColor = new List<Gradient>();
    [SerializeField] private int m_colorIndex;
    [SerializeField] private ParticleSystem m_particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (m_particleSystem != null)
        {
            if (m_colorIndex < m_listOfColor.Count)
            {
                var colorParticle = m_particleSystem.colorOverLifetime;
                colorParticle.color = m_listOfColor[m_colorIndex];
            }
        }
    }
}
