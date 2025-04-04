using UnityEngine;

namespace MVZ2.ChapterTransition
{
    public class ChapterTransition : MonoBehaviour
    {
        public void SetWheelRootRotation(float rotation)
        {
            wheelRoot.eulerAngles = new Vector3(0, 0, rotation);
        }
        public void SetTitleSprite(Sprite sprite)
        {
            titleRenderer.sprite = sprite;
        }
        [SerializeField]
        private Transform wheelRoot;
        [SerializeField]
        private SpriteRenderer titleRenderer;
    }
}
