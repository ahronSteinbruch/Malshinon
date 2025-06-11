using Malshinon.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Services
{
    public class TagAnalyzer
    {
        private readonly Dictionary<string, Tag> tagByName;
        private readonly List<Tag> allTags;

        public TagAnalyzer(List<Tag> tags)
        {
            allTags = tags;
            tagByName = tags.ToDictionary(t => t.Name.ToLower());
        }

        public Tag? AnalyzeTextAndFindPrimaryTag(string reportText)
        {
            var foundDirectTags = new HashSet<Tag>();

            // 1. מצא תגיות שמופיעות ישירות בטקסט
            foreach (var tag in allTags)
            {
                if (reportText.Contains(tag.Name, StringComparison.OrdinalIgnoreCase))
                    foundDirectTags.Add(tag);
            }

            if (foundDirectTags.Count == 0)
                return null;

            // 2. הרחבה לתגיות קרובות (מרחק 1 או 2) בלי מעגלים
            var tagScores = new Dictionary<Tag, int>();
            var visited = new HashSet<Tag>();

            foreach (var tag in foundDirectTags)
            {
                TraverseTags(tag, 0, tagScores, visited, maxDepth: 2);
            }

            // 3. דירוג התגיות לפי קרבה
            var bestTag = tagScores.OrderBy(t => t.Value).FirstOrDefault().Key;

            return bestTag;
        }

        private void TraverseTags(Tag tag, int depth, Dictionary<Tag, int> scores, HashSet<Tag> visited, int maxDepth)
        {
            if (depth > maxDepth || visited.Contains(tag))
                return;

            visited.Add(tag);

            // ככל שקרוב יותר, עדיפות גבוהה יותר
            if (!scores.ContainsKey(tag) || scores[tag] > depth)
                scores[tag] = depth;

            foreach (var neighbor in tag.ConnectedTags)
            {
                TraverseTags(neighbor, depth + 1, scores, visited, maxDepth);
            }
        }
    }

}
