using Asp.net_Core_Web_API_Assignment_Backend.Interfaces;
using Asp.net_Core_Web_API_Assignment_Backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_Core_Web_API_Assignment_Backend.Repositories
{
    public class UserRepo : IRepo<User, int, bool>, IAuth<User>
    {
        private readonly UserDbContext db;
        public UserRepo(UserDbContext db)
        {
            
            
            this.db = db;
        }

        public bool Add(User user)
        {
            //string sql = @"
                //CREATE TABLE Users (
                //Id INT IDENTITY(1,1) PRIMARY KEY,
                //Username VARCHAR(50),
                //Password VARCHAR(50),
                //Email VARCHAR(50),
                //PhoneNumber VARCHAR(50),
                //Address VARCHAR(50)
            //);";
            //var data = db.Database.ExecuteSqlRaw(sql);

            string sqldata = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
            db.Database.ExecuteSqlRaw(sqldata,
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Password", user.Password)
               
                );

            if (user != null)
            {
               
                return true;
            }
            else
            {
                return false;
            }

        }

        public User Authenticate(string username, string password)
        {
            string sql = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
            var user = db.Users.FromSqlRaw(sql,
               new SqlParameter("@Username", username),
               new SqlParameter("@Password", password)
               ).AsEnumerable().FirstOrDefault();

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            string sql = "DELETE FROM Users WHERE Id = @Id";
            if (id > 0)
            {
                db.Database.ExecuteSqlRaw(sql,
                new SqlParameter("@Id", id));
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<User> GetAll()
        {
            string sql = "SELECT * FROM Users";
            var data = db.Users.FromSqlRaw(sql).AsEnumerable();
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public User Get(int id)
        {
            string sql = "SELECT * FROM Users WHERE Id = @Id";
            var user = db.Users.FromSqlRaw(sql, 
                new SqlParameter("@Id", id))
                .AsEnumerable()
                .FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public bool Update(User user)
        {
            string sql = "UPDATE Users SET Username = @Username, Password = @Password WHERE Id = @Id";
            db.Database.ExecuteSqlRaw(sql,
                new SqlParameter("@Id", user.Id),
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Password", user.Password)
               
                 );
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
