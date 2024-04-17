using Microsoft.Data.SqlClient;

namespace TrainingFPTCo.Models.Queries
{
	public class AccountQuery
	{
		public bool UpdateAccountById(
			int roleId,
			string userName,
			string password,
			string extraCode,
			string email,
			string phone,
			string? address,
			string fullName,
			string? firstName,
			string? lastName,
			DateTime birthday,
			string? gender,
			string? education,
			string? programmingLanguage,
			int? toeicScore,
			string? skill,
			string? ipClient,
			DateTime? lastLogin,
			DateTime? lastLogout,
			int id
)
		{
			bool checkUpdate = false;
			using (SqlConnection connection = Database.GetSqlConnection())
			{
				string sql = "UPDATE [Users] SET [RoleId] = @RoleId, [Username] = @Username, [Password] = @Password, [ExtraCode] = @ExtraCode, [Email] = @Email, [Phone] = @Phone, [Address] = @Address, [FullName] = @FullName, [FirstName] = @FirstName, [LastName] = @LastName, [Birthday] = @Birthday, [Gender] = @Gender, [Education] = @Education, [ProgrammingLanguage] = @ProgrammingLanguage, [ToeicScore] = @ToeicScore, [Skill] = @Skill, [IpClient] = @IpClient, [LastLogin] = @LastLogin, [LastLogout] = @LastLogout, [UpdatedAt] = @UpdatedAt WHERE [Id] = @Id AND [DeletedAt] IS NULL";
				connection.Open();
				SqlCommand cmd = new SqlCommand(sql, connection);
				cmd.Parameters.AddWithValue("@RoleId", roleId);
				cmd.Parameters.AddWithValue("@Username", userName);
				cmd.Parameters.AddWithValue("@Password", password);
				cmd.Parameters.AddWithValue("@ExtraCode", extraCode);
				cmd.Parameters.AddWithValue("@Email", email);
				cmd.Parameters.AddWithValue("@Phone", phone);
				cmd.Parameters.AddWithValue("@Address", address ?? DBNull.Value.ToString());
				cmd.Parameters.AddWithValue("@FullName", fullName);
				cmd.Parameters.AddWithValue("@FirstName", firstName ?? DBNull.Value.ToString());
				cmd.Parameters.AddWithValue("@LastName", lastName ?? DBNull.Value.ToString());
				cmd.Parameters.AddWithValue("@Birthday", birthday);
				cmd.Parameters.AddWithValue("@Gender", gender ?? DBNull.Value.ToString());
				cmd.Parameters.AddWithValue("@Education", education ?? DBNull.Value.ToString());
				cmd.Parameters.AddWithValue("@ProgrammingLanguage", programmingLanguage ?? DBNull.Value.ToString());
				cmd.Parameters.AddWithValue("@ToeicScore", toeicScore.HasValue ? (object)toeicScore : DBNull.Value);
				cmd.Parameters.AddWithValue("@Skill", skill ?? DBNull.Value.ToString());
				cmd.Parameters.AddWithValue("@IpClient", ipClient ?? DBNull.Value.ToString());
				cmd.Parameters.AddWithValue("@LastLogin", lastLogin.HasValue ? (object)lastLogin : DBNull.Value);
				cmd.Parameters.AddWithValue("@LastLogout", lastLogout.HasValue ? (object)lastLogout : DBNull.Value);
				cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
				cmd.Parameters.AddWithValue("@Id", id);
				int rowsAffected = cmd.ExecuteNonQuery();
				checkUpdate = rowsAffected > 0;
				connection.Close();
			}
			return checkUpdate;
		}

		public bool DeleteAccountById(int id)
		{
			bool checkDelete = false;
			using (SqlConnection connection = Database.GetSqlConnection())
			{
				string sqlQuery = "UPDATE [Users] SET [DeletedAt] = @DeletedAt WHERE [Id] = @id";
				connection.Open();
				SqlCommand cmd = new SqlCommand(sqlQuery, connection);
				cmd.Parameters.AddWithValue("@DeletedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@Id", id);
				cmd.ExecuteNonQuery();
				checkDelete = true;
				connection.Close();
			}
			return checkDelete;
		}

		public AccountDetail GetDetailAccountById(int id)
		{
			AccountDetail detail = new AccountDetail();
			using (SqlConnection connection = Database.GetSqlConnection())
			{
				string sql = "SELECT * FROM [Users] WHERE [Id] = @id AND [DeletedAt] IS NULL";
				connection.Open();
				SqlCommand cmd = new SqlCommand(sql, connection);
				cmd.Parameters.AddWithValue("@id", id);
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						detail.Id = Convert.ToInt32(reader["Id"]);
						detail.RoleId = Convert.ToInt32(reader["RoleId"]);
						detail.Username = reader["Username"].ToString();
						detail.Password = reader["Password"].ToString();
						detail.ExtraCode = reader["ExtraCode"].ToString();
						detail.Email = reader["Email"].ToString();
						detail.Phone = reader["Phone"].ToString();
						detail.Address = reader["Address"].ToString();
						detail.FullName = reader["FullName"].ToString();
						detail.FirstName = reader["FirstName"].ToString();
						detail.LastName = reader["LastName"].ToString();
						detail.Birthday = Convert.ToDateTime(reader["Birthday"]);
						detail.Gender = reader["Gender"].ToString();
						detail.Education = reader["Education"].ToString();
						detail.ProgrammingLanguage = reader["ProgrammingLanguage"].ToString();
						detail.ToeicScore = reader["ToeicScore"] != DBNull.Value ? Convert.ToInt32(reader["ToeicScore"]) : (int?)null;
						detail.Skill = reader["Skill"].ToString();
						detail.IpClient = reader["IpClient"].ToString();
						detail.LastLogin = reader["LastLogin"] != DBNull.Value ? Convert.ToDateTime(reader["LastLogin"]) : (DateTime?)null;
						detail.LastLogout = reader["LastLogout"] != DBNull.Value ? Convert.ToDateTime(reader["LastLogout"]) : (DateTime?)null;
					}
				}
				connection.Close();
			}
			return detail;
		}


		public List<AccountDetail> GetAllDataAccounts()
		{
			List<AccountDetail> Accounts = new List<AccountDetail>();
			using (SqlConnection connection = Database.GetSqlConnection())
			{
				string sql = "SELECT [us].*, [ro].Name AS RoleName FROM [Users] [us] JOIN [Roles] [ro] ON [us].RoleId = [ro].Id WHERE [us].[DeletedAt] IS NULL";
				connection.Open();
				SqlCommand cmd = new SqlCommand(sql, connection);
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						AccountDetail detail = new AccountDetail();
						detail.Id = Convert.ToInt32(reader["Id"]);
						detail.RoleId = Convert.ToInt32(reader["RoleId"]);
						detail.Username = reader["Username"].ToString();
						detail.Password = reader["Password"].ToString();
						detail.ExtraCode = reader["ExtraCode"].ToString();
						detail.Email = reader["Email"].ToString();
						detail.Phone = reader["Phone"].ToString();
						detail.Address = reader["Address"].ToString();
						detail.FullName = reader["FullName"].ToString();
						detail.FirstName = reader["FirstName"].ToString();
						detail.LastName = reader["LastName"].ToString();
						detail.Birthday = Convert.ToDateTime(reader["Birthday"]);
						detail.Gender = reader["Gender"].ToString();
						detail.Education = reader["Education"].ToString();
						detail.ProgrammingLanguage = reader["ProgrammingLanguage"].ToString();
						detail.ToeicScore = reader["ToeicScore"] != DBNull.Value ? Convert.ToInt32(reader["ToeicScore"]) : (int?)null;
						detail.Skill = reader["Skill"].ToString();
						detail.IpClient = reader["IpClient"].ToString();
						detail.LastLogin = reader["LastLogin"] != DBNull.Value ? Convert.ToDateTime(reader["LastLogin"]) : (DateTime?)null;
						detail.LastLogout = reader["LastLogout"] != DBNull.Value ? Convert.ToDateTime(reader["LastLogout"]) : (DateTime?)null;
						detail.ViewRoleName = reader["RoleName"].ToString();
						Accounts.Add(detail);
					}
				}
				connection.Close();
			}
			return Accounts;
		}


		public bool DeleteItemAccountById(int id = 0)
		{
			bool statusDelete = false;
			using (SqlConnection connection = Database.GetSqlConnection())
			{
				string sqlQuery = "UPDATE [Users] SET [DeletedAt] = @deletedAt WHERE [Id] = @id";
				SqlCommand command = new SqlCommand(sqlQuery, connection);
				connection.Open();
				command.Parameters.AddWithValue("@id", id);
				command.Parameters.AddWithValue("@deletedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				command.ExecuteNonQuery();
				connection.Close();
				statusDelete = true;
			}
			return statusDelete;
		}
		public int InsertAccount(
		string nameAccount,
		int roleId,
		string password,
		string extraCode,
		string email,
		string phone,
		string? address,
		string fullName,
		string? firstName,
		string? lastName,
		DateTime birthday,
		string? gender,
		string? education,
		string? programmingLanguage,
		int? toeicScore,
		string? skill,
		string? ipClient,
		DateTime? lastLogin,
		DateTime? lastLogout
)
		{
			int idAccount = 0;
			using (SqlConnection connection = Database.GetSqlConnection())
			{
				string sqlQuery = "INSERT INTO [Users] ([RoleId], [Username], [Password], [ExtraCode], [Email], [Phone], [Address], [FullName], [FirstName], [LastName], [Birthday], [Gender], [Education], [ProgrammingLanguage], [ToeicScore], [Skill], [IpClient], [LastLogin], [LastLogout], [CreatedAt]) VALUES (@RoleId, @Username, @Password, @ExtraCode, @Email, @Phone, @Address, @FullName, @FirstName, @LastName, @Birthday, @Gender, @Education, @ProgrammingLanguage, @ToeicScore, @Skill, @IpClient, @LastLogin, @LastLogout, @CreatedAt); SELECT SCOPE_IDENTITY();";
				SqlCommand cmd = new SqlCommand(sqlQuery, connection);
				connection.Open();
				cmd.Parameters.AddWithValue("@RoleId", roleId);
				cmd.Parameters.AddWithValue("@Username", nameAccount);
				cmd.Parameters.AddWithValue("@Password", password);
				cmd.Parameters.AddWithValue("@ExtraCode", extraCode);
				cmd.Parameters.AddWithValue("@Email", email);
				cmd.Parameters.AddWithValue("@Phone", phone);
				cmd.Parameters.AddWithValue("@Address", address ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@FullName", fullName);
				cmd.Parameters.AddWithValue("@FirstName", firstName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@LastName", lastName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Birthday", birthday);
				cmd.Parameters.AddWithValue("@Gender", gender ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Education", education ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ProgrammingLanguage", programmingLanguage ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ToeicScore", toeicScore ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Skill", skill ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@IpClient", ipClient ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@LastLogin", lastLogin ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@LastLogout", lastLogout ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
				idAccount = Convert.ToInt32(cmd.ExecuteScalar());
				connection.Close();
			}
			return idAccount;
		}

	}
}
