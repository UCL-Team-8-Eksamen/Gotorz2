﻿using System.Security.Cryptography;
using System.Text;

namespace Gotorz2
{
	public class User
	{
		public string Email { get; }
		public string Role { get; set; }
		public User(string email, string role = "user")
		{
			Email = email;
			Role = role;
		}
	}
	public interface IUserDatabase
	{
		Task<User> AuthenticateUser(string email, string password);
		Task<User> AddUser(string email, string password);
	}
	public class UserDatabase : IUserDatabase
	{
		private readonly IWebHostEnvironment env;
		public UserDatabase(IWebHostEnvironment env) => this.env = env;
		private static string CreateHash(string password)
		{
			var salt = "997eff51db1544c7a3c2ddeb2053f052";
			var h = new HMACSHA256(Encoding.UTF8.GetBytes(salt + password));
			byte[] data = h.ComputeHash(Encoding.UTF8.GetBytes(password));
			return System.Convert.ToBase64String(data);
		}
		public async Task<User> AuthenticateUser(string email, string password)
		{
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
				return null;
			var path = System.IO.Path.Combine(env.ContentRootPath, "Users");
			if (!System.IO.Directory.Exists(path))
				return null;
			path = System.IO.Path.Combine(path, email);
			if (!System.IO.File.Exists(path))
				return null;
			if (await System.IO.File.ReadAllTextAsync(path) != CreateHash(password))
				return null;
			return new User(email);
		}
		public async Task<User> AddUser(string email, string password)
		{
			try
			{
				if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
					return null;
				var path = System.IO.Path.Combine(env.ContentRootPath, "Users");
				if (!System.IO.Directory.Exists(path))
					System.IO.Directory.CreateDirectory(path);
				path = System.IO.Path.Combine(path, email);
				if (System.IO.File.Exists(path))
					return null;
				await System.IO.File.WriteAllTextAsync(path, CreateHash(password));
				return new User(email);
			}
			catch
			{
				return null;
			}
		}
	}
}