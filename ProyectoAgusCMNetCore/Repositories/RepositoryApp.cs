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

        private int GetLastTicketId()
        {
            var consulta = (from datos in this.context.Tickets select datos).OrderBy(x => x.idticket).LastOrDefault();

            if (consulta == null)
            {
                return 1;
            }

            int id = consulta.idticket;

            return id + 1;
        }

        private int GetLastGroupId()
        {
            var consulta = (from datos in this.context.Groups select datos).OrderBy(x => x.idgroup).LastOrDefault();

            if (consulta == null)
            {
                return 1;
            }

            int id = consulta.idgroup;

            return id + 1;
        }

        public User ExisteUser(string username, string password)
        {
            var consulta = from datos in this.context.Users where datos.UserName == username && datos.Password == password select datos;

            return consulta.FirstOrDefault();
        }

        public List<Group> GetGroups()
        {
            var consulta = from datos in this.context.Groups where datos.idgroup != 1 select datos;

            return consulta.ToList();
        }

        public void SaveTicket(string titulo, string tickettext, int usuariocreado, int grupo)
        {
            Ticket ticket = new Ticket();
            ticket.idticket = GetLastTicketId();
            ticket.titulo = titulo;
            ticket.textoticket = tickettext;
            ticket.usuariocreado = usuariocreado;
            ticket.usuarioasignado = 0;
            ticket.grupoasingado = grupo;
            ticket.fechacreacion = DateTime.Now;

            this.context.Tickets.Add(ticket);
            this.context.SaveChanges();            
        }

        public void CreateGroup(string name)
        {
            Group g = new Group();
            g.idgroup = GetLastGroupId();
            g.name = name;
            this.context.Groups.Add(g);
            this.context.SaveChanges();
        }

        public List<Ticket> GetCreatedTickets(int iduser)
        {
            var consulta = from datos in this.context.Tickets where datos.usuariocreado == iduser select datos;

            return consulta.ToList();
        }

        public Ticket FindTicket(int idticket)
        {
            var consulta = from datos in this.context.Tickets where datos.idticket == idticket select datos;

            return consulta.FirstOrDefault();
        }
    }
}
