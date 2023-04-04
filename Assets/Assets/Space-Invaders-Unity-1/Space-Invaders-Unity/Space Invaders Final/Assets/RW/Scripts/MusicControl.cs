/*
 * Copyright (c) 2021 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using UnityEngine;

namespace RayWenderlich.SpaceInvadersUnity
{
    public class MusicControl : MonoBehaviour
    {
        private readonly float defaultTempo = 1.33f; // 4 beats in 3 seconds

        [SerializeField] 
        private AudioSource source;

        [SerializeField] 
        internal int pitchChangeSteps = 5;

        [SerializeField] 
        private float maxPitch = 5.5f;

        private float pitchChange;

        internal float Tempo { get; private set; }

        internal void StopPlaying() => source.Stop();

        internal void IncreasePitch()
        {
            if (source.pitch == maxPitch) 
            {
                return;
            }

            source.pitch = Mathf.Clamp(source.pitch + pitchChange, 1, maxPitch);
            Tempo = Mathf.Pow(2, pitchChange) * Tempo;
        }

        private void Start()
        {
            source.pitch = 1f;
            Tempo = defaultTempo;
            pitchChange = maxPitch / pitchChangeSteps;
        }
    }
}