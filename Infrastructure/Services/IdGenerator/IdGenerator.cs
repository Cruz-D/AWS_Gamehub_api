using System;

public class IdGenerator : IIdGenerator
{
    public string GenerateId()
    {
        return Guid.NewGuid().ToString();
    }

}
