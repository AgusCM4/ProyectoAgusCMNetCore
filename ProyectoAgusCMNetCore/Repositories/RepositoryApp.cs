using ProyectoAgusCMNetCore.Data;
using ProyectoAgusCMNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Repositories
{
    public class RepositoryApp
    {
        private MainContext context;

        public RepositoryApp(MainContext context)
        {
            this.context = context;
        }

        public List<User> GetUsers()
        {
            var consulta = from datos in this.context.Users select datos;

            return consulta.ToList();
        }

        public void CreateUser(string nombre, string apellidos, string username, string usermail, string password)
        {
            User user = new User();
            user.Userid = GetLastUserId();
            user.Name = nombre;
            user.LastName = apellidos;
            user.UserName = username;
            user.Email = usermail;
            user.Password = password;
            user.Admin = false;
            user.CreatedAt = DateTime.Now;
            user.Groups = "1";
            //
            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        private int GetLastUserId()
        {
            var consulta = (from datos in this.context.Users select datos).OrderBy(x=>x.Userid).LastOrDefault();

            if (consulta == null)
            {
                return 1;
            }

            int id = consulta.Userid;

            return id + 1;
        }

        public User ExisteUser(string username, string password)
        {
            var consulta = from datos in this.context.Users where datos.UserName == username && datos.Password == password select datos;

            return consulta.FirstOrDefault();
        }
    }
}
