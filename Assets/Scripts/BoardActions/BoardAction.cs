using UnityEngine;

namespace BoardActions{
    public abstract class BoardAction : MonoBehaviour{
        public abstract void Init();
        public abstract void Clear();
    }
}