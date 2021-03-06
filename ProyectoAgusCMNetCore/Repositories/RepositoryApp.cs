//using NugetProyectoAgus;
//using ProyectoAgusCMNetCore.Data;
//using ProyectoAgusCMNetCore.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ProyectoAgusCMNetCore.Repositories
//{
//    public class RepositoryApp
//    {
//        private MainContext context;

//        public RepositoryApp(MainContext context)
//        {
//            this.context = context;
//        }

//        public List<User> GetUsers()
//        {
//            var consulta = from datos in this.context.Users select datos;

//            return consulta.ToList();
//        }

//        public void CreateUser(string nombre, string apellidos, string username, string usermail, string password)
//        {
//            User user = new User();
//            user.Userid = GetLastUserId();
//            user.Name = nombre;
//            user.LastName = apellidos;
//            user.UserName = username;
//            user.Email = usermail;
//            user.Password = password;
//            user.Admin = false;
//            user.CreatedAt = DateTime.Now;
//            user.Groups = "1";
//            //
//            this.context.Users.Add(user);
//            this.context.SaveChanges();
//        }

//        private int GetLastUserId()
//        {
//            var consulta = (from datos in this.context.Users select datos).OrderBy(x => x.Userid).LastOrDefault();

//            if (consulta == null)
//            {
//                return 1;
//            }

//            int id = consulta.Userid;

//            return id + 1;
//        }

//        private int GetLastTicketId()
//        {
//            var consulta = (from datos in this.context.Tickets select datos).OrderBy(x => x.idticket).LastOrDefault();

//            if (consulta == null)
//            {
//                return 1;
//            }

//            int id = consulta.idticket;

//            return id + 1;
//        }

//        private string GetLastGroupId()
//        {
//            var consulta = (from datos in this.context.Groups select datos).OrderBy(x => x.idgroup).LastOrDefault();

//            if (consulta == null)
//            {
//                return "1";
//            }

//            int id = int.Parse(consulta.idgroup);

//            int resultado = id + 1;

//            return resultado.ToString();
//        }
//        private int GetLastCommentId()
//        {
//            var consulta = (from datos in this.context.Comments select datos).OrderBy(x => x.IdComentario).LastOrDefault();

//            if (consulta == null)
//            {
//                return 1;
//            }

//            int id = consulta.IdComentario;

//            return id + 1;
//        }

//        public User ExisteUser(string username, string password)
//        {
//            var consulta = from datos in this.context.Users where datos.UserName == username && datos.Password == password select datos;

//            return consulta.FirstOrDefault();
//        }

//        public List<Group> GetGroups()
//        {
//            var consulta = from datos in this.context.Groups select datos;

//            return consulta.ToList();
//        }

//        public void SaveTicket(string titulo, string tickettext, int usuariocreado, int grupo)
//        {
//            Ticket ticket = new Ticket();
//            ticket.idticket = GetLastTicketId();
//            ticket.titulo = titulo;
//            ticket.textoticket = tickettext;
//            ticket.usuariocreado = usuariocreado;
//            ticket.usuarioasignado = 0;
//            ticket.grupoasingado = grupo;
//            ticket.fechacreacion = DateTime.Now;
//            ticket.estadoticket = "Sin Asignar";

//            this.context.Tickets.Add(ticket);
//            this.context.SaveChanges();
//        }

//        public void CreateGroup(string name)
//        {
//            Group g = new Group();
//            g.idgroup = GetLastGroupId();
//            g.name = name;
//            this.context.Groups.Add(g);
//            this.context.SaveChanges();
//        }

//        public List<Ticket> GetCreatedTickets(int iduser)
//        {
//            var consulta = from datos in this.context.Tickets where datos.usuariocreado == iduser select datos;

//            return consulta.ToList();
//        }

//        public Ticket FindTicket(int idticket)
//        {
//            var consulta = from datos in this.context.Tickets where datos.idticket == idticket select datos;

//            return consulta.FirstOrDefault();
//        }

//        public List<Ticket> GetTicketsSoporte()
//        {
//            var consulta = from datos in this.context.Tickets where datos.grupoasingado == 3 && datos.usuarioasignado == 0 select datos;
//            return consulta.ToList();
//        }

//        public List<Ticket> GetTickets()
//        {
//            var consulta = from datos in this.context.Tickets select datos;
//            return consulta.ToList();
//        }

//        public List<Ticket> GetTicketsDesarrollo()
//        {
//            var consulta = from datos in this.context.Tickets where datos.grupoasingado == 2 && datos.usuarioasignado == 0 select datos;
//            return consulta.ToList();
//        }

//        public User FindUser(int id)
//        {
//            var consulta = from datos in this.context.Users where datos.Userid == id select datos;

//            return consulta.FirstOrDefault();
//        }

//        public void AsignMeTicket(int idUser, int idTicket)
//        {
//            Ticket t = FindTicket(idTicket);
//            t.usuarioasignado = idUser;
//            t.estadoticket = "En proceso";
//            this.context.SaveChanges();
//        }

//        public List<Ticket> GetAssignedTickets(int idUser)
//        {
//            var consulta = from datos in this.context.Tickets where datos.usuarioasignado == idUser select datos;

//            return consulta.ToList();
//        }

//        public void CreateComment(int idTicket, string mensaje, string User)
//        {
//            Comment com = new Comment();
//            com.IdComentario = GetLastCommentId();
//            com.idTicket = idTicket;
//            com.Comentario = mensaje;
//            com.ComentarioUsuario = User;
//            com.FechaComentario = DateTime.Now;

//            this.context.Comments.Add(com);

//            this.context.SaveChanges();
//        }

//        public List<Comment> GetComments(int idTicket)
//        {
//            var consulta = from datos in this.context.Comments where datos.idTicket == idTicket select datos;

//            return consulta.ToList();
//        }

//        public void CompleteTicket(int idTicket, string User)
//        {
//            Ticket t = FindTicket(idTicket);
//            t.estadoticket = "Completado";

//            CreateComment(t.idticket, "Completado", User);

//            this.context.SaveChanges();
//        }

//        public List<User> GetUsersGroup(string idgroup)
//        {
//            var consulta = from datos in this.context.Users where datos.Groups == idgroup select datos;

//            return consulta.ToList();
//        }

//        public string GetNameGroup(string idgroup)
//        {
//            var consulta = from datos in this.context.Groups where datos.idgroup == idgroup select datos.name;

//            return consulta.FirstOrDefault().ToString();
//        }

//        public void EditUser(int iduser, string address, string phone, string profesion)
//        {
//            User usu = FindUser(iduser);
//            usu.Address = address;
//            usu.Phone = phone;
//            usu.Profession = profesion;

//            this.context.SaveChanges();
//        }

//        public void DeleteUser(int iduser)
//        {
//            User usu = FindUser(iduser);

//            this.context.Users.Remove(usu);

//            this.context.SaveChanges();
//        }

//        public Group FindGroup(String idGroup)
//        {
//            var consulta = from datos in this.context.Groups where datos.idgroup == idGroup select datos;

//            return consulta.FirstOrDefault();
//        }

//        public void DeleteGroup(string idgroup)
//        {
//            Group g = FindGroup(idgroup);

//            this.context.Groups.Remove(g);

//            this.context.SaveChanges();
//        }
//    }
//}
