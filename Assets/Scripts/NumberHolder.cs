using MLAPI;
using MLAPI.NetworkVariable;
using TMPro;
using UnityEngine;

namespace DapperDino.CardGame
{
    public class NumberHolder : NetworkBehaviour
    {
        [SerializeField] private TMP_Text numberText;

        private NetworkVariable<int> myNumber = new NetworkVariable<int>(new NetworkVariableSettings
        {
            ReadPermission = NetworkVariablePermission.OwnerOnly
        });

        public override void NetworkStart()
        {
            if (IsServer)
            {
                if (NetworkManager.Singleton.ConnectedClientsList.Count == 1)
                {
                    transform.position = new Vector3(0f, 3f, 0f);
                }
                else
                {
                    transform.position = new Vector3(0f, -3f, 0f);
                }
            }

            myNumber.OnValueChanged += HandleNumberChanged;
        }

        private void OnDestroy()
        {
            myNumber.OnValueChanged -= HandleNumberChanged;
        }

        private void HandleNumberChanged(int oldValue, int newValue)
        {
            numberText.text = newValue.ToString();
        }

        public void UpdateNumber(int newNumber)
        {
            myNumber.Value = newNumber;
        }
    }
}
