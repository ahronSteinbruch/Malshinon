using Malshinon.models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Malshinon.DAL
{
    internal class TagsRepository
    {
        private readonly ConnectionWrapper _connection = ConnectionWrapper.getInstance();

        public Tag? GetTagByName(string name)
        {
            const string query = "SELECT * FROM Tags WHERE Name = @name";
            var parameters = new Dictionary<string, object> { { "@name", name } };
            using var reader = _connection.ExecuteSelect(query, parameters);

            if (reader != null && reader.Read())
            {
                return new Tag
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()
                };
            }
            return null;
        }

        public List<string> GetConnectedTagNamesRecursive(string rootTagName, int maxDepth)
        {
            var rootTag = GetTagByName(rootTagName);
            if (rootTag == null) return new List<string>();

            var visited = new HashSet<int> { rootTag.Id };
            var queue = new Queue<(int tagId, int depth)>();
            queue.Enqueue((rootTag.Id, 0));
            var result = new List<string> { rootTag.Name };

            while (queue.Count > 0)
            {
                var (currentId, depth) = queue.Dequeue();
                if (depth >= maxDepth) continue;

                var neighbors = GetRelatedTagIds(currentId);
                foreach (var neighborId in neighbors)
                {
                    if (!visited.Contains(neighborId))
                    {
                        visited.Add(neighborId);
                        var neighborTag = GetTagById(neighborId);
                        if (neighborTag != null)
                        {
                            result.Add(neighborTag.Name);
                            queue.Enqueue((neighborId, depth + 1));
                        }
                    }
                }
            }

            return result;
        }

        private Tag? GetTagById(int id)
        {
            const string query = "SELECT * FROM Tags WHERE Id = @id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            using var reader = _connection.ExecuteSelect(query, parameters);

            if (reader != null && reader.Read())
            {
                return new Tag
                {
                    Id = id,
                    Name = reader["Name"].ToString()
                };
            }
            return null;
        }

        private List<int> GetRelatedTagIds(int tagId)
        {
            const string query = @"
                SELECT TagId FROM ReportTags WHERE ReportId IN (
                    SELECT ReportId FROM ReportTags WHERE TagId = @tagId
                ) AND TagId != @tagId";

            var parameters = new Dictionary<string, object> { { "@tagId", tagId } };
            var result = new List<int>();

            using var reader = _connection.ExecuteSelect(query, parameters);
            while (reader != null && reader.Read())
            {
                result.Add(Convert.ToInt32(reader["TagId"]));
            }
            return result;
        }
    }
}
