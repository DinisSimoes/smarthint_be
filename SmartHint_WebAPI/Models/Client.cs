namespace SmartHint_WebAPI.Models
{
    public class Client
    {
        public int? ID { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public int telefone { get; set; }
            
        public string tipo { get; set; }
            
        public int? CPF { get; set; }
            
            
        public int? CNPJ { get; set; }

        public string inscricaoEstadual { get; set; }
        public bool bloqueado { get; set; }
        public char genero { get; set; }
        public DateTime dataNascimento { get; set; }

        public DateTime dataCadastro { get; set; }
        public string senha { get; set; }
        

    }
}
