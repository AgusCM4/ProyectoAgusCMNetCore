using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NugetProyectoAgus;
using ProyectoAgusCMNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Services
{
    public class ServiceApiProyecto
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;
        private IConfiguration configuration1;

        public ServiceApiProyecto(string urlapi, IConfiguration configuration)
        {
            this.UrlApi = urlapi;
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
            configuration1 = configuration;
        }

        public async Task<string> GetTokenAsync(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                LoginModel model = new LoginModel
                {
                    UserName = username,
                    Password = password
                };
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/Prod/auth/login";
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(data);
                    string token = jObject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        //METODO CON SEGURIDAD QUE RECIBE EL TOKEN
        private async Task<T> CallApiAsync<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }

            }
        }

        public async Task<User> PerfilUser(string token)
        {
            string request = "/Prod/PerfilUser";
            User usu = await this.CallApiAsync<User>(request, token);
            return usu;
        }

        public async Task<List<User>> GetUsers(string token)
        {
            string request = "/Prod/GetUsers";
            List<User> usuarios = await this.CallApiAsync<List<User>>(request, token);
            return usuarios;
        }

        public async Task<List<Group>> GetGroups(string token)
        {
            string request = "/Prod/GetGroups";
            List<Group> grupos = await this.CallApiAsync<List<Group>>(request, token);
            return grupos;
        }

        public async Task<List<Ticket>> GetCreatedTickets(string token, int idticket)
        {
            string request = "/Prod/GetCreatedTickets/" + idticket;
            List<Ticket> tickets = await this.CallApiAsync<List<Ticket>>(request, token);
            return tickets;
        }

        public async Task<Ticket> FindTicket(string token, int idticket)
        {
            string request = "/Prod/FindTicket/" + idticket;
            Ticket tic = await this.CallApiAsync<Ticket>(request, token);
            return tic;
        }

        public async Task<List<Ticket>> GetTicketSoporte(string token)
        {
            string request = "/Prod/GetTicketsSoporte";
            List<Ticket> tickets = await this.CallApiAsync<List<Ticket>>(request, token);
            return tickets;
        }

        public async Task<List<Ticket>> GetTickets(string token)
        {
            string request = "/Prod/GetTickets";
            List<Ticket> tickets = await this.CallApiAsync<List<Ticket>>(request, token);
            return tickets;
        }

        public async Task<List<Ticket>> GetTicketsDesarrollo(string token)
        {
            string request = "/Prod/GetTicketsDesarrollo";
            List<Ticket> tickets = await this.CallApiAsync<List<Ticket>>(request, token);
            return tickets;
        }

        public async Task<User> FindUser(string token, int iduser)
        {
            string request = "/Prod/FindUser/" + iduser;
            User usu = await this.CallApiAsync<User>(request, token);
            return usu;
        }

        public async Task AssignMeTicket(string token, int iduser, int idticket)
        {
            string request = "/Prod/AsignMeTicket/" + iduser + "/" + idticket;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
            }
        }

        public async Task CompleteTicket(string token, int idticket, string user)
        {
            string request = "/Prod/CompleteTicket/" + idticket + "/" + user;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
            }
        }

        public async Task<List<Ticket>> GetAssignedTickets(string token, int iduser)
        {
            string request = "/Prod/GetAssignedTickets/" + iduser;
            List<Ticket> usu = await this.CallApiAsync<List<Ticket>>(request, token);
            return usu;
        }

        public async Task<List<Comment>> GetComments(string token, int idticket)
        {
            string request = "/Prod/GetCommments/" + idticket;
            List<Comment> comentarios = await this.CallApiAsync<List<Comment>>(request, token);
            return comentarios;
        }

        public async Task<List<User>> GetUsersGroup(string token, string idgroup)
        {
            string request = "/Prod/GetUsersGroup/" + idgroup;
            List<User> users = await this.CallApiAsync<List<User>>(request, token);
            return users;
        }

        public async Task<string> GetNameGroup(string token, string idgroup)
        {
            string request = "/Prod/GetNameGroup/" + idgroup;
            string name = await this.CallApiAsync<string>(request, token);
            return name;
        }

        public async Task<Group> FindGroup(string token, string idgroup)
        {
            string request = "/Prod/FindGroup/" + idgroup;
            Group gru = await this.CallApiAsync<Group>(request, token);
            return gru;
        }

        public async Task CreateUser(string nombre, string apellidos, string username, string usermail, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/Prod/CreateUser";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                User usu = new User()
                {                    
                    Name = nombre,
                    LastName = apellidos,
                    UserName=username,
                    Email=usermail,
                    Password=password
                };
                string json = JsonConvert.SerializeObject(usu);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task CreateGroup(string token, string nombre)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/Prod/CreateUser";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                Group usu = new Group()
                {
                    name = nombre
                };
                string json = JsonConvert.SerializeObject(usu);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task CreateComment(string token, int idTicket, string mensaje, string user)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/Prod/CreateUser";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                Comment com = new Comment()
                {
                    idTicket=idTicket,
                    Comentario=mensaje,
                    ComentarioUsuario=user
                };
                string json = JsonConvert.SerializeObject(com);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task SaveTicket(string token, string titulo, string tickettext, int usuariocreado, int grupo)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/Prod/SaveTicket";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                Ticket t = new Ticket()
                {
                    idticket = 0,
                    titulo=titulo,
                    textoticket=tickettext,
                    usuariocreado=usuariocreado,
                    grupoasingado=grupo
                };
                string json = JsonConvert.SerializeObject(t);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task EditUser(string token, int iduser, string address, string phone, string profesion)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/Prod/EditUser";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                User u = new User()
                {
                    Userid=iduser,
                    Address=address,
                    Phone=phone,
                    Profession=profesion
                };
                string json = JsonConvert.SerializeObject(u);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeleteUser(string token, int iduser)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/Prod/DeleteUser/" + iduser;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task DeleteGroup(string token, string idgroup)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/Prod/DeleteGroup/" + idgroup;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task SendMail(string email, string subject, string body)
        {
            var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1);
            Destination destination = new Destination();
            destination.ToAddresses = new List<string> { email };
            Message message = new Message();
            message.Subject = new Content(subject);
            Body cuerpo = new Body();
            cuerpo.Html = new Content(body);
            cuerpo.Text = new Content(body);
            message.Body = cuerpo;
            SendEmailRequest request = new SendEmailRequest();
            //QUIEN ENVIA EL MENSAJE (NUESTRO USER VERIFIED EN SERVICIO SES)
            request.Source = "agustincampostajamar@gmail.com";
            request.Destination = destination;
            request.Message = message;
            SendEmailResponse response = await client.SendEmailAsync(request);           
        }
    }
}
