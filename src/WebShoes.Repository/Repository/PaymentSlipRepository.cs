using WebShoes.Domain.Entities;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Repository.Repository
{
    public class PaymentSlipRepository : BaseRepository<PaymentSlip>, IPaymentSlipRepository
    {
        private IPaymentSlipRepository _paymentSlipRepository;
        public PaymentSlip GetByReference(string reference)
        {
            return _paymentSlipRepository.GetByReference(reference);
        }
    }
}
