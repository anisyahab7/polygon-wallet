using PolyWallet.Engine.Domain.Extensions;
using PolyWallet.Engine.Domain.Models;

namespace PolyWallet.Engine.Validation;

public sealed class TransactionValidator
{
    public bool IsStructurallyValid(TransactionRecord record)
    {
        if (string.IsNullOrWhiteSpace(record.TransactionHash))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(record.FromAddress) || string.IsNullOrWhiteSpace(record.ToAddress))
        {
            return false;
        }

        if (record.Value < 0m)
        {
            return false;
        }

        if (record.FromAddress.LooksLikeHexAddress() && !record.FromAddress.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return true;
    }

    public IReadOnlyList<TransactionRecord> FilterValid(IEnumerable<TransactionRecord> source) =>
        source.Where(IsStructurallyValid).ToList();
}
