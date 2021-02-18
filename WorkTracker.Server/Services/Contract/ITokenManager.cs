using System;

namespace WorkTracker.Server.Services.Contract
{
    public interface ITokenManager
    {
        String GenerateJwtToken();
    }
}
