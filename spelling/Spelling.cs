namespace spelling;
using bst_code;

public static class Spelling {
    public static List<string> Fix(List<string> inputWords) {
        Tree<string> dict = new();
        List<string> output = new();
        int neverSeenCount = 0;
        int seenBeforeCount = 0;

        for (int i = 0; i < inputWords.Count; i++) {
            bool neverSeen = dict.Add(inputWords[i]);

            // Decide whether to increment the "words already seen" or the "new words seen"
            if (neverSeen) {
                neverSeenCount++;
            } else {
                seenBeforeCount++;
            }

            // Every third word you've never seen
            if (neverSeenCount > 2) {
                string? successor = GetSuccessor(dict, inputWords[i]);
                if (successor == null) {
                    output.Add(inputWords[i]);
                } else {
                    output.Add(successor);
                }
                neverSeenCount = 0;

            // Every third word you've already seen
            } else if (seenBeforeCount > 2) {
                string? successor = GetSuccessor(dict, inputWords[i]);
                dict.Remove(inputWords[i]);
                if (successor != null) {
                    output.Add(successor);
                }
                seenBeforeCount = 0;

            // Usual case
            } else {
                output.Add(inputWords[i]);
            }
        }
        
        return output;
    }

    // Get the next succesor using the InOrder iterator
    private static string? GetSuccessor(Tree<string> dict, string stringToFind) {
        string? output = null;

        foreach (string current in dict.InOrder()) {
            if (current.CompareTo(stringToFind) > 0) {
                output = current;
                break;
            }
        }

        return output;
    }
}
