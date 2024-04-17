using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace TrainingFPTCo.Models.Queries
{
	public class RoleQuery
	{
		public List<RoleDetail> GetAllRoles()
		{
			List<RoleDetail> roles = new List<RoleDetail>();
			using (SqlConnection connection = Database.GetSqlConnection())
			{
				string sql = "SELECT * FROM [Roles]";
				connection.Open();
				SqlCommand cmd = new SqlCommand(sql, connection);
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						RoleDetail role = new RoleDetail();
						role.Name = reader["Name"].ToString();
						role.Description = reader["Description"].ToString();
						role.Status = reader["Status"].ToString();
						roles.Add(role);
					}
				}
				connection.Close();
			}
			return roles;
		}
	}

	public class RoleDetail
	{
		public string? Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Status { get; set; }

	}
}
