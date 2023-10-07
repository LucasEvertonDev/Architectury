namespace Architecture.Application.Core.Structure.Attributes;

public class CacheAttribute : Attribute
{
    public string Key { get; set; }

    public long? SlidingExpirationInMinutes { get; set; }

    public long? AbsoluteExpirationInMinutes { get; set; }

    public CacheAttribute(string Key, long SlidingExpirationInMinutes, long AbsoluteExpirationInMinutes)
    {
        this.Key = Key;
        this.SlidingExpirationInMinutes = SlidingExpirationInMinutes;
        this.AbsoluteExpirationInMinutes = AbsoluteExpirationInMinutes;
    }
}
