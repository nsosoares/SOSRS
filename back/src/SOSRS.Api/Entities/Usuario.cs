namespace SOSRS.Api.Entities
{
    public class Usuario
    {
        public Usuario(Guid id, string user, string password, string cpf)
        {
            Id = id;
            User = user;
            Password = password;
            Cpf = cpf;
        }
        protected Usuario() { }

        public Guid Id { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }
        public string Cpf { get; private set; }

        public IEnumerable<Abrigo> Abrigos { get; private set;}
    }
}
