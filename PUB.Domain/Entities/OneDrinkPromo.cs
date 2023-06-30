namespace PUB.Domain.Entities
{
    public class OneDrinkPromo : EntityBase
    {
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public DateTime? Birth { get; private set; }
        public bool IsUsed { get; private set; }

        public OneDrinkPromo(string name, string cpf, DateTime? birth = null)
        {
            Name = name;
            Cpf = cpf;
            Birth = birth;
            IsUsed = false;
        }

        public void UsePromo()
        {
            IsUsed = true;
        }

        public void Normalize()
        {
            Cpf = Cpf.Trim().Replace(".", "").Replace("-", "");
        }
    }
}