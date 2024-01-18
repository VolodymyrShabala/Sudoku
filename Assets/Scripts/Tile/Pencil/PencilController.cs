using System.Collections.Generic;

namespace Tile.Pencil{
    public class PencilController{
        private readonly PencilModel model;
        private readonly PencilView[] views;

        public PencilController(PencilModel pencilModel, PencilView[] pencilViews) {
            model = pencilModel;
            views = pencilViews;
        }
        
        public void Show(List<int> indices) {
            foreach (int index in indices) {
                Show(index);
            }
        }
        
        public void Show(int numberIndex) {
            int viewIndex = model.GetViewIndexFromNumberIndex(numberIndex);
            if (viewIndex != -1) {
                views[viewIndex].Show();
            }
        }
        
        public void Hide(int number) {
            int index = model.GetViewIndexFromNumberIndex(number);
            if (index != -1) {
                views[index].Hide();
            }
        }

        public bool CanShow(int numberIndex) {
            int viewIndex = model.GetViewIndexFromNumberIndex(numberIndex);
            bool result = viewIndex != -1;
            return result;
        }

        public bool IsShowing(int number) {
            bool result = false;
            int index = model.GetViewIndexFromNumberIndex(number);
            if (index != -1) {
                result = views[index].IsShowing();
            }

            return result;
        }

        public void Highlight(int number) {
            int index = model.GetViewIndexFromNumberIndex(number);
            if (index != -1) {
                views[index].Highlight(true);
            }
        }

        public void RemoveAllHighlight() {
            IEnumerable<int> viewIndices = model.GetAllIndices();
            foreach (int index in viewIndices) {
                views[index].Highlight(false);
            }
        }

        public List<int> GetAllShowingNumberIndices() {
            Dictionary<int, int> dictionary = model.GetDictionary();
            List<int> result = new();
            foreach (KeyValuePair<int, int> pair in dictionary) {
                bool isShowing = views[pair.Value].IsShowing();
                if (isShowing) {
                    result.Add(pair.Key);
                }
            }

            return result;
        }
        
        public void HideAll() {
            IEnumerable<int> viewIndices = model.GetAllIndices();
            foreach (int index in viewIndices) {
                views[index].Hide();
            }
        }
    }
}