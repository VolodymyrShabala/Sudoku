using Enums;
using Tile.Pencil;
using UnityEngine;

namespace Tile{
    public class TileController{
        private readonly TileView view;
        private readonly TileModel model;
        public PencilController pencilController { get; private set; }

        public TileController(TileView tileView, TileModel tileModel) {
            view = tileView;
            model = tileModel;
        }

        public void Init(PencilController pencil) {
            pencilController = pencil;
            bool isNumberShown = model.IsCorrectNumberShown();
            string numberToShow = isNumberShown ? model.GetVisualNumber() : string.Empty;
            view.Init(numberToShow);
        }

        public void PlaceNumber(int numberIndex, string visualNumber) {
            bool isCorrectNumberShown = model.IsCorrectNumberShown();
            if (isCorrectNumberShown) {
                return;
            }
            
            if (model.IsCorrectNumber(numberIndex)) {
                model.SetCorrectNumberPlaced(true);
                model.SetShowingNumber(numberIndex);
                view.ShowCorrectNumber(visualNumber);
            }
            else {
                model.SetShowingNumber(numberIndex);
                view.ShowWrongNumber(visualNumber);
            }
        }

        public void RemoveNumber() {
            model.SetCorrectNumberPlaced(false);
            model.RemoveShowingNumber();
            view.ShowNumber(string.Empty);
        }

        public bool IsCorrectNumberShown() {
            bool result = model.IsCorrectNumberShown();
            return result;
        }

        public bool IsNumberShown() {
            bool result = model.HasNumberShown();
            return result;
        }

        public int GetCorrectNumberIndex() {
            int result = model.GetCorrectNumberIndex();
            return result;
        }

        public int GetShowingNumberIndex() {
            int result = model.GetShowingNumber();
            return result;
        }

        public void HighlightTile() {
            view.SetColor(TileBackgroundColorEnum.Highlighted);
        }

        public void HighlightSameNumberTile(bool isSimple) {
            view.SetColor(TileBackgroundColorEnum.Highlighted);
            if (isSimple == false) {
                view.PlayBorderAnimation(TileBackgroundColorEnum.Highlighted);
            }
        }

        public void SelectTile(bool isSimple) {
            view.SetColor(TileBackgroundColorEnum.Selected);
            if (isSimple == false) {
                view.PlayBorderAnimation(TileBackgroundColorEnum.Selected);
            }
        }

        public void HighlightWrongNumber() {
            view.SetColor(TileBackgroundColorEnum.Selected);
            view.PlayBorderAnimation(TileBackgroundColorEnum.WrongNumber);
        }

        public void HighlightBorderSimple() {
            view.PlayBorderAnimation(TileBackgroundColorEnum.Selected);
        }

        public void HighlightAdjacentWrongTile() {
            view.SetColor(TileBackgroundColorEnum.WrongNumber);
        }

        public void RemoveHighlight() {
            view.SetColor(TileBackgroundColorEnum.Default);
        }

        public RectTransform GetPencilHolder() {
            RectTransform result = view.GetPencilHolder();
            return result;
        }
        
        public RectTransform GetRectTransform() {
            RectTransform result = view.transform as RectTransform;
            return result;
        }
        
        public Vector2Int GetBoardPosition() {
            Vector2Int result = model.GetGridPosition();
            return result;
        }
    }
}