namespace WebShoes.Domain.Entities.Values
{
    public enum OrderStatus
    {
        Cancelled = 1,
        WaitingPayment = 2,
        PaymentApproved = 3,
        PaymentReproved = 4,
        Shipping = 5
    }
}
