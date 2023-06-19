namespace Minsait.Challenge.Domain.Merchants.Interfaces
{
    public interface IMerchantRemover
    {
        Task DeleteAsync(Guid id);
    }
}
