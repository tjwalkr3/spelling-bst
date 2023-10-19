namespace spelling;
using bst_code;

public static class Spelling {
    public static List<string> Fix(List<string> inputWords) {
        Tree<string> dict = new();
        List<string> output = new();
        int neverSeenCount = 0;
        int seenBeforeCount = 0;

        for (int i = 0; i < inputWords.Count; i++) {
            bool neverSeen = false;

            // Have I seen this word before? 
            try {
                string found = dict.Find(inputWords[i]);
                if (found != inputWords[i]) {
                    throw new KeyNotFoundException("Item could not be found in this SortedSet");
                }
            } catch (KeyNotFoundException) {
                neverSeen = true;
            }

            // I've never seen this word before. 
            if (neverSeen) {
                neverSeenCount++;

                // Three times
                if (neverSeenCount % 3 == 0) {
                    try {
                        string successor = dict.Find(inputWords[i]);
                        output.Add(successor);
                    } catch (KeyNotFoundException) {
                        output.Add(inputWords[i]);
                        dict.Add(inputWords[i]);
                    }

                // Usual case
                } else {
                    dict.Add(inputWords[i]);
                    output.Add(inputWords[i]);
                }

            // I've seen this word before. 
            } else {
                seenBeforeCount++;

                // Three times
                if (seenBeforeCount % 3 == 0) {
                    dict.Remove(inputWords[i]);
                    try {
                        string successor = dict.Find(inputWords[i]);
                        output.Add(successor);
                    } catch (KeyNotFoundException) {

                    }

                // Usual case
                } else {
                    output.Add(inputWords[i]);
                }
            }
        }

        return output;
    }
}
