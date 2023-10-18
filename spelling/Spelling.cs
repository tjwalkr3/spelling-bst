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

            try {
                dict.Find(inputWords[i]);
            } catch (KeyNotFoundException) {
                neverSeen = true;
            }

            // Decide whether to increment the "words already seen" or the "new words seen"
            if (neverSeen) {
                neverSeenCount++;
                // Every third word you've never seen
                if (neverSeenCount % 3 == 0) {
                    string? successor = GetSuccessor(dict, inputWords[i]);
                    if (successor == null) {
                        output.Add($"{inputWords[i]}");
                        dict.Add(inputWords[i]);
                    } else {
                        output.Add($"{successor}");
                    }
                } else {
                    dict.Add(inputWords[i]);
                    output.Add($"{inputWords[i]}");
                }
            } else {
                seenBeforeCount++;
                if (seenBeforeCount % 3 == 0) {
                    dict.Remove(inputWords[i]);
                    string? successor = GetSuccessor(dict, inputWords[i]);
                    if (successor != null) {
                        output.Add($"{successor}");
                    }

                // Usual case
                } else {
                    output.Add($"{inputWords[i]}");
                }
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
