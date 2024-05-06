using SOSRS.Api.Helpers;

namespace SOSRS.Api.ValueObjects;

public class SearchableStringVO
{
    public SearchableStringVO(string value)
    {
        Value = value;
        SearchableValue = value.ToSerachable();
    }

    public string? Value { get; private set; }
    public string? SearchableValue { get; private set; }
}
