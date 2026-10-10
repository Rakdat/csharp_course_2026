namespace App.Lab2;

public class Frequency
{
    private static void AddFreq(Dictionary<string, Dictionary<string, int>> freq, string key, string val)
    {
        if (!freq.TryGetValue(key, out var value))
        {
            value = new Dictionary<string, int>();
            freq.Add(key, value);
        }
        
        if (!value.TryGetValue(val, out var intvalue))
            freq[key].Add(val, 1);
        else
            freq[key][val] = ++intvalue;
    }

    private static void AddRes(Dictionary<string, Dictionary<string, int>> freq, Dictionary<string, string> result)
    {
        foreach (var i in freq.Keys)
        {
            var maxcnt = 0;
            string best = null;
            foreach (var j in freq[i].Keys)
            {
                var count = freq[i][j];
                if (count <= maxcnt && (count != maxcnt || (best != null && string.CompareOrdinal(j, best) >= 0))) continue;
                maxcnt = freq[i][j];
                best = j;
            }
            result.Add(i, best);
        }
    }
    
    public static Dictionary<string, string> FrequencyAnalysis(string inputString)
    {
        var frequencyStatsBi = new Dictionary<string, Dictionary<string, int>>();
        var frequencyStatsTre = new Dictionary<string, Dictionary<string, int>>();
        
        var lower = inputString.ToLower();
        var sentences = lower.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var sentence in sentences)
        {
            var words = sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            if (words.Length < 2) 
                continue;

            for (var i = 0; i < words.Length - 1; i++)
            {
                AddFreq(frequencyStatsBi, words[i], words[i + 1]);
            }

            if (words.Length < 3) 
                continue;
            
            for (var i = 0; i < words.Length - 2; i++)
            {
                AddFreq(frequencyStatsTre, words[i] + " " +  words[i + 1], words[i + 2]);
            }
        }

        var res = new  Dictionary<string, string>();
        AddRes(frequencyStatsBi, res);
        AddRes(frequencyStatsTre, res);

        return res;
    }
}