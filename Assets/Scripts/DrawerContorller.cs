using UnityEngine;

public class DrawerContorller : MonoBehaviour, IEventListener_BtnTap
{
    private ParticleSystem particleSys;
    private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000]; // 粒子池
    private int particleCount = 0;

    void Start()
    {
        EventManager.Instance.RegisterEventListener<DrawerContorller>(EventType.BtnTap, this, true);
        // 1. 动态创建一个Particle System组件
        particleSys = gameObject.GetComponent<ParticleSystem>();
    }

    public void OnBtnTap() {
        AddParticle(Random.insideUnitCircle * 5, Color.white);
    }

    void AddParticle(Vector3 position, Color color)
    {
        if (particleCount >= particles.Length) return;

        particles[particleCount].position = position;
        particles[particleCount].startColor = color;
        particles[particleCount].startSize = 2f;
        particleCount++;

        // 将更新后的粒子数组设置回系统
        particleSys.SetParticles(particles, particleCount);
    }

    void Update()
    {
        // 可选：让粒子随时间缩小或移动
        for (int i = 0; i < particleCount; i++)
        {
            particles[i].position += new Vector3(0, 0.01f, 0); // 缓慢上浮
            particles[i].startSize *= 0.998f; // 缓慢缩小
        }
        particleSys.SetParticles(particles, particleCount);
    }
}
