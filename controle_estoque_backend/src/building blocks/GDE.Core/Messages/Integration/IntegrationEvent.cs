using FluentValidation.Results;
using GDE.Core.Data;

namespace GDE.Core.Messages.Integration
{
    public abstract class IntegrationEvent : Event
    {
        public ValidationResult? ValidationResult { get; set; }

        protected IntegrationEvent()
        {
            ValidationResult = new ValidationResult();
        }

        public void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        public async Task<ValidationResult> PersistirDados(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Commit())
                AdicionarErro("Hove um erro ao persistir os dados.");

            return ValidationResult;
        }
    }
}
