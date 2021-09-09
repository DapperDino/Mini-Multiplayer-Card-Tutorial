using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using UnityEngine;

namespace DapperDino.CardGame
{
    public class NumberGenerator : NetworkBehaviour
    {
        public void GenerateNewNumber() => GenerateNewNumberServerRpc();

        [ServerRpc(RequireOwnership = false)]
        private void GenerateNewNumberServerRpc(ServerRpcParams serverRpcParams = default)
        {
            ulong senderId = serverRpcParams.Receive.SenderClientId;

            NetworkObject playerObject = NetworkSpawnManager.GetPlayerNetworkObject(senderId);

            if (playerObject == null) { return; }

            var numberHolder = playerObject.GetComponent<NumberHolder>();

            numberHolder.UpdateNumber(Random.Range(0, 101));
        }
    }
}
