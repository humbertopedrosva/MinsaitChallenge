namespace Minsait.Challenge.Domain.MerchantReleases.Interfaces
{
    public interface IMerchantReleaseRemover
    {
        Task DeleteAsync(Guid id);
        Task DeleteAllFromMerchant(Guid merchantId);
    }
}
