using System;
using Enums;
using UnityEngine;

namespace Tile{
    public class TileBorders : MonoBehaviour{
        [SerializeField] private Animation animationComponent;
        [SerializeField] private AnimationClip highlightAnimation;
        [SerializeField] private AnimationClip wrongNumberAnimation;

        public void PlayAnimation(TileBackgroundColorEnum colorEnum) {
            switch (colorEnum) {
                case TileBackgroundColorEnum.Default:
                    break;
                case TileBackgroundColorEnum.Highlighted:
                    animationComponent.clip = highlightAnimation;
                    animationComponent.Play();
                    break;
                case TileBackgroundColorEnum.Selected:
                    animationComponent.clip = highlightAnimation;
                    animationComponent.Play();
                    break;
                case TileBackgroundColorEnum.WrongNumber:
                    animationComponent.clip = wrongNumberAnimation;
                    animationComponent.Play();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(colorEnum), colorEnum, null);
            }
        }
    }
}