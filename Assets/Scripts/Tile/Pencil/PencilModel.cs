using System.Collections.Generic;
using System.Linq;

namespace Tile.Pencil{
    public class PencilModel{
        private readonly Dictionary<int, int> indexIndexToView;
        
        public PencilModel(Dictionary<int, int> indexToViewDictionary) {
            indexIndexToView = indexToViewDictionary;
        }

        public int GetViewIndexFromNumberIndex(int numberIndex) {
            int result = -1;
            if (indexIndexToView.TryGetValue(numberIndex, out int value)) {
                result = value;
            }

            return result;
        }

        // TODO V: I think this needs to be removed
        public Dictionary<int, int> GetDictionary() {
            return indexIndexToView;
        }

        public IEnumerable<int> GetAllIndices() {
            int[] result = indexIndexToView.Values.ToArray();
            return result;
        }
    }
}