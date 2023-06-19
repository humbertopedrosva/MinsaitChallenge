namespace Minsait.Challenge.Domain.DTOs
{
    public class ConsolidateReleasesDTO
    {
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public List<ReleaseDTO> Releases { get; set; } = new();
        public decimal Balance { get; set; }
    }
}
