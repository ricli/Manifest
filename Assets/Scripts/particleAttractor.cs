//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class particleAttractor : MonoBehaviour {
//	public ParticleEmitter p;
//	public Particle[] particles;
// 
//	void Start() {
//   		p = (ParticleEmitter)(GameObject.Find("StrangeParticles").GetComponent(typeof(ParticleEmitter)));
//    	particles = p.particles;
//	}
// 
//	void Update() {
//    	for (int i=0; i < particles.GetUpperBound(0)) {
//        	particles[i].position = Vector3.Lerp(particles[i].position,transform.position,Time.deltaTime / 2.0f);
//    	}
//    	p.particles = particles;
//	}
//}
