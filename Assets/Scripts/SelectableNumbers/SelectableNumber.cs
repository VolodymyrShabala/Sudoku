using System.Globalization;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SelectableNumbers{
    public class SelectableNumber : MonoBehaviour{
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI amountLeftText;
        [SerializeField] private Color pencilBackgroundColor;
        [SerializeField] private Color pencilNumberTextColor;
        [SerializeField] private Color pencilAmountLeftColor;
        private Image backgroundImage;
        private Color originalNumberTextColor;
        private Color originalImageColor;
        private Color originalAmountLeftColor;

        private int numberIndex;
        private int amountLeft;
        // V: Because I use layout group and I want to keep the position of objects the same, I can't use gameObject.activeInHierarchy
        private bool isActive = true;

        public void Init(int index, string visualNumber, int numbersAmountLeft) {
            numberIndex = index;
            numberText.text = visualNumber;
            amountLeft = numbersAmountLeft;
            UpdateAmountLeftText();

            backgroundImage = button.GetComponent<Image>();
            originalNumberTextColor = numberText.color;
            originalImageColor = backgroundImage.color;
            originalAmountLeftColor = amountLeftText.color;

            button.onClick.AddListener(ButtonPressed);

            if (amountLeft <= 0) {
                ChangeActiveState(false);
            }
        }

        public void IncrementAmountLeft() {
            amountLeft++;
            if (isActive == false && amountLeft > 0) {
                ChangeActiveState(true);
            }

            if (isActive) {
                UpdateAmountLeftText();
            }
        }

        public void DecrementAmountLeft() {
            amountLeft--;
            if (isActive == false) {
                return;
            }

            if (amountLeft <= 0) {
                ChangeActiveState(false);
            }

            UpdateAmountLeftText();
        }

        private void UpdateAmountLeftText() {
            amountLeftText.text = amountLeft.ToString(CultureInfo.InvariantCulture);
        }

        private void ButtonPressed() {
            EventSystem.Trigger(new NumberSelectedEvent(numberIndex));
        }

        public void ChangeVisualState(bool isPencil) {
            if (isActive == false) {
                return;
            }

            amountLeftText.color = isPencil ? pencilAmountLeftColor : originalAmountLeftColor;
            numberText.color = isPencil ? pencilNumberTextColor : originalNumberTextColor;
            backgroundImage.color = isPencil ? pencilBackgroundColor : originalImageColor;
        }

        private void ChangeActiveState(bool state) {
            isActive = state;

            int alpha = state ? 255 : 0;
            Color numberColor = numberText.color;
            numberColor.a = alpha;
            numberText.color = numberColor;

            Color amountLeftColor = amountLeftText.color;
            amountLeftColor.a = alpha;
            amountLeftText.color = amountLeftColor;

            Color imageColor = backgroundImage.color;
            imageColor.a = alpha;
            backgroundImage.color = imageColor;

            button.interactable = state;
        }

        public void Clear() {
            button.onClick.RemoveListener(ButtonPressed);
            backgroundImage = null;
        }
    }
}