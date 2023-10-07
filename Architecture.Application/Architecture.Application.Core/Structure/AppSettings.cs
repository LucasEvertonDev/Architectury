﻿namespace Architecture.Application.Core.Structure;

public class AppSettings
{
    public Connectionstrings ConnectionStrings { get; set; }
    public Logging Logging { get; set; }
    public MemoryCache MemoryCache { get; set; }
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

public class MemoryCache
{
    public long SlidingExpirationInMinutes { get; set; }

    public long AbsoluteExpirationInMinutes { get; set; }
}
