namespace Simulator;

internal class Animals
{
    private string _description = "Unknown";

    public string Description
    {
        get => _description;
        init
        {
            var trimmed = value.Trim();
            if (trimmed.Length < 3)
                _description = trimmed.PadRight(3, '#');
            else if (trimmed.Length > 15)
                _description = trimmed.Substring(0, 15).TrimEnd().PadRight(3, '#');
            else
                _description = trimmed;
        }
    }

    public uint Size { get; set; } = 3;

    public string Info => $"{Description} <{Size}>";
}