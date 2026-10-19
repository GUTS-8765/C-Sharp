using System;
using System.Security.Cryptography;

namespace ConsoleApp4
{
    internal class Passphrase
    {
        List<string> randomWords = new List<string>
        {
            "apple", "running", "shadow", "bridge", "guitar", "window", "cloud", "journey", "bottle", "keyboard",
            "yellow", "silence", "hammer", "flight", "mountain", "pocket", "stream", "puzzle", "candle", "blanket",
            "forest", "picture", "market", "coffee", "island", "jacket", "mirror", "pillow", "ticket", "garden",
            "breeze", "thunder", "button", "castle", "desert", "engine", "feather", "garage", "honest", "insect",
            "jungle", "kitten", "lantern", "magnet", "needle", "oxygen", "purple", "quarry", "rabbit", "sailor",
            "tunnel", "unique", "velvet", "whisper", "anchor", "bucket", "circus", "dragon", "elastic", "fountain",
            "glance", "harvest", "impact", "jacket", "ladder", "marble", "nephew", "ocean", "palace", "quartz",
            "rescue", "saddle", "timber", "umbrella", "valley", "walnut", "zipper", "active", "bright", "clever",
            "danger", "energy", "famous", "gentle", "hungry", "ignore", "jealous", "knight", "lizard", "monkey",
            "notice", "orange", "pencil", "quench", "rhythm", "silver", "theory", "update", "victory", "winter"
        };

        List<string> selectedWords = new List<string>();

        public string GetRandomPhrase(int len)
        {
            string total = "";

            for (int i = 0; i < len; i++)
            {
                int randomIndex = RandomNumberGenerator.GetInt32(0, randomWords.Count);
                selectedWords.Add(randomWords[randomIndex]);

                total = string.Join("-", selectedWords);
            }

            return total;
        }

        public double GetPassphraseStrength(int len)
        {
            return len * Math.Log2(randomWords.Count);
        }
    }
}
