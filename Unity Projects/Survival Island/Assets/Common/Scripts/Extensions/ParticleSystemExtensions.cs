using UnityEngine;

namespace SurvivalIsland.Common.Extensions
{
    public static class ParticleSystemExtensions
    {
        public static ParticleSystem TryPlay(this ParticleSystem self)
        {
            if (!self.isPlaying)
                self.Play();

            return self;
        }
    }
}