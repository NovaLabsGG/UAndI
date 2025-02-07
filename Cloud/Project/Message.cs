using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;

namespace Cloud;

public class Message
{
    [CloudCodeFunction("Message")]
    public async Task<string> SendPlayerMessage(
        IExecutionContext context,
        PushClient pushClient,
        string playerId,
        string type,
        string message
    )
    {
        var response = await pushClient.SendPlayerMessageAsync(context, message, type, playerId);
        return response.ToString();
    }

    public class ModuleConfig : ICloudCodeSetup
    {
        public void Setup(ICloudCodeConfig config)
        {
            config.Dependencies.AddSingleton(PushClient.Create());
        }
    }
}