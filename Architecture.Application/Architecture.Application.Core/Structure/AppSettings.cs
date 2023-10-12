namespace Architecture.Application.Core.Structure;
public class AppSettings
{
    public Connectionstrings ConnectionStrings { get; set; }
    public Logging Logging { get; set; }
    public Memorycache MemoryCache { get; set; }
    public Swagger Swagger { get; set; }
    public Jwt Jwt { get; set; }
}

public class Connectionstrings
{
    public string SqlConnection { get; set; }
}

public class Logging
{
    public Loglevel LogLevel { get; set; }
}

public class Loglevel
{
    public string Default { get; set; }
    public string MicrosoftAspNetCore { get; set; }
}

public class Memorycache
{
    public int SlidingExpirationInMinutes { get; set; }
    public int AbsoluteExpirationInMinutes { get; set; }
}

public class Swagger
{
    public string FlowLogin { get; set; }
}

public class Jwt
{
    public string Key { get; set; }
    public int ExpireInMinutes { get; set; }
}
