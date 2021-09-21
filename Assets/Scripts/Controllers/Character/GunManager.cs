using Controllers.Weapon;

using UnityEngine;

namespace Controllers.Character {
    public class GunManager : MonoBehaviour {
        [Header("References")]
        [SerializeField]
        private Transform equipSocket;
        [SerializeField] private GameObject equipped;

        public Gun EquippedGun { get; private set; }
        public bool CanShoot => EquippedGun != null;

        private BaseCharacter _owner;


        private void Start() {
            _owner = GetComponent<BaseCharacter>();
            if (_owner == null) {
                Debug.Log("GunManager must be attached to Character component.");
            }

            Equip(equipped);
        }

        public void Equip(GameObject toEquip) {
            if (EquippedGun != null) {
                Destroy(equipped);
                EquippedGun = null;
            }

            var gunInst = Instantiate(toEquip, equipSocket);
            equipped = gunInst;
            EquippedGun = equipped.GetComponent<Gun>();
            EquippedGun.GripTransform.position = equipSocket.transform.position;
        }
    }
}