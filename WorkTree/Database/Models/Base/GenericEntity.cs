namespace WorkTree.Database.Models.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        protected BaseEntity()
        {// Gera automaticamente um novo Guid para o Id
         // quando uma instância é criada.
            Id = Guid.NewGuid();
        }

        public virtual bool IsValid()
        {
            //Validacoes genericas
            return true;
        }
    }
}