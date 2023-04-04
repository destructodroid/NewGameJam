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
    public class TorchkaManager : MonoBehaviour
    {
        [SerializeField] 
        Texture2D damageTexture;

        [SerializeField] 
        private Sprite torchkaSprite;

        [Space]
        [Header("Torchka Spawn Settings")]
        [SerializeField] 
        private Transform spawnMidPoint;

        [SerializeField] 
        private Torchka torchkaPrefab;
        
        [SerializeField] 
        private int totalCount = 4;
        
        [SerializeField] 
        private float spacing = 150f;

        private float torchkaPPU;
        private Vector2 torchkaPivot;
        private Color[] damagePixelArray;

        private void Awake()
        {
            if (damageTexture == null) 
            {
                return;
            }

            damagePixelArray = damageTexture.GetPixels();

            torchkaPPU = torchkaSprite.pixelsPerUnit;
            torchkaPivot = torchkaSprite.pivot;

            int halfCount = totalCount / 2;
            float startShift = totalCount % 2 == 0 ? halfCount - 0.5f : halfCount;

            Vector2 currentPos =
                (Vector2)spawnMidPoint.position + spacing * startShift * Vector2.left;
            for (int i = 0; i < totalCount; i++)
            {
                var torchka = Instantiate(torchkaPrefab, currentPos, Quaternion.identity);
                torchka.manager = this;
                currentPos += spacing * Vector2.right;
            }
        }

        public bool CheckForDamage(Texture2D tex, Vector2 contactPosition)
        {
            int coordX = Mathf.RoundToInt(contactPosition.x * torchkaPPU + torchkaPivot.x);
            int coordY = Mathf.RoundToInt(contactPosition.y * torchkaPPU + torchkaPivot.y);

            if (tex.GetPixel(coordX, coordY).a == 0) 
            {
                return false;
            }
            
            var dir = (Random.value > 0.5) ? -1 : 1;
            var startX = coordX + damageTexture.width / 2 * -dir;
            coordY += damageTexture.height / 2 * -dir;
            for (int y = 0; y < damageTexture.height; y++)
            {
                coordX = startX;
                for (int x = 0; x < damageTexture.width; x++)
                {
                    var thisPix = tex.GetPixel(coordX, coordY);
                    thisPix.a *= damagePixelArray[x + y * damageTexture.width].a;
                    tex.SetPixel(coordX, coordY, thisPix);
                    coordX += dir;
                }

                coordY += dir;
            }

            tex.Apply();
            return true;
        }
    }
}