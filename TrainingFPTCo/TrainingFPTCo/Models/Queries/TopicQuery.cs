using Microsoft.Data.SqlClient;

namespace TrainingFPTCo.Models.Queries
{
    public class TopicQuery
    {
    
		public List<TopicDetail> GetAllTopics(string SearchString, string FilterStatus)
		{
            List<TopicDetail> topics = new List<TopicDetail>();
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sql = "SELECT [top].*, [co].[Name] AS [NameCourse] FROM [Topics] AS [top] INNER JOIN [Courses] AS [co] ON [top].[CourseId] = [co].[Id] WHERE [top].[DeletedAt] IS NULL";
                if (!string.IsNullOrEmpty(SearchString))
                {
					sql += " AND ([top].[Name] LIKE @search OR [co].[Name] LIKE @search)";

				}

				// Thêm điều kiện lọc theo trạng thái nếu FilterStatus được cung cấp
				if (!string.IsNullOrEmpty(FilterStatus))
                {
                    sql += " AND [top].[Status] = @status";
                }

                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@search", "%" + SearchString + "%" ?? DBNull.Value.ToString());
                if (FilterStatus != null)
                {
                    command.Parameters.AddWithValue("@status", FilterStatus ?? DBNull.Value.ToString());
                }
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
					{
						TopicDetail topic = new TopicDetail();
						topic.Id = Convert.ToInt32(reader["Id"]);
						topic.CourseId = Convert.ToInt32(reader["CourseId"]);
						topic.Name = reader["Name"].ToString();
						topic.Description = reader["Description"].ToString();
						topic.Status = reader["Status"].ToString();
						topic.NameDocuments = reader["Documents"].ToString();
						topic.NameAttachFile = reader["AttachFile"].ToString();
						topic.NamePosterTopic = reader["PosterTopic"].ToString();
						topic.TypeDocument = reader["TypeDocument"].ToString();
						topic.NameCourse = reader["NameCourse"].ToString();
						topic.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
						if (reader["UpdatedAt"] != DBNull.Value)
						{
							topic.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
						}                        //topic.DeletedAt = Convert.ToDateTime(reader["DeletedAt"]);
						topics.Add(topic);
					}
					connection.Close();
				}
			}
			return topics;
		}

		public int InsertItemTopic(
                int courseId,
                string nameTopic,
                string description,
                string status,
                string? documents,
                string? attachFile,
                string? posterTopic,
                string typeDocument
            )
        {
            int idTopic = 0;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sqlQuery = "INSERT INTO [Topics] ([CourseId],[Name],[Description],[Status],[Documents],[AttachFile],[PosterTopic],[TypeDocument],[CreatedAt]) VALUES(@CourseId,@Name,@Description,@Status,@Documents,@AttachFile,@PosterTopic,@TypeDocument,@CreatedAt) SELECT SCOPE_IDENTITY() ";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.Parameters.AddWithValue("@Name", nameTopic);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Documents", documents);
                cmd.Parameters.AddWithValue("@AttachFile", attachFile);
                cmd.Parameters.AddWithValue("@PosterTopic", posterTopic);
                cmd.Parameters.AddWithValue("@TypeDocument", typeDocument);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                idTopic = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return idTopic;
        }

        public bool UpdateTopicById(
                int courseId,
                string name,
                string description,
                string documents,
                string attachFile,
                string posterTopic,
                string typeDocument,
                string status,
                int id
            )
        {
            bool checkUpdate = false;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sql = "UPDATE [Topics] SET [CourseId] = @CourseId, [Name] = @Name, [Description] = @Description, [Documents] = @Documents, [AttachFile] = @AttachFile, [PosterTopic] = @PosterTopic, [TypeDocument] = @TypeDocument, [Status] = @Status, [UpdatedAt] = @UpdatedAt WHERE [Id] = @Id AND [DeletedAt] IS NULL";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Documents", documents);
                cmd.Parameters.AddWithValue("@AttachFile", attachFile);
                cmd.Parameters.AddWithValue("@PosterTopic", posterTopic);
                cmd.Parameters.AddWithValue("@TypeDocument", typeDocument);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                checkUpdate = true;
                connection.Close();
            }
            return checkUpdate;
        }

        public TopicDetail GetDetailTopicById(int id)
        {
            TopicDetail detail = new TopicDetail();
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sql = "SELECT * FROM [Topics] WHERE [Id]=@id AND [DeletedAt] IS NULL";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detail.Id = Convert.ToInt32(reader["ID"]);
                        detail.CourseId = Convert.ToInt32(reader["CourseId"]);
                        detail.Name = reader["Name"].ToString();
                        detail.Description = reader["Description"].ToString();
                        detail.Status = reader["Status"].ToString();
                        detail.NameDocuments = reader["Documents"].ToString();
                        detail.NameAttachFile = reader["AttachFile"].ToString();
                        detail.NamePosterTopic = reader["PosterTopic"].ToString();
                        detail.TypeDocument = reader["TypeDocument"].ToString();
                    }
                }
                connection.Close();
            }

            return detail;
        }

        public bool DeleteItemTopicById(int id = 0)
        {
            bool statusDelete = false;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sqlQuery = "UPDATE [Topics] SET [DeletedAt] =@deletedAt WHERE [Id] = @id";
                SqlCommand command = new SqlCommand(@sqlQuery, connection);
                connection.Open();
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("@deletedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();
                connection.Close();
                statusDelete = true;

            }


            return (statusDelete);
        }

    }
}