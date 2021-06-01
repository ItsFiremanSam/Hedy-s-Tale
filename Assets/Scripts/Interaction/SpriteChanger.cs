using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public class SpriteChanger : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite puzzleSolvedSprite;

        public void ChangeSprite()
        {

            spriteRenderer.sprite = puzzleSolvedSprite;

        }
    }
}
