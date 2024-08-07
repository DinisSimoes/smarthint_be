﻿using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SmartHint_WebAPI.Models;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHint_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        // GET: api/<ClientController>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Client client = new Client();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
                {
                    connection.Open();
                    string query = $"Select * from Clientes where ID=@id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                client.ID = reader.GetInt32("id");
                                client.nome = reader.GetString("nome");
                                client.email = reader.GetString("email");
                                client.telefone = reader.GetInt32("telefone");
                                client.tipo = reader.GetString("tipo");
                                client.CPF = reader.IsDBNull("CPF") ? null : reader.GetInt32("CPF");
                                client.CNPJ = reader.IsDBNull("CNPJ") ? null : reader.GetInt32("CNPJ");
                                client.inscricaoEstadual = reader.GetString("inscricaoEstadual");
                                client.bloqueado = reader.GetBoolean("bloqueado");
                                client.genero = reader.IsDBNull("genero") ? null : Convert.ToChar(reader.GetString("genero"));
                                client.dataNascimento = reader.IsDBNull("dataNascimento") ? null : reader.GetDateTime("dataNascimento");
                                client.dataCadastro = reader.GetDateTime("dataCadastro");
                                client.senha = reader.GetString("senha");
                                return Ok(client);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("verifyMail")]
        public IActionResult verifyMail(string email)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
                {
                    connection.Open();
                    string query = $"Select email from Clientes where email=@email";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("email", email);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return BadRequest();
                            }
                            else
                            {
                                return Ok();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("verifyCPF")]
        public IActionResult verifyCPF(Int64? CPF = null)
        {
            try
            {
                if (CPF is not null)
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
                    {
                        connection.Open();
                        string query = $"Select CPF from Clientes where CPF=@CPF";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("CPF", CPF);
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    return BadRequest();
                                }
                                else
                                {
                                    return Ok();
                                }
                            }
                        }
                    }
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("verifyCNPJ")]
        public IActionResult verifyCNPJ(Int64? CNPJ = null)
        {
            try
            {
                if (CNPJ is not null)
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
                    {
                        connection.Open();
                        string query = $"Select CNPJ from Clientes where CNPJ=@CNPJ";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("CNPJ", CNPJ);
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    return BadRequest();
                                }
                                else
                                {
                                    return Ok();
                                }
                            }
                        }
                    }
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("verifyInscricao")]
        public IActionResult verifyInscricao(string inscricaoEstadual)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
                {
                    connection.Open();
                    string query = $"Select inscricaoEstadual from Clientes where inscricaoEstadual=@inscricaoEstadual";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("inscricaoEstadual", inscricaoEstadual);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return BadRequest();
                            }
                            else
                            {
                                return Ok();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ClientController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("GetAllClients")]
        public IActionResult GetAllClients()
        {
            try
            {
                SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("SQLConnection").ToString());
                SqlDataAdapter da = new SqlDataAdapter("Select * from Clientes", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<Client> clientList = new List<Client>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Client cl = new Client();
                    cl.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    cl.nome = Convert.ToString(dt.Rows[i]["nome"].ToString());
                    cl.email = Convert.ToString(dt.Rows[i]["email"]);
                    cl.telefone = Convert.ToInt32(dt.Rows[i]["telefone"]);

                    cl.tipo = Convert.ToString(dt.Rows[i]["tipo"]);


                    if (dt.Rows[i]["CPF"] is null)
                    {
                        cl.CPF = Convert.ToInt32(dt.Rows[i]["CPF"]);
                    }
                    else
                    {
                        cl.CPF = null;
                    }


                    if (dt.Rows[i]["CNPJ"] is null)
                    {
                        cl.CNPJ = Convert.ToInt32(dt.Rows[i]["CNPJ"]);
                    }
                    else
                    {
                        cl.CNPJ = null;
                    }


                    cl.inscricaoEstadual = Convert.ToString(dt.Rows[i]["inscricaoEstadual"]);
                    cl.bloqueado = Convert.ToBoolean(dt.Rows[i]["bloqueado"]);

                    if (dt.Rows[i]["genero"] is null)
                    {
                        cl.genero = Convert.ToChar(dt.Rows[i]["genero"]);
                    }
                    else
                    {
                        cl.genero = null;
                    }

                    if (dt.Rows[i]["dataNascimento"] is null)
                    {
                        cl.dataNascimento = Convert.ToDateTime(dt.Rows[i]["dataNascimento"]);
                    }
                    else
                    {
                        cl.dataNascimento = null;
                    }
                    
                    
                    cl.dataCadastro = Convert.ToDateTime(dt.Rows[i]["dataCadastro"]);
                    cl.senha = Convert.ToString(dt.Rows[i]["senha"]);
                    clientList.Add(cl);
                }
                if (clientList.Count > 0)
                    return Ok(clientList);
                else
                {
                    return BadRequest("No data found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("EditClient")]
        public IActionResult EditClient(Client client)
        {
            bool isInsert = false;
            string query = string.Empty;

            if (client.ID.Equals(-1))
            {
                isInsert = true;
                query = $@"insert into 
                    Clientes (nome, email, telefone, tipo, CPF, CNPJ, inscricaoEstadual, bloqueado, genero, dataNascimento, senha, dataCadastro)
                    values (@nome, @email, @telefone, @tipo, @CPF, @CNPJ, @inscricaoEstadual, @bloqueado, @genero, @dataNascimento, @senha, @dataCadastro)";
            }
            else
            {
                isInsert = false;
                query = $@"Update Clientes 
                        set 
                            nome = @nome, 
                            email = @email, 
                            telefone = @telefone, 
                            tipo = @tipo, 
                            CPF = @CPF, 
                            CNPJ = @CNPJ, 
                            inscricaoEstadual = @inscricaoEstadual, 
                            bloqueado = @bloqueado, 
                            genero = @genero, 
                            dataNascimento = @dataNascimento, 
                            senha = @senha       
                        where id=@id";
            }

            try
            {
                using (var conn = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
                {
                    conn.Open();


                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@nome", client.nome);
                        command.Parameters.AddWithValue("@id", client.ID);
                        command.Parameters.AddWithValue("@email", client.email);
                        command.Parameters.AddWithValue("@telefone", client.telefone);
                        command.Parameters.AddWithValue("@tipo", client.tipo);
                        command.Parameters.AddWithValue("@CPF", client.CPF == null ? DBNull.Value : client.CPF);
                        command.Parameters.AddWithValue("@CNPJ", client.CNPJ == null ? DBNull.Value : client.CNPJ);
                        command.Parameters.AddWithValue("@inscricaoEstadual", client.inscricaoEstadual);
                        command.Parameters.AddWithValue("@bloqueado", client.bloqueado == null ? DBNull.Value : client.bloqueado);
                        command.Parameters.AddWithValue("@genero", client.genero == null ? DBNull.Value : client.genero);
                        command.Parameters.AddWithValue("@dataNascimento", client.dataNascimento == null ? DBNull.Value : client.dataNascimento);
                        command.Parameters.AddWithValue("@senha", client.senha);
                        if (isInsert)
                        {
                            command.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
                        }

                            command.ExecuteNonQuery();

                        return Ok();
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public readonly IConfiguration _configuration;

        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
